using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace odacc
{
	public class UserService
	{

		private ILogger _logger;

		private HashSet<UserSession> _users;
		private HashSet<UserSession> _reserves;

		public UserService(ILogger<UserService> logger)
		{
			_logger = logger;
			_users = new HashSet<UserSession>();
			_reserves = new HashSet<UserSession>();
		}

		public async ValueTask BroadcastMessage(UserSession session, string message)
		{
			foreach (var u in _users)
			{
				await u.SendAsync($"{session.deviceid}: {message}");
			}
		}

		public async ValueTask EnterRoom(UserSession session)
		{
			lock (_users)
			{
				_users.Add(session);
			}

			//foreach (var u in _users)
			//{
			//    await u.SendAsync($"{session.deviceid} entered just now.");
			//}

			//_logger.LogInformation($"{session.deviceid} entered.");

			session.Closed += async (s, e) =>
			{
				await LeaveRoom(s as UserSession);
			};

			await Task.CompletedTask;
		}

		public async ValueTask LeaveRoom(UserSession session)
		{
			lock (_users)
			{
				_users.Remove(session);
			}

			//foreach (var u in _users)
			//{
			//    await u.SendAsync($"{session.deviceid} left.");
			//}

			//_logger.LogInformation($"{session.deviceid} left.");

			await Task.CompletedTask;
		}


		public async ValueTask getConnectList(UserSession mySession)
		{
			var arr = new ArrayList();

			foreach (var u in _users)
			{
				arr.Add(u.deviceid);
			}

			var res = new
			{
				res = "connectList",
				cnt = _users.Count,
				arr,
				rsvcnt = _reserves.Count
			};

			await mySession.SendAsync(JsonSerializer.Serialize(res));
		}

		public UserSession getSessionByDeviceid(string deviceid)
		{
			foreach (var u in _users)
			{
				if (u.deviceid == deviceid)
				{
					return u;
				}
			}

			return null;
		}

		public void reserveRes(UserSession session)
		{
			lock (_reserves)
			{
				_reserves.Add(session);
			}
		}

		public void reservedSend(string toid, string buf)
		{
			var arr = new HashSet<UserSession>();

			foreach (var sess in _reserves)
			{
				if (sess.reservedid == toid)
				{
					sess.SendAsync(buf);
					arr.Add(sess);
				}
				else if (string.IsNullOrEmpty(sess.reservedid))
				{
					arr.Add(sess);
				}
				else if (sess.State == SuperSocket.SessionState.Closed)
				{
					arr.Add(sess);
				}
			}

			lock (_reserves)
			{
				foreach (var sess in arr)
				{
					_reserves.Remove(sess);
				}
			}

		}

		public void renewReserved()
		{
			var arr = new HashSet<UserSession>();

			foreach (var sess in _reserves)
			{
				if (sess.State == SuperSocket.SessionState.Closed)
				{
					arr.Add(sess);
				}
			}

			lock (_reserves)
			{
				foreach (var sess in arr)
				{
					_reserves.Remove(sess);
				}
			}

		}

	}
}
