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
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class MenuController : ControllerBase
	{
		private readonly DeviceContext _context;
		private readonly ILogger<MenuController> _logger;

		public MenuController(DeviceContext context, ILogger<MenuController> logger)
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

			var bufsql = @"
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
FROM devices AS  d
LEFT JOIN products  AS p ON d.productid = p.id
LEFT JOIN monitors  AS m ON m.id = (SELECT MAX(id) FROM monitors WHERE deviceid=d.id) ";

			if (account.geocode.Substring(0, 1) == "1")
			{
				if (account.geocode.Substring(3) == "00")
				{
					bufsql += $"WHERE d.geocode LIKE '{account.geocode.Substring(0, 3)}%' ";
				}
				else
				{
					bufsql += $"WHERE d.geocode = '{account.geocode}' ";
				}
			}

			switch (account.role)
			{
				case "super":
				case "admin":
					break;
				case "user":
					bufsql += $"AND d.depart = '{account.depart}'";
					break;
				default:
					return BadRequest("wrong role");
			}

			var arr = await _context.DevMonis.FromSqlRaw(bufsql).ToListAsync();


			//List<Object> arr = new List<Object>();

			//using (var command = _context.Database.GetDbConnection().CreateCommand())
			//{
			//	command.CommandText = bufsql;
			//	_context.Database.OpenConnection();
			//	using (var reader = command.ExecuteReader())
			//	{
			//		if (reader.HasRows)
			//		{
			//			while (reader.Read())
			//			{
			//				arr.Add(new
			//				{
			//					deviceid = reader.GetString(0),
			//					name = reader.GetString(1),
			//					company = reader.GetString(2),
			//					lati = reader.GetString(3),
			//					longi = reader.GetString(4),
			//					odor = reader.GetString(5),
			//					silution = reader.GetString(6),
			//					solidity = reader.GetString(7),
			//					h2s = reader.GetString(8),
			//					nh3 = reader.GetString(9),
			//					voc = reader.GetString(10),
			//					winddirect = reader.GetString(11),
			//					windspeed = reader.GetString(12),
			//					temperature = reader.GetString(13),
			//					humidity = reader.GetString(14),
			//					status = reader.GetString(15),
			//					alert = reader.GetString(16)
			//				});
			//			}
			//		}
			//	}
			//}

			return Ok(arr);

			//var arrdev = await _context.Devices
			//	.Join(
			//		_context.Products,
			//		devices => devices.productid,
			//		products => products.id,
			//		(devices, products) => new
			//		{
			//			deviceid = devices.id,
			//			name = devices.name,
			//			company = products.company,
			//			lati = devices.lati,
			//			longi = devices.longi
			//		}
			//	)
			//	.GroupJoin(
			//		_context.Monitors.DefaultIfEmpty(),
			//		combined => combined.deviceid,
			//		monitors => monitors.deviceid,
			//		(combined, monitorGroup) => new
			//		{
			//			deviceid = combined.deviceid,
			//			name = combined.name,
			//			company = combined.company,
			//			lati = combined.lati,
			//			longi = combined.longi,
			//			//odor = monitorGroup.LastOrDefault().odor,
			//			//silution = monitorGroup.LastOrDefault().silution,
			//			//solidity = monitorGroup.LastOrDefault().solidity,
			//			//h2s = monitorGroup.LastOrDefault().h2s,
			//			//nh3 = monitorGroup.LastOrDefault().nh3,
			//			//voc = monitorGroup.LastOrDefault().voc,
			//			//winddirect = monitorGroup.LastOrDefault().airt,
			//			//windspeed = monitorGroup.LastOrDefault().spd,
			//			//temperature = monitorGroup.LastOrDefault().tmp,
			//			//humidity = monitorGroup.LastOrDefault().hum,
			//			//status = monitorGroup.LastOrDefault().status,
			//			//alert = monitorGroup.LastOrDefault().alert
			//		}
			//	)
			//	.ToListAsync();

			//return Ok(arrdev);


			//var arrdev = await _context.Devices
			//	.Join(
			//		_context.Products,
			//		devices => devices.productid,
			//		products => products.id,
			//		(devices, products) => new
			//		{
			//			deviceid = devices.id,
			//			name = devices.name,
			//			company = products.company,
			//			lati = devices.lati,
			//			longi = devices.longi
			//		}
			//	)
			//	.ToListAsync();

			//var arrdev = await _context.Devices.Where(d => d.geocode == account.geocode)
			//	.Join(
			//		_context.Products,
			//		devices => devices.productid,
			//		products => products.id,
			//		(devices, products) => new
			//		{
			//			deviceid = devices.id,
			//			name = devices.name,
			//			company = products.company,
			//			lati = devices.lati,
			//			longi = devices.longi
			//		}
			//	)
			//	.ToListAsync();

			//if (account.role == "user")
			//{
			//	arrdev = await _context.Devices.Where(d => d.geocode == account.geocode && d.depart == account.depart)
			//		.Join(
			//			_context.Products,
			//			devices => devices.productid,
			//			products => products.id,
			//			(devices, products) => new
			//			{
			//				deviceid = devices.id,
			//				name = devices.name,
			//				company = products.company,
			//				lati = devices.lati,
			//				longi = devices.longi
			//			}
			//		)
			//		.ToListAsync();
			//}


			//var arrmon = await _context.Devices
			//	.GroupJoin(
			//		_context.Monitors,
			//		devices => devices.id,
			//		monitors => monitors.deviceid,
			//		(devices, monitorGroup) => new
			//		{
			//			devices = devices,
			//			monitorGroup = monitorGroup.DefaultIfEmpty()
			//		}
			//	)
			//	.ToListAsync();

			//List < Object> arr = new List<Object>();

			//foreach (var dev in arrdev)
			//{
			//	var moni = await _context.Monitors
			//		.Where(a => a.deviceid == dev.deviceid)
			//		.OrderByDescending(a => a.id)
			//		.FirstOrDefaultAsync();

			//	if (moni == null)
			//	{
			//		moni = new Monitor();
			//	}

			//	arr.Add(new
			//	{
			//		deviceid = dev.deviceid,
			//		name = dev.name,
			//		company = dev.company,
			//		odor = moni.odor,
			//		silution = moni.silution,
			//		solidity = moni.solidity,
			//		h2s = moni.h2s,
			//		nh3 = moni.nh3,
			//		voc = moni.voc,
			//		lati = dev.lati,
			//		longi = dev.longi,
			//		winddirect = moni.airt,
			//		windspeed = moni.spd,
			//		temperature = moni.tmp,
			//		humidity = moni.hum,
			//		status = moni.status,
			//		alert = moni.alert
			//	});
			//}

			//return Ok(arr);
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

			//dynamic arrS = new ArrayList();
			//dynamic arrA = new ArrayList();
			//dynamic arrE = new ArrayList();

			//foreach (Attrib attr in attribs)
			//{
			//	switch (attr.type)
			//	{
			//		case "Sensor":
			//			arrS.Add(new
			//			{
			//				attr.id,
			//				attr.alias,
			//				attr.name,
			//				attr.onoff,
			//				attr.label,
			//				attr.spec,
			//				attr.chemiunit,
			//				attr.threshold,
			//				attr.min,
			//				attr.max,
			//				attr.elecunit
			//			});
			//			break;
			//		case "Actuator":
			//			arrA.Add(new
			//			{
			//				attr.id,
			//				attr.alias,
			//				attr.name,
			//				attr.label,
			//				attr.chemiunit,
			//				attr.threshold,
			//				attr.min,
			//				attr.max
			//			});
			//			break;
			//		default:
			//			arrE.Add(new
			//			{
			//				attr.id,
			//				attr.type,
			//				attr.alias,
			//				attr.name,
			//				attr.label,
			//				attr.chemiunit,
			//				attr.threshold,
			//				attr.min,
			//				attr.max,
			//				attr.note
			//			});
			//			break;
			//	}
			//}

			//return Ok(new
			//{
			//	buf.id,
			//	buf.name,
			//	company = buf.company,
			//	Sensor = arrS,
			//	Actuator = arrA,
			//	etc = arrE
			//});

			return Ok(new
			{
				buf.id,
				buf.name,
				company = buf.company,
				attribs
			});
		}

		//public async Task<ActionResult> DeviceList(Device req)
		//{
		//	var buf = await _context.Products.Where(a => a.id == req.productid).FirstOrDefaultAsync();

		//	if (buf == null)
		//	{
		//		return NotFound();
		//	}

		//	List<Object> res = new List<Object>();

		//	var devices = await _context.Devices.Where(a => a.productid == req.productid).ToListAsync();

		//	foreach (Device bufdev in devices)
		//	{
		//		var arr = getChart(bufdev.id, DateTime.Now, 0);

		//		var devdto = new
		//		{
		//			bufdev.id,
		//			bufdev.name,
		//			company = buf.company,
		//			bufdev.control,
		//			bufdev.addr,
		//			bufdev.geocode,
		//			bufdev.status,
		//			arr.NH3,
		//			arr.H2S,
		//			arr.Odor,
		//			arr.Voc
		//		};
		//		res.Add(devdto);
		//	}

		//	return Ok(res);
		//}

		public async Task<ActionResult> DeviceAllList()
		{
			var account = await _context.Users.Where(a => a.userid == User.Identity.Name).FirstOrDefaultAsync();
			if (account == null)
			{
				return NotFound("No User");
			}

			var prods = await _context.Products.ToListAsync();

			List<Object> res = new List<Object>();

			foreach (var prod in prods)
			{
				var arrqry = _context.Devices.Where(d => d.productid == prod.id);

				if (account.geocode.Substring(0, 1) == "1")
				{
					if (account.geocode.Substring(3) == "00")
					{
						var key = account.geocode.Substring(0, 3);
						arrqry = arrqry.Where(d => d.geocode.Contains(key));
					}
					else
					{
						arrqry = arrqry.Where(d => d.geocode == account.geocode);
					}
				}

				if (account.role == "user")
				{
					arrqry = arrqry.Where(d => d.depart == account.depart);
				}

				var devices = await arrqry.ToListAsync();

				foreach (Device bufdev in devices)
				{
					var arr = getChart(bufdev.id, DateTime.Now, 0);
					//var arr = getChart(bufdev.id, DateTime.Now.AddYears(-1), DateTime.Now, 1, 10);

					var devdto = new
					{
						bufdev.id,
						bufdev.name,
						company = prod.company,
						productid = prod.id,
						productName = prod.name,
						bufdev.control,
						bufdev.addr,
						bufdev.depart,
						bufdev.geocode,
						bufdev.status,
						arr.NH3,
						arr.H2S,
						arr.Odor,
						arr.Voc
					};
					res.Add(devdto);
				}
			}

			return Ok(res);
		}

		//public async Task<ActionResult> DeviceInfo(Device req)
		//{

		//	var buf = await _context.Devices.Where(a => a.id == req.id).FirstOrDefaultAsync();

		//	if (buf == null)
		//	{
		//		return NotFound();
		//	}

		//	var product = await _context.Products.Where(a => a.id == buf.productid).FirstOrDefaultAsync();

		//	if (product == null)
		//	{
		//		return NotFound();
		//	}

		//	var arr = getChart(buf.id, DateTime.Now, 0);

		//	var res = new
		//	{
		//		deviceid = buf.id,
		//		buf.productid,
		//		productname = product.name,
		//		product.company,
		//		product.regist,
		//		devicename = buf.name,
		//		buf.control,
		//		buf.macaddr,
		//		buf.addr,
		//		buf.geocode,
		//		buf.status,
		//		buf.memo,
		//		buf.firmware,
		//		buf.serverip,
		//		buf.serverport,
		//		//buf.measect,
		//		//buf.meacycle,
		//		//buf.flushsect,
		//		//buf.restsect,
		//		//buf.multiple,
		//		//buf.ratio,
		//		//buf.constant,
		//		//buf.resolution,
		//		//buf.deci,
		//		arr.NH3,
		//		arr.H2S,
		//		arr.Odor,
		//		arr.Voc
		//	};

		//	return Ok(res);

		//}

		//private List<Object> getChart()
		//{
		//    List<Object> res = new List<Object>();
		//    var rand = new Random();

		//    for (int i = 1; i < 11; i++)
		//    {
		//        var obj = new
		//        {
		//            x = DateTime.Now.AddSeconds(-5 * i).ToString("HH:mm:ss"),
		//            y = String.Format("{0:F2}", rand.NextDouble() * 10)
		//        };

		//        res.Add(obj);
		//    }

		//    return res;
		//}

		//private List<Object> getChart(DateTime basetime, int cycle)
		//{
		//	List<Object> res = new List<Object>();
		//	var rand = new Random();

		//	for (int i = 1; i <= 20; i++)
		//	{
		//		var obj = new
		//		{
		//			x = basetime.AddSeconds(cycle * i).ToString("yyyy-MM-dd HH:mm:ss"),
		//			y = String.Format("{0:F2}", rand.NextDouble() * 10)
		//		};

		//		res.Add(obj);
		//	}

		//	return res;
		//}

		//private dynamic getChart(string deviceid)
		//{
		//	var monis = _context.Monitors.Where(m => m.deviceid == deviceid)
		//		.OrderByDescending(m => m.sensingDt)
		//		.Take(10)
		//		.ToList();

		//	List<Object> NH3 = new List<Object>();
		//	List<Object> H2S = new List<Object>();
		//	List<Object> Odor = new List<Object>();
		//	List<Object> Voc = new List<Object>();

		//	foreach (Monitor moni in monis)
		//	{
		//		var dt = moni.sensingDt.ToString("HH:mm:ss");

		//		NH3.Add(new { x = dt, y = moni.nh3 });
		//		H2S.Add(new { x = dt, y = moni.h2s });
		//		Odor.Add(new { x = dt, y = moni.odor });
		//		Voc.Add(new { x = dt, y = moni.voc });
		//	}

		//	return new { NH3, H2S, Odor, Voc };
		//}

		private dynamic getChart(string deviceid, DateTime basetime, int cycle)
		{
			var bufrex = _context.JDevices.Where(d => d.id == deviceid).FirstOrDefault();

			List<Object> NH3 = new List<Object>();
			List<Object> H2S = new List<Object>();
			List<Object> Odor = new List<Object>();
			List<Object> Voc = new List<Object>();

			var monis = new List<Monitor>();

			if (bufrex == null)
			{
				monis = _context.Monitors.Where(m => m.deviceid == deviceid && m.sensingDt < basetime)
					.OrderByDescending(m => m.sensingDt)
					.Take(10)
					.ToList();

			}
			else
			{
				var rex_nh3 = bufrex.rex_nh3.Replace(" x ", "nh3");
				var rex_h2s = bufrex.rex_h2s.Replace(" x ", "h2s");
				var rex_odor = bufrex.rex_odor.Replace(" x ", "odor");
				var rex_voc = bufrex.rex_voc.Replace(" x ", "voc");

				var bufsql = string.Format(
					$"select " +
					$"convert(format(ifnull({rex_nh3},0),3), CHAR) as nh3, " +
					$"convert(format(ifnull({rex_h2s},0),3), CHAR) as h2s, " +
					$"convert(format(ifnull({rex_odor},0),3), CHAR) as odor, " +
					$"convert(format(ifnull({rex_voc},0),3), CHAR) as voc " +
					@",id
,deviceid
,silution
,solidity
,airt
,spd
,tmp
,hum
,status
,alert
,sensingDt " +
					$"from monitors " +
					$"where deviceid='{deviceid}' and sensingDt < '{basetime.ToString("yyyy-MM-dd HH:mm:ss")}'" +
					$"order by sensingDt desc " +
					$"limit 10");

				try
				{
					monis = _context.Monitors.FromSqlRaw(bufsql).ToList();
				}
				catch (Exception ex)
				{
					_logger.LogInformation(ex.Message);
					//return BadRequest(new { result = ex.Message });
				}
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
