using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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


	public async Task<string> reqHttpPost(string _apicmd, string _data)
	{
		//Stopwatch sw = new Stopwatch();
		//sw.Start();

		using (var client = new HttpClient())
		{
			string _status = "";
			string _result = "";

			try
			{
				var response = await client.PostAsync(
					 //"http://localhost:48769/" + _apicmd,
					 "http://localhost:5000/" + _apicmd,
					 new StringContent(_data, Encoding.UTF8, "application/json"));

				_result = await response.Content.ReadAsStringAsync();
				_status = response.StatusCode.ToString();
			}
			catch (Exception ex)
			{
				_result = ex.Message;
				_status = ex.HResult.ToString();
			}

			//sw.Stop();

			return $"{_status}|{_result}";

		}
	}

}
