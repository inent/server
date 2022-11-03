using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using odacc.Models;
using odmon.Models;

namespace odacc.Services
{
	public interface IAccumService
	{
		void writeDBjuwon(string bufjson);
		void writeDBinsys(string bufjson);
	}

	public class AccumService : IAccumService
	{
		private readonly ILogger _logger;
		private readonly DeviceContext _context;
		private readonly UserService _userService;

		public AccumService(ILogger<AccumService> logger, DeviceContext context, UserService userService)
		{
			_logger = logger;
			_context = context;
			_userService = userService;
		}

		public void writeDBjuwon(string bufjson)
		{
			var req = JsonSerializer.Deserialize<ReqJuwon>(bufjson);

			if (req.type != "ioStat")
			{
				_userService.reservedSend(req.deviceId, bufjson);
				//var buflog = new WorkLog()
				//{
				//	userid = req.deviceId,
				//	part = req.company,
				//	level = "Normal",
				//	content = bufjson,
				//	times = DateTime.Now
				//};
				//_context.WorkLogs.Add(buflog);
				//_context.SaveChanges();

				return;
			}

			var res = NetworkMan.Instance.reqHttpPost("api/Odacc/AccumJuwon",bufjson).Result;

			_logger.LogInformation(res);

			//var buf = new Monitor()
			//{
			//	id = 0,
			//	deviceid = req.deviceId,
			//	odor = req.ioStat.input.odorVolt.ToString(),
			//	silution = "-1",
			//	solidity = "-1",
			//	h2s = req.ioStat.input.h2sVolt.ToString(),
			//	nh3 = req.ioStat.input.nh3Volt.ToString(),
			//	voc = req.ioStat.input.vocVolt.ToString(),
			//	airt = req.ioStat.input.dirAngle.ToString(),
			//	spd = req.ioStat.input.aVelCnt.ToString(),
			//	tmp = req.ioStat.input.exTmp.ToString(),
			//	hum = req.ioStat.input.exHum.ToString(),
			//	status = req.ioStat.input.btStart.ToString(),
			//	alert = "-1",
			//	sensingDt = Convert.ToDateTime(req.sendDt)
			//};

			//_context.Monitors.Add(buf);
			//_context.SaveChanges();

		}

		public void writeDBinsys(string bufjson)
		{
			var req = JsonSerializer.Deserialize<ReqInsys>(bufjson);

			if (req.type != "data")
			{
				_userService.reservedSend(req.deviceId, bufjson);
				//var buflog = new WorkLog()
				//{
				//	userid = req.deviceId,
				//	part = req.company,
				//	level = "Normal",
				//	content = bufjson,
				//	times = DateTime.Now
				//};
				//_context.WorkLogs.Add(buflog);
				//_context.SaveChanges();

				return;
			}

			var res = NetworkMan.Instance.reqHttpPost("api/Odacc/AccumInsys", bufjson).Result;

			_logger.LogInformation(res);

			//var buf = new Monitor()
			//{
			//	id = 0,
			//	deviceid = req.deviceId,
			//	odor = nullCheck(req.sensorData.odor),
			//	silution = "-1",
			//	solidity = "-1",
			//	h2s = nullCheck(req.sensorData.h2s),
			//	nh3 = nullCheck(req.sensorData.nh3),
			//	voc = nullCheck(req.sensorData.voc),
			//	airt = "-1",
			//	spd = "-1",
			//	tmp = nullCheck(req.sensorData.temperature),
			//	hum = nullCheck(req.sensorData.humidity),
			//	status = "-1",
			//	alert = "-1",
			//	sensingDt = Convert.ToDateTime(req.timestamp)
			//};

			//_context.Monitors.Add(buf);
			//_context.SaveChanges();

		}

		private string nullCheck(string buf)
		{
			return (string.IsNullOrEmpty(buf) ? "-1" : buf);
		}
	}
}
