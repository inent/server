using Microsoft.Extensions.Logging;
using odacc.Models;
using odmon.Models;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using System;
using System.Threading.Tasks;

namespace odacc.Commands
{
	public class noneProc : ICommand<UserSession, StringPackageInfo>
	{
		private ILogger _logger;
		//private readonly OdorContext _context;

		public noneProc(ILogger<noneProc> logger)
		{
			_logger = logger;
			//_context = context;
		}
		public void Execute(UserSession session, StringPackageInfo package)
		{
			//_logger.LogInformation(session.SessionID);
			_logger.LogInformation(session.RemoteEndPoint + " / " + package.Body);

			//session.deviceid = buf.deviceid;
			//await _userService.EnterRoom(session);

			//var buf = new WorkLog()
			//{
			//	userid = session.RemoteEndPoint.ToString(),
			//	part = "odacc",
			//	level = "Normal",
			//	content = package.Body,
			//	times = DateTime.Now
			//};

			//_context.WorkLogs.Add(buf);
			//_context.SaveChanges();
		}

	}
}
