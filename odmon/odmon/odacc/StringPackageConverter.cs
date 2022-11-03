using Microsoft.Extensions.Logging;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using odacc.Models;

namespace odacc
{
    class StringPackageConverter : IPackageMapper<WebSocketPackage, StringPackageInfo>
    {

        //private readonly ILogger _logger;

        //public StringPackageConverter()
        //{
        //}

        //public StringPackageConverter(ILogger<StringPackageConverter> logger)
        //{
        //    _logger = logger;
        //}

        public StringPackageInfo Map(WebSocketPackage package)
        {
            var pack = new StringPackageInfo();
            //var arr = package.Message.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            //pack.Key = arr[0];
            //pack.Body = arr[1];

            pack.Key = "noneProc";
            var arrCompany = new List<string> { "JTRON", "insys" };

            try
            {
                var buf = JsonSerializer.Deserialize<CommandBin>(package.Message);

                if (String.IsNullOrEmpty(buf.command))
                {

                    if (!String.IsNullOrEmpty(buf.company) 
                        &&  arrCompany.Contains(buf.company)
                        && !String.IsNullOrEmpty(buf.deviceId) )
                    {
                            pack.Key = "accumProc";
                    }

                }
                else
                {
                    pack.Key = buf.command;
                }
            }
            catch
            {
            }

            pack.Body = package.Message;

            return pack;
        }
    }
}
