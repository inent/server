using Microsoft.Extensions.Logging;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using odacc.Models;
using odacc.Services;
using odmon.Models;
using Microsoft.Extensions.DependencyInjection;

namespace odacc.Commands
{
	public class accumProc : IAsyncCommand<UserSession, StringPackageInfo>
	{
		private ILogger _logger;
		private UserService _userService;
		private readonly IServiceProvider _serviceProvider;

		public accumProc(ILogger<accumProc> logger, UserService userService, IServiceProvider serviceProvider)
		{
			_logger = logger;
			_userService = userService;
			_serviceProvider = serviceProvider;

		}
		public async ValueTask ExecuteAsync(UserSession session, StringPackageInfo package)
		{
			var buf = JsonSerializer.Deserialize<CommandBin>(package.Body);

			_logger.LogInformation($"{session.SessionID} {buf.company}");
			_logger.LogInformation($"{session.RemoteEndPoint} / {package.Body}");

			session.deviceid = buf.deviceId;
			await _userService.EnterRoom(session);

			using (IServiceScope scope = _serviceProvider.CreateScope())
			{
				IAccumService _accumService = scope.ServiceProvider.GetRequiredService<IAccumService>();

				switch (buf.company)
				{
					case "JTRON":
						_accumService.writeDBjuwon(package.Body);
						break;
					case "insys":
						_accumService.writeDBinsys(package.Body);
						break;
				}
			}



			//var buflog = new WorkLog()
			//{
			//    userid = session.RemoteEndPoint.ToString(),
			//    part = buf.company,
			//    level = "Normal",
			//    content = package.Body,
			//    times = DateTime.Now
			//};

			//_context.WorkLogs.Add(buflog);
			//_context.SaveChanges();

		}

	}
}
