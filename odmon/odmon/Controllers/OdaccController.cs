using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odacc.Models;
using odmon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odmon.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OdaccController : ControllerBase
	{
		private readonly DeviceContext _context;

		public OdaccController(DeviceContext context)
		{
			_context = context;
		}

		public async Task<ActionResult> AccumJuwon(ReqJuwon req)
		{
			var buf = new Monitor()
			{
				id = 0,
				deviceid = req.deviceId,
				odor = req.ioStat.input.odorVolt.ToString(),
				silution = "-1",
				solidity = "-1",
				h2s = req.ioStat.input.h2sVolt.ToString(),
				nh3 = req.ioStat.input.nh3Volt.ToString(),
				voc = req.ioStat.input.vocVolt.ToString(),
				airt = req.ioStat.input.dirAngle.ToString(),
				spd = req.ioStat.input.aVelCnt.ToString(),
				tmp = req.ioStat.input.exTmp.ToString(),
				hum = req.ioStat.input.exHum.ToString(),
				status = req.ioStat.input.btStart.ToString(),
				alert = "-1",
				sensingDt = Convert.ToDateTime(req.sendDt)
			};

			_context.Monitors.Add(buf);
			_context.SaveChanges();

			var bufsql = $"INSERT INTO recents SELECT * FROM monitors where id={buf.id}";

			await _context.Database.ExecuteSqlRawAsync(bufsql);

			return Ok(new { result = "success" });
		}

		public async Task<ActionResult> AccumInsys(ReqInsys req)
		{
			var buf = new Monitor()
			{
				id = 0,
				deviceid = req.deviceId,
				odor = nullCheck(req.sensorData.indol),
				silution = "-1",
				solidity = "-1",
				h2s = nullCheck(req.sensorData.h2s),
				nh3 = nullCheck(req.sensorData.nh3),
				voc = nullCheck(req.sensorData.voc),
				airt = "-1",
				spd = "-1",
				tmp = nullCheck(req.sensorData.temperature),
				hum = nullCheck(req.sensorData.humidity),
				status = "-1",
				alert = "-1",
				sensingDt = Convert.ToDateTime(req.timestamp)
			};

			_context.Monitors.Add(buf);
			_context.SaveChanges();

			var bufsql = $"INSERT INTO recents SELECT * FROM monitors where id={buf.id}";

			await _context.Database.ExecuteSqlRawAsync(bufsql); 
			
			return Ok(new { result = "success" });
		}

		private string nullCheck(string buf)
		{
			return (string.IsNullOrEmpty(buf) ? "-1" : buf);
		}
	}
}
