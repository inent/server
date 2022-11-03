using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using System.Text.Json;
using System.Threading.Tasks;
using odacc.Models;
using odacc.Services;
using odmon.Models;
using System;

namespace odacc.Commands
{
	public class orderJtron : IAsyncCommand<UserSession, StringPackageInfo>
	{
		private ILogger _logger;
		private UserService _userService;

		public orderJtron(ILogger<orderInsys> logger, UserService userService)
		{
			_logger = logger;
			_userService = userService;
		}
		public async ValueTask ExecuteAsync(UserSession session, StringPackageInfo package)
		{
			var buf = JsonSerializer.Deserialize<CommandBin>(package.Body);

			//_logger.LogInformation(session.SessionID + " {buf.company}");
			_logger.LogInformation(session.RemoteEndPoint + " / " + package.Body);

			var toSess = _userService.getSessionByDeviceid(buf.sendto);

			if (toSess == null)
			{
				await session.SendAsync("id not Found");
				return;
			}

			session.reservedid = buf.sendto;
			_userService.reserveRes(session);

			await toSess.SendAsync(buf.ordermsg);

			_logger.LogInformation($"send to : {buf.sendto} / {buf.ordermsg}");


			//var buflog = new WorkLog()
			//{
			//	userid = buf.deviceId,
			//	part = buf.sendto,
			//	level = "Normal",
			//	content = buf.ordermsg,
			//	times = DateTime.Now
			//};
			//_context.WorkLogs.Add(buflog);
			//_context.SaveChanges();
		}

	}
}
