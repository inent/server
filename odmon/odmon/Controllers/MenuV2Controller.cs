using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using odmon.Models;

namespace odmon.Controllers
{
	[Route("api/v2/menu/[action]")]
	[ApiController]
	public class MenuV2Controller : ControllerBase
	{
		private readonly DeviceContext _context;
		private readonly ILogger<MenuController> _logger;

		public MenuV2Controller(DeviceContext context, ILogger<MenuController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<ActionResult> Monitoring()
		{
			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound("No User");
			}

			if (account.geocode.Length != 5)
			{
				return BadRequest("Wrong User Geocode");
			}

			var bufsql = @$"
SELECT 
	d.id AS deviceid
	,d.name
	,p.company
	,d.lati
	,d.longi
	,ifnull(m.odor		,0) AS odor
	,ifnull(m.silution	,0) AS silution	
	,ifnull(m.solidity	,0) AS solidity	
	,ifnull(m.h2s		,0) AS h2s		
	,ifnull(m.nh3		,0) AS nh3		
	,ifnull(m.voc		,0) AS voc		
	,ifnull(m.airt		,0) AS winddirect		
	,ifnull(m.spd		,0) AS windspeed		
	,ifnull(m.tmp		,0) AS temperature		
	,ifnull(m.hum		,0) AS humidity		
	,ifnull(m.status	,0) AS status	
	,ifnull(m.alert		,0) AS alert		
	,ifnull(m.sensingDt	,0) AS sensingDt
FROM coverages AS c 
LEFT JOIN devices AS d ON c.targetid = d.id
LEFT JOIN products AS p ON d.productid = p.id
LEFT JOIN recents AS m ON m.id = (SELECT MAX(id) FROM recents WHERE deviceid=d.id)
WHERE userid='{account.userid}' AND cate='D' ";

			var arr = await _context.DevMonis.FromSqlRaw(bufsql).ToListAsync();

			return Ok(arr);

		}

		public async Task<ActionResult> Products()
		{
			var arr = await _context.Products.ToListAsync();

			return Ok(arr);
		}

		public async Task<ActionResult> ProductInfo(Product req)
		{
			if (req.id == null)
			{
				return BadRequest();
			}

			var buf = await _context.Products.Where(a => a.id == req.id).FirstOrDefaultAsync();

			if (buf == null)
			{
				return NotFound();
			}

			var attribs = await _context.Attribs.Where(a => a.productid == buf.id).ToListAsync();

			return Ok(new
			{
				buf.id,
				buf.name,
				company = buf.company,
				attribs
			});
		}

		public async Task<ActionResult> BoundsDevice()
		{
			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound("No User");
			}

			if (account.geocode.Length != 5)
			{
				return BadRequest("Wrong User Geocode");
			}

			var bufsql = @$"
SELECT 
	d.id
	,d.name
	,p.company
	,p.id AS productid 
	,p.name AS productName 
	,d.control
	,d.addr
	,d.depart
	,d.geocode
	,d.status
FROM coverages AS c 
LEFT JOIN devices AS d ON c.targetid = d.id
LEFT JOIN products AS p ON d.productid = p.id
WHERE userid='{account.userid}' AND cate='D' ";

			var res = await _context.DevBounds.FromSqlRaw(bufsql).ToListAsync();

			return Ok(res);
		}

		public async Task<ActionResult> BoundsChart()
		{
			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound("No User");
			}

			if (account.geocode.Length != 5)
			{
				return BadRequest("Wrong User Geocode");
			}

			var bufsql = @$"
SELECT 
deviceid,
nh3,
h2s,
odor,
voc,
sensingDt
FROM (
   SELECT *, RANK() OVER (PARTITION BY m.deviceid ORDER BY m.sensingDt DESC) AS a
   FROM (
	SELECT 
		r.deviceid
		,r.nh3
		,r.h2s
		,r.odor
		,r.voc
		,r.sensingDt
	FROM coverages AS c 
	LEFT JOIN recents AS r ON c.targetid = r.deviceid
	WHERE userid='{account.userid}' AND cate='D'
	)  m
) AS rankrow
WHERE deviceid IS NOT NULL AND rankrow.a <= 10";

			var res = await _context.DevCharts.FromSqlRaw(bufsql).ToListAsync();

