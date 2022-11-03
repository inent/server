using System;
using System.Drawing;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public sealed class NetworkMan
{
    #region Singleton
    private static volatile NetworkMan instance;
    private static object syncRoot = new Object();

    private NetworkMan() { }

    public static NetworkMan Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new NetworkMan();
                }
            }

            return instance;
        }
    }

    #endregion

    public RichTextBox txtLog;

    //private string _status = "";

    //public async void requestServer(string _data)
    //{
    //	string _res = "";

    //	_res = await reqHttpPost(DataMan.Instance.UserToken, _data);

    //	if (_status == "OK")
    //	{
    //		//txtRes.Text += JValue.Parse(_res).ToString(Formatting.Indented);
    //	}
    //	else
    //	{
    //		txtRes.Text += _res;
    //	}
    //}

    //public async Task<string> reqHttpPost(string _header, string _data)
    //{
    //	Stopwatch sw = new Stopwatch();
    //	sw.Start();

    //	using (var client = new HttpClient())
    //	{
    //		string _buf;

    //		try
    //		{
    //			if (_header.Length > 10)
    //			{
    //				client.DefaultRequestHeaders.Add("Authorization", _header);
    //			}

    //			var response = await client.PostAsync(
    //				 DataMan.Instance.addr + DataMan.Instance.apicmd,
    //				 new StringContent(_data, Encoding.UTF8, "application/json"));

    //			_buf = await response.Content.ReadAsStringAsync();
    //			_status = response.StatusCode.ToString();
    //		}
    //		catch (Exception ex)
    //		{
    //			_buf = ex.Message;
    //			_status = ex.HResult.ToString();
    //		}

    //		sw.Stop();

    //		txtRes.Text = sw.ElapsedMilliseconds + " ms"
    //			+ Environment.NewLine + _status
    //			+ Environment.NewLine;

    //		return _buf;

    //	}
    //}

    ClientWebSocket client;

    public void connectWS()
    {
        client = new ClientWebSocket();

        client.ConnectAsync(new Uri(DataMan.Instance.addr), CancellationToken.None).Wait();

        var receiving = Receiving(client);
        Task.WhenAll(receiving);

        writeLog("req", "connected");
    }

    public void sendWS(string message)
    {

        var bytes = Encoding.UTF8.GetBytes(message);

        client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None).Wait();

        writeLog("req", message);
    }

    public void closeWS()
    {
        client.Abort();
        writeLog("req", "disconnected");
    }



    public void receiveWS(string msg)
    {
        writeLog("res", msg);
    }

    private async Task Receiving(ClientWebSocket client)
    {
        var buffer = new byte[1024 * 4];

        while (true)
        {
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)

                writeLog("res", Encoding.UTF8.GetString(buffer, 0, result.Count));

            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                break;
            }
        }
    }


    private void writeLog(string category, string msg)
    {
        txtLog.AppendText(DateTime.Now.ToShortTimeString());

        switch (category)
        {
            case "req":
                txtLog.SelectionColor = Color.Blue;
                break;
            case "res":
                txtLog.SelectionColor = Color.Red;
                break;
        }

        txtLog.AppendText(String.Format(" {0} {1}", category, msg));
        txtLog.AppendText(Environment.NewLine);

        txtLog.ScrollToCaret();

    }

}
