using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using odmon.Helpers;
using odmon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace odmon.Services
{
	public interface IAlertService
	{
		void Send(string sendTo, string content);
		void checkAlert();
	}

	public class AlertService : IAlertService
	{
		private readonly ILogger<AlertService> _logger;
		private readonly DeviceContext _context;
		public AlertService(ILogger<AlertService> logger, DeviceContext context)
		{
			_logger = logger;
			_context = context;
		}

		public void Send(string sendTo, string content)
		{
			var data = JsonSerializer.Serialize(new
			{
				usercode = "lomadata",
				deptcode = "U6-2W8-03",
				messages = new ArrayList() {
						new {
                            //message_id = "",
                            to =  sendTo
						}
					},
				text = content,
				from = "01032768150",
				//reserved_time = "209912310000"
			},
			new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			});

			reqHttpPost(data).Wait();
		}

		private async Task reqHttpPost(string _data)
		{
			string _status;

			using (var client = new HttpClient())
			{
				string _buf;

				try
				{
					var response = await client.PostAsync(
						 "https://rest.surem.com/sms/v1/json",
						 new StringContent(_data, Encoding.UTF8, "application/json"));

					_buf = await response.Content.ReadAsStringAsync();
					_status = response.StatusCode.ToString();
				}
				catch (Exception ex)
				{
					_buf = ex.Message;
					_status = ex.HResult.ToString();
				}

				_logger.LogInformation(_status);
				_logger.LogInformation(_buf);
			}
		}


		public void checkAlert()
		{
			var arrOn = _context.Alerts.Where(a => a.setup == "on").ToList();
			var arrDeviceid = new List<string>();

			foreach (var bufOn in arrOn)
			{
				var bufqry = @"SELECT * FROM monitors WHERE sensingDt > DATE_ADD(NOW(), INTERVAL -5 SECOND)";

				var maxerr = bufOn.err;

				switch (bufOn.type)
				{
					case "nh3":
					case "h2s":
					case "odor":
					case "voc":
						bufqry += $" AND {bufOn.type} > {maxerr} ";
						break;
					default:
						continue;
				}

				var arrMoni = new List<Monitor>();

				try
				{
					_logger.LogInformation(bufqry);
					arrMoni = _context.Monitors.FromSqlRaw(bufqry).ToList();
				}
				catch (Exception ex)
				{
					_logger.LogInformation(ex.Message);
				}

				foreach (var bufMoni in arrMoni)
				{
					if (!arrDeviceid.Contains(bufMoni.deviceid))
					{
						arrDeviceid.Add(bufMoni.deviceid);
						setAlertList(bufOn.id, bufMoni.deviceid, "error", bufOn.type, $"{bufOn.type} over {maxerr}");
					}
				}

			}

		}

		private void setAlertList(int alertid, string deviceid, string type, string kind, string Msg)
		{
			var alertusers = _context.AlertUsers.ToList();

			foreach (var bufuser in alertusers)
			{
				_context.AlertLists.Add(
					new AlertList
					{
						alertid = alertid,
						userid = bufuser.userid,
						status = "",
						type = type,
						name = "",
						kind = kind,
						content = Msg,
						value = "",
						times = DateTime.Now
					});
				_context.SaveChanges();

				_logger.LogInformation($"SMS Message {bufuser.telno} : {Msg}");
				//Send(bufuser.telno, Msg);
			}


		}

	}
}
