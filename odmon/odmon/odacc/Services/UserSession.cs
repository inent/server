using SuperSocket.WebSocket.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace odacc
{
    public class UserSession : WebSocketSession
    {
        public string deviceid { get; set; }
        public string reservedid { get; set; }
        //protected override async ValueTask OnSessionConnectedAsync()
        //{
        //    await this.SendAsync("connected : " + this.SessionID);
        //}
    }
}