			return Ok(res);
		}


		private dynamic getChart(string deviceid, DateTime fromTime, DateTime toTime, int cycle, int num)
		{
			var bufrex = _context.JDevices.Where(d => d.id == deviceid).FirstOrDefault();

			List<Object> NH3 = new List<Object>();
			List<Object> H2S = new List<Object>();
			List<Object> Odor = new List<Object>();
			List<Object> Voc = new List<Object>();

			//var monis = _context.Monitors.Where(m => m.deviceid == deviceid && m.sensingDt > fromTime && m.sensingDt < toTime)
			//		.OrderByDescending(m => m.sensingDt)
			//		.ToList();

			var monis = new List<Monitor>();

			var bufsql = @",sensingDt,id,deviceid,silution,solidity,airt,spd,tmp,hum,status,alert FROM monitors ";

			if (bufrex == null)
			{
				bufsql = $"SELECT convert(AVG(nh3),CHAR) as nh3," +
					$"convert(AVG(h2s),CHAR) as h2s," +
					$"convert(AVG(odor),CHAR) as odor," +
					$"convert(AVG(voc),CHAR) as voc" + bufsql;
			}
			else
			{
				var rex_nh3 = bufrex.rex_nh3.Replace(" x ", "AVG(nh3)");
				var rex_h2s = bufrex.rex_h2s.Replace(" x ", "AVG(h2s)");
				var rex_odor = bufrex.rex_odor.Replace(" x ", "AVG(odor)");
				var rex_voc = bufrex.rex_voc.Replace(" x ", "AVG(voc)");

				bufsql =
					$"SELECT convert(format(LEAST(GREATEST(ifnull({rex_nh3},0),0),100),3), CHAR) as nh3, " +
					$"convert(format(LEAST(GREATEST(ifnull({rex_h2s},0),0),100),3), CHAR) as h2s, " +
					$"convert(format(LEAST(GREATEST(ifnull({rex_odor},0),0),100),3), CHAR) as odor, " +
					$"convert(format(LEAST(GREATEST(ifnull({rex_voc},0),0),100),3), CHAR) as voc " +
					bufsql;
			}

			bufsql = $"SELECT * FROM (" + bufsql +
					$"WHERE deviceid='{deviceid}' and " +
					$"sensingDt > '{fromTime.ToString("yyyy-MM-dd HH:mm:ss")}' and " +
					$"sensingDt < '{toTime.ToString("yyyy-MM-dd HH:mm:ss")}'" +
					$"GROUP BY FLOOR(UNIX_TIMESTAMP(sensingDt)/{cycle}) " +
					$"ORDER BY sensingDt DESC LIMIT {num}) AS a ORDER BY sensingDt";

			_logger.LogInformation(bufsql);

			try
			{
				monis = _context.Monitors.FromSqlRaw(bufsql).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				//return BadRequest(new { result = ex.Message });
			}

			foreach (Monitor moni in monis)
			{
				var dt = moni.sensingDt.ToString("yyyy-MM-dd HH:mm:ss");

				NH3.Add(new { x = dt, y = moni.nh3 });
				H2S.Add(new { x = dt, y = moni.h2s });
				Odor.Add(new { x = dt, y = moni.odor });
				Voc.Add(new { x = dt, y = moni.voc });
			}

			return new { NH3, H2S, Odor, Voc };
		}


		//private dynamic getChart(string deviceid, DateTime basetime, int cycle)
		//{
		//	List<Object> NH3 = new List<Object>();
		//	List<Object> H2S = new List<Object>();
		//	List<Object> Odor = new List<Object>();
		//	List<Object> Voc = new List<Object>();

		//	var monis = new List<Monitor>();

		//	monis = _context.Monitors.Where(m => m.deviceid == deviceid && m.sensingDt < basetime)
		//		.OrderByDescending(m => m.sensingDt)
		//		.Take(10)
		//		.ToList();

		//	foreach (Monitor moni in monis)
		//	{
		//		var dt = moni.sensingDt.ToString("yyyy-MM-dd HH:mm:ss");

		//		NH3.Add(new { x = dt, y = moni.nh3 });
		//		H2S.Add(new { x = dt, y = moni.h2s });
		//		Odor.Add(new { x = dt, y = moni.odor });
		//		Voc.Add(new { x = dt, y = moni.voc });
		//	}

		//	return new { NH3, H2S, Odor, Voc };
		//}



		//[HttpPost]
		//public async Task<IActionResult> currentdevice()
		//{
		//	var devices = await _context.Devices.ToListAsync();
		//	var deviceAttr = await _context.Attribs.ToListAsync();

		//	List<Object> arr = new List<Object>();

		//	foreach (Device bufdev in devices)
		//	{
		//		var listAttr = await _context.Attribs.Where(a => a.productid == bufdev.productid).ToListAsync();
		//		var devdto = new
		//		{
		//			bufdev.id,
		//			bufdev.productid,
		//			bufdev.name,
		//			bufdev.control,
		//			bufdev.macaddr,
		//			bufdev.addr,
		//			bufdev.geocode,
		//			bufdev.lati,
		//			bufdev.longi,
		//			bufdev.status,
		//			bufdev.memo,
		//			bufdev.firmware,
		//			listAttr
		//		};
		//		arr.Add(devdto);
		//	}

		//	return Ok(arr);
		//}


		[HttpPost]
		public async Task<IActionResult> alertusage()
		{
			if (String.IsNullOrEmpty(User.Identity.Name))
			{
				return BadRequest("Login First");
			}

			DateTime today = DateTime.Now;

			var num_all = await _context.AlertLists.CountAsync();

			var num_month = await _context.AlertLists
				.Where(a => a.times.Month == today.Month && a.times.Year == today.Year)
				.CountAsync();

			var num_year = await _context.AlertLists
				.Where(a => a.times.Year == today.Year)
				.CountAsync();

			var users = new List<AlertUser>();

			if (User.IsInRole(Role.Super))
			{
				users = await _context.AlertUsers.ToListAsync();
			}
			else if (User.IsInRole(Role.Admin))
			{
				var users4admin = await users4role();

				var bufsql = $"SELECT a.id, a.userid, a.name, a.telno " +
					$"FROM alertusers a " +
					$"LEFT JOIN users AS u ON u.userid = a.userid " +
					$"WHERE u.geocode LIKE (SELECT CONCAT(SUBSTR(geocode,1,3),'%') FROM users WHERE userid='{User.Identity.Name}')";

				users = await _context.AlertUsers.FromSqlRaw(bufsql).ToListAsync();
			}
			else
			{
				users = await _context.AlertUsers.Where(u => u.userid == User.Identity.Name).ToListAsync();
			}

			return Ok(new { all = num_all, month = num_month, year = num_year, users });
		}

		[HttpPost]
		public async Task<IActionResult> chart(ReqChart req)
		{
			List<Object> res = new List<Object>();

			if (req.deviceid?.Count == null || req.deviceid?.Count < 1)
			{
				return BadRequest("No Data");
			}

			int cycle = 0;

			switch (req.sect)
			{
				case "rt": cycle = 5; break;
				case "1m": cycle = 60; break;
				case "5m": cycle = 60 * 5; break;
				case "10m": cycle = 60 * 10; break;
				case "1h": cycle = 60 * 60; break;
				case "1d": cycle = 60 * 60 * 24; break;
				case "1w": cycle = 60 * 60 * 24 * 7; break;
				case "1M": cycle = 60 * 60 * 24 * 30; break;
				default:
					return BadRequest("No Data");
			}

			switch (req.arith)
			{
				case "sum": break;
				case "avg": break;
				case "ctr": break;
				case "min": break;
				case "max": break;
				case "frq": break;
				default:
					return BadRequest("No Data");
			}

			var fromDate = Convert.ToDateTime(req.fromDate);
			var toDate = Convert.ToDateTime(req.toDate);

			var limit = Convert.ToInt32(req.limit);

			foreach (string deviceid in req.deviceid)
			{
				var arr = getChart(deviceid, fromDate, toDate, cycle, limit);

				res.Add(new
				{
					deviceid,
					arr.NH3,
					arr.H2S,
					arr.Odor,
					arr.Voc
				});

				//res.Add(await ChartData(deviceid, toDate, cycle));
			}

			await Task.CompletedTask;

			return Ok(res);
		}

		//private async Task<dynamic> ChartData(string deviceid, DateTime basetime, int cycle)
		//{
		//	//List<Object> res = new List<Object>();

		//	//var monitor = await _context.Monitors.ToListAsync();

		//	var NH3 = getChart(basetime, cycle);
		//	var H2S = getChart(basetime, cycle);
		//	var Odor = getChart(basetime, cycle);
		//	var Voc = getChart(basetime, cycle);

		//	return new
		//	{
		//		deviceid,
		//		NH3,
		//		H2S,
		//		Odor,
		//		Voc
		//	};
		//}

		public async Task<IActionResult> GeoCodeList()
		{
			//List<Object> res = new List<Object>();

			var res = await _context.GeoCodes.ToListAsync();

			return Ok(res);
		}

		public async Task<ActionResult> users4geo()
		{
			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound("Login First");
			}

			var bufsql = "SELECT * FROM users ";

			if (account.geocode.Substring(0, 1) == "1")
			{
				if (account.geocode.Substring(3) == "00")
				{
					bufsql += $"WHERE geocode LIKE '{account.geocode.Substring(0, 3)}%' ";
				}
				else
				{
					bufsql += $"WHERE geocode = '{account.geocode}' ";
				}
			}

			switch (account.role)
			{
				case "super":
				case "admin":
					break;
				case "user":
					bufsql += $"AND depart = '{account.depart}'";
					break;
				default:
					return BadRequest("wrong role");
			}

			var accounts = await _context.Users.FromSqlRaw(bufsql).ToListAsync();

			var res = new List<Object>();

			foreach (var buf in accounts)
			{
				res.Add(new
				{
					buf.userid,
					buf.username,
					buf.depart,
					buf.position,
					buf.email,
					buf.phone,
					buf.role,
					buf.geocode
				});
			}

			return Ok(res);
		}

		private async Task<List<string>> users4role()
		{
			var res = new List<string>();

			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return res;
			}

			var bufsql = "SELECT * FROM users ";

			if (account.geocode.Substring(0, 1) == "1")
			{
				if (account.geocode.Substring(3) == "00")
				{
					bufsql += $"WHERE geocode LIKE '{account.geocode.Substring(0, 3)}%' ";
				}
				else
				{
					bufsql += $"WHERE geocode = '{account.geocode}' ";
				}
			}

			switch (account.role)
			{
				case "super":
				case "admin":
					break;
				case "user":
					bufsql += $"AND depart = '{account.depart}'";
					break;
				default:
					return res;
			}

			var accounts = await _context.Users.FromSqlRaw(bufsql).ToListAsync();

			foreach (var buf in accounts)
			{
				res.Add(buf.userid);
			}

			return res;
		}

		private async Task<List<string>> Device4GeoAsync(User account)
		{
			var bufsql = "SELECT * FROM devices ";

			if (account.geocode.Substring(0, 1) == "1")
			{
				if (account.geocode.Substring(3) == "00")
				{
					bufsql += $"WHERE geocode LIKE '{account.geocode.Substring(0, 3)}%' ";
				}
				else
				{
					bufsql += $"WHERE geocode = '{account.geocode}' ";
				}
			}

			switch (account.role)
			{
				case "super":
				case "admin":
					break;
				case "user":
					bufsql += $"AND depart = '{account.depart}'";
					break;
			}

			var arrDevice = await _context.Devices.FromSqlRaw(bufsql).ToListAsync();

			var res = new List<string>();

			foreach (var bufDevice in arrDevice)
			{
				res.Add(bufDevice.id);
			}

			return res;
		}

		public async Task<ActionResult> currcond()
		{
			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound("Login First");
			}

			var bufqry = $"SELECT COUNT(id) AS cnt, DATE_FORMAT(times, '%Y-%m-%d') AS f_times " +
				$"FROM alertlists " +
				$"WHERE userid = '{User.Identity.Name}' " +
				$"GROUP BY f_times " +
				$"LIMIT 20";

			var arrAlertList = new List<Object>();

			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = bufqry;
				_context.Database.OpenConnection();
				using (var reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							arrAlertList.Add(new
							{
								y = reader.GetValue(0).ToString(),
								x = reader.GetString(1)
							});
						}
					}
				}
			}

			var arrDevice = await Device4GeoAsync(account);
			var devnum = arrDevice.Count().ToString();
			var devon = _context.Devices
				.Where(d => arrDevice.Contains(d.id) && d.status == "on")
				.Count().ToString();

			bufqry = $"SELECT COUNT(id) AS cnt, DATE_FORMAT(times, '%Y-%m-%d') AS f_times " +
				$"FROM alertlists " +
				$"WHERE userid = '{User.Identity.Name}' " +
				$"GROUP BY f_times " +
				$"LIMIT 20";

			var nh3 = "0";
			var h2s = "0";
			var odor = "0";
			var voc = "0";
			var indol = "0";

			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = bufqry;
				_context.Database.OpenConnection();
				using (var reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							switch (reader.GetString(1))
							{
								case "nh3": nh3 = reader.GetValue(0).ToString(); break;
								case "h2s": h2s = reader.GetValue(0).ToString(); break;
								case "odor": odor = reader.GetValue(0).ToString(); break;
								case "voc": voc = reader.GetValue(0).ToString(); break;
								case "indol": indol = reader.GetValue(0).ToString(); break;
							}
						}
					}
				}
			}

			var res = new
			{
				alerts = arrAlertList,
				devnum,
				devon,
				nh3,
				h2s,
				odor,
				voc,
				indol
			};

			return Ok(res);
		}


	}
}
