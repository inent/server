using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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

	public TextBox txtRes;
	public TextBox txtReq;

	private string _status = "";

	public async void requestServer(string _data)
	{
		string _res = "";

		_res = await reqHttpPost(DataMan.Instance.UserToken, _data);

		if (_status == "OK")
		{
			txtRes.Text += JValue.Parse(_res).ToString(Formatting.Indented);
		}
		else
		{
			txtRes.Text += _res;
		}
	}

	public async Task<string> reqHttpPost(string _header, string _data)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();

		using (var client = new HttpClient())
		{
			string _buf;

			try
			{
				if (_header.Length > 10)
				{
					client.DefaultRequestHeaders.Add("Authorization", _header);
				}

				var response = await client.PostAsync(
					 DataMan.Instance.addr + DataMan.Instance.apicmd,
					 new StringContent(_data, Encoding.UTF8, "application/json"));

				_buf = await response.Content.ReadAsStringAsync();
				_status = response.StatusCode.ToString();
			}
			catch (Exception ex)
			{
				_buf = ex.Message;
				_status = ex.HResult.ToString();
			}

			sw.Stop();

			txtRes.Text = sw.ElapsedMilliseconds + " ms"
				+ Environment.NewLine + _status
				+ Environment.NewLine;

			return _buf;

		}
	}

}
