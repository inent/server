using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odmon.Models;
using odmon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odmon.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class TestController : ControllerBase
	{
		private readonly DeviceContext _context;
		private readonly IEmailService _emailService;
		private readonly IAlertService _alertService;
		private readonly IWebHostEnvironment _env;


		public TestController(DeviceContext context, IEmailService emailService, IAlertService alertService, IWebHostEnvironment env)
		{
			_context = context;
			_emailService = emailService;
			_alertService = alertService;
			_env = env;
		}

		[HttpPost]
		public async Task<ActionResult> setAlert(Monitor req)
		{
			//if (req.id < 0)
			//{
			//	return BadRequest("need id");
			//}

			//var alert = _context.AlertLists
			//	.Where(a => a.alertid == req.id)
			//	.OrderByDescending(a => a.times)
			//	.FirstOrDefault();

			//if (alert == null)
			//{
			//	alert = new AlertList { status = "solved" };
			//}

			//var buf = new AlertList
			//{
			//	alertid = req.id,
			//	userid = "id000",
			//	//status = alert.status == "solved" ? "pending" : "solved",
			//	type = "warn",
			//	name = "장비3",
			//	kind = "전원",
			//	content = "장비고장",
			//	value = "ON",
			//	times = DateTime.Now
			//};

			//var buf = new Monitor
			//{
			//	deviceid = "device-id-05",
			//	odor = "-1",
			//	silution = "-1",
			//	solidity = "-1",
			//	h2s = "10001",
			//	nh3 = "-1",
			//	voc = "-1",
			//	airt = "-1",
			//	spd = "-1",
			//	tmp = "-1",
			//	hum = "-1",
			//	status = "-1",
			//	alert = "-1",
			//	sensingDt = DateTime.Now,
			//};

			req.sensingDt = DateTime.Now;

			_context.Monitors.Add(req);
			await _context.SaveChangesAsync();

			return Ok(new { result = "Test Alert Inserted" });
		}




		//[HttpPost]
		//public ActionResult setAlert(Alert req)
		//{
		//	var alert = _context.Alerts.Where(a => a.idx == req.idx && a.power == "on").FirstOrDefault();

		//	if (alert == null)
		//	{
		//		return BadRequest();
		//	}

		//	var rec = _context.AlertRecords.Where(a => a.alertidx == req.idx)
		//		.OrderByDescending(a => a.idx)
		//		.FirstOrDefault();

		//	var bufstr = "pending";
		//	if (rec != null)
		//	{
		//		if (rec.status == "pending")
		//		{
		//			bufstr = "solved";
		//		}
		//	}

		//	AlertRecord bufrec = new AlertRecord
		//	{
		//		alertidx = alert.idx,
		//		status = bufstr,
		//		type = "fatal",
		//		device = "장비3",
		//		kind = "전원",
		//		content = "장비고장",
		//		value = "on",
		//		times = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
		//	};
		//	_context.Add(bufrec);

		//	foreach (var bufid in alert.target.Split(","))
		//	{
		//		var user = _context.Users.Where(u => u.userid == bufid && u.onweb).FirstOrDefault();

		//		if (user != null)
		//		{
		//			if (bufstr == "pending")
		//			{
		//				AlertList buf = new AlertList { alertidx = alert.idx, userid = bufid, type = "web", content = "장비고장!!" };
		//				_context.Add(buf);
		//			}
		//			else
		//			{
		//				var buf = _context.AlertLists.Where(a => a.userid == bufid && a.alertidx == alert.idx)
		//					.OrderByDescending(a => a.idx)
		//					.FirstOrDefault();
		//				buf.confirm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		//			}


		//		}
		//	}

		//	_context.SaveChanges();

		//	return Ok(new { result = "alert setting finished." });
		//}

		public ActionResult CheckFormula(JDevice req)
		{
			//var bufexp = "0.1 * exp( 0.2 * {0} )";
			//var bufform = string.Format(bufexp, "nh3");
			//var bufh2s = string.Format("0.1 * LN({0}) + 0.2", "h2s");
			//var bufodor = string.Format("0.2 * 10 * 0.3 * POW({0}, 0.4)", "odor");

			var rex_nh3 = req.rex_nh3.Replace(" x ", "nh3");

			var bufsql = string.Format($"select convert({rex_nh3}, CHAR) as nh3 " +
				$"from monitors " +
				$"where deviceid='dc:a6:32:e4:e6:1f' " +
				$"order by sensingDt desc " +
				$"limit 1");

			try
			{
				var res = _context.Monitors
					.FromSqlRaw(bufsql)
					.Select(m => new { result = m.nh3 })
					.FirstOrDefault();

				return Ok(res);
			}
			catch (Exception ex)
			{
				return Ok(new { result = ex.Message });
			}

		}

		public ActionResult getenv()
		{
			return Ok(new { _env.ContentRootPath, _env.WebRootPath, _env.WebRootFileProvider });
		}


	}
}