using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using System.Text.Json;
using System.Threading.Tasks;
using odacc.Models;
using odacc.Services;

namespace odacc.Commands
{
	public class orderInsys : IAsyncCommand<UserSession, StringPackageInfo>
	{
		private ILogger _logger;
		private UserService _userService;

		public orderInsys(ILogger<orderInsys> logger, UserService userService)
		{
			_logger = logger;
			_userService = userService;
		}
		public async ValueTask ExecuteAsync(UserSession session, StringPackageInfo package)
		{
			var buf = JsonSerializer.Deserialize<CommandBin>(package.Body);

			//_logger.LogInformation(session.SessionID + " {buf.company}");
			_logger.LogInformation(session.RemoteEndPoint + " / " + package.Body);

			//session.deviceid = buf.sendto;

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
		}

	}
}
