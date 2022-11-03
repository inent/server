using Newtonsoft.Json;
using System;
using System.Windows.Forms;

public sealed class DataMan
{
	#region Singleton
	private static volatile DataMan instance;
	private static object syncRoot = new Object();

	private DataMan() { }

	public static DataMan Instance
	{
		get
		{
			if (instance == null)
			{
				lock (syncRoot)
				{
					if (instance == null)
						instance = new DataMan();
				}
			}

			return instance;
		}
	}

	#endregion

	public int nAppVersion = 32;

	public string addr = "192.168.0.100";
	public string apicmd = "";
	public string jsonstr = "";
	public string UserID = "id001";
	public string UserPW = "id001";

	public string adminID = "admin-device-id-";
	public string testID = "test-device-id-";
	public string deviceID = "24:6F:28:DA:E7:74";

	public string UserToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvY3ljbGVhbi5pbyIsImlhdCI6MTUzMTIxMDU0NSwibmJmIjoxNTMxMjEwNTQ1LCJleHAiOjE1MzE4MTUzNDUsImRhdGEiOnsidXNlciI6eyJpZCI6IjYwMzcifX19.KM_65O_Du-x0puNUybrUArjwg2p1Kc7EOveHhUL5oak";

	public void setServer(int No)
	{
		switch (No)
		{
			case 0:
				addr = "ws://175.208.89.113:7000/";
				break;
			case 1:
				addr = "ws://58.225.62.101:7000/";
				break;
			case 2:
				addr = "ws://192.168.0.21:7000/";
				break;
			case 3:
				addr = "ws://localhost:7000/";
				break;

		}
	}

	public void addReqItem(ListBox _list)
	{
		_list.Items.Add("connect");
		_list.Items.Add("Disconnect");

		_list.Items.Add("send_test");
		_list.Items.Add("admin_connlist");
		_list.Items.Add("---------");
		_list.Items.Add("jtron_sysinfo");
		_list.Items.Add("jtron_sample");
		_list.Items.Add("jtron_clean");
		_list.Items.Add("jtron_stop");
		_list.Items.Add("jtron_autolevel");
		_list.Items.Add("jtron_rsvsample");
		_list.Items.Add("jtron_rsvsamplet");
		_list.Items.Add("jtron_autosample");

		_list.Items.Add("---------");
		_list.Items.Add("insys_status");
		_list.Items.Add("insys_setup");
		_list.Items.Add("insys_res");

		_list.Items.Add("---------");
		_list.Items.Add("insys_moni");
		_list.Items.Add("jtron_moni");
		_list.Items.Add("jtron_res_sys");

		_list.Items.Add("---------");
		_list.Items.Add("send_dummy");


		adminID = "admin-device-id-" + getRandom("xxxx");
		testID = "test-device-id-" + getRandom("xxxx");
	}

	public void setReqItem(string _id)
	{
		dynamic data = null;
		var buftime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");

		switch (_id)
		{
			case "connect":
				NetworkMan.Instance.connectWS();
				break;

			case "Disconnect":
				NetworkMan.Instance.closeWS();
				break;

			case "admin_connlist":
				data = new
				{
					command = "connectList",
					deviceId = adminID
				};
				break;

			case "insys_status":
				data = new
				{
					command = "orderInsys",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "insys",
								name = "server",
								timestamp = buftime,
								deviceId = deviceID,
								type = "status"
							}
						)

				};
				break;
			case "insys_setup":
				data = new
				{
					command = "orderInsys",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "insys",
								name = "server",
								timestamp = buftime,
								deviceId = deviceID,
								type = "setup",
								//firmVer = "1.0",
								setup = new
								{
									insideTime = 2,
									outsideTime = 2,
									restTime = 26,
									dataInterval = 5
								},
								//network = new
								//{
								//	serverURI = ""
								//}
							}
						)

				};
				break;

			case "insys_res":
				data = new
				{
					company = "insys",
					model = "model",
					name = "sensor",
					timestamp = "yyyy-mm-dd HH:mm:ss",
					deviceId = adminID,
					firmVer = "firmVer",
					type = "statusResult",
					network = new
					{
						ipaddr = "Device IP address",
						signalStrength = "RSSI 값"
					},
					setup = new
					{
						insideTime = "내부공기 순환 시간(minutes)",
						outsideTime = "외부공기 순환 시간(minutes)",
						restTime = "휴식 시간(minutes)",
						dataInterval = "센서 데이터 전송 주기(seconds)"
					}
				};
				break;

			case "send_test":
				data = new
				{
					//command = "CON",
					system = new { sensingDt = "yyyy-MM-dd HH:mm:ss" },
					deviceId = testID
				};
				break;

			case "insys_moni":
				data = makeInsys();
				break;

			case "jtron_moni":
				data = makeJuwon();
				break;

			case "jtron_res_sys":
				data = new
				{
					company = "JTRON",
					name = "client",
					deviceId = adminID,
					sendDt = buftime,
					type = "sysInfo",
					sysInfo = new
					{
						fwVersion = "V1.0001",
						cntProcess = 0,
						reservedDateTime = "00-01-01 00:00:03",
						reservedProcess = 0,
						autoProcOdorLev = 1.23,
						autoProcess = 0,
						sampleStartTime = "2021-07-14 06:54:25",
						sampleLabTime = "06:54:25",
						sampled = 0,
						serverdn = "lomadata.com",
						serverip = "175.208.89.113",
						socket = "websocket",
						portNo = "7000"
					}
				};
				break;



			case "jtron_sysinfo":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "sysInfo"
							}
						)

				};
				break;
			case "jtron_sample":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "sample",
								sample = new
								{
									actuate = "On",
									serverdn = "lomadata.com",
									serverip = "175.208.89.113",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;
			case "jtron_clean":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "clean",
								clean = new
								{
									actuate = "On",
									serverdn = "lomadata.com",
									serverip = "175.208.89.113",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;
			case "jtron_stop":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "stop",
								stop = new
								{
									actuate = "On",
									serverdn = "lomadata.com",
									serverip = "175.208.89.113",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;
			case "jtron_autolevel":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "autoSampleLv",
								autoSampleLv = new
								{
									value = "2.5",
									serverdn = "lomadata.com",
									serverip = "192.168.0.134",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;
			case "jtron_rsvsample":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "rsvSample",
								rsvSample = new
								{
									actuate = "On",
									serverdn = "lomadata.com",
									serverip = "192.168.0.134",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;
			case "jtron_rsvsamplet":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "rsvSampleT",
								rsvSampleT = new
								{
									time = "2021-07-26 12:34:56",
									serverdn = "lomadata.com",
									serverip = "192.168.0.134",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;
			case "jtron_autosample":
				data = new
				{
					command = "orderJtron",
					deviceId = adminID,
					sendto = deviceID,
					ordermsg = toJsonStr(
							new
							{
								company = "JTRON",
								name = "server",
								deviceId = deviceID,
								sendDt = buftime,
								type = "autoSample",
								autoSample = new
								{
									actuate = "On",
									serverdn = "lomadata.com",
									serverip = "192.168.0.134",
									socket = "websocket",
									portNo = "7000"
								}
							})

				};
				break;




			case "send_dummy":
				data = "";
				break;

			default:
				break;
		}

		if (data != null)
		{
			var msg = toJsonStr(data);
			NetworkMan.Instance.sendWS(msg);
		}
	}


	private dynamic makeJuwon()
	{
		var unixTime = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss");

		var buf = new
		{
			company = "JTRON",
			name = "client",
			deviceId = "dc:a6:32:7b:24:b4",
			sendDt = unixTime,
			type = "ioStat",
			ioStat = new
			{
				input = new
				{
					exTmp = getRandom("xx.xx"),
					exHum = getRandom("xx.xx"),
					inTmp = getRandom("xx.xx"),
					inHum = getRandom("xx.xx"),
					dirAngle = getRandom("xx.xx"),
					ovpVolt = getRandom("x.xxx"),
					preVolt = getRandom("x.xxx"),
					odorVolt = getRandom("x.xxx"),
					h2sVolt = getRandom("x.xxx"),
					nh3Volt = getRandom("x.xxx"),
					vocVolt = getRandom("x.xxx"),
					aVelCnt = getRandom("1"),
					btStart = getRandom("1"),
					btSample = getRandom("1"),
					btClean = getRandom("1"),
					limitSt = getRandom("1"),
					door1St = getRandom("1"),
					door2St = getRandom("1"),
					pumpSt = getRandom("1")
				},
				output = new
				{
					solOut1 = getRandom("1"),
					solOut2 = getRandom("1"),
					solOut3 = getRandom("1"),
					samplePumpOut = getRandom("1"),
					sensorPumpOut = getRandom("1"),
					fanOut = getRandom("1"),
					heatOut = getRandom("1")
				}
			}
		};

		return buf;
	}

	private dynamic makeInsys()
	{
		var unixTime = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss");

		var buf = new
		{
			company = "insys",
			model = "INPF-001-A",
			name = "sensor",
			timestamp = unixTime,
			deviceId = "246F28DAE774",
			firmVer = "1.6",
			type = "data",
			status = new
			{
				pump = "on",
				in_valve = "on",
				out_valve = "off"
			},
			sensorData = new
			{
				h2s = getRandom("xx").ToString(),
				nh3 = getRandom("xx").ToString(),
				indol = getRandom("xx").ToString(),
				temperature = "0.0",
				humidity = "-1",
				acCurrent = getRandom("xx").ToString()
			}
		};

		return buf;
	}

	Random rand = new Random();

	private double getRandom(string req)
	{
		switch (req)
		{
			case "xx.xx": return rand.Next(100) / 100D + rand.Next(100);
			case "x.xxx": return rand.Next(1000) / 1000D + rand.Next(10);
			case "xxxx": return rand.Next(10000);
			case "xx": return rand.Next(100);
			case "1": return rand.Next(2);
		}

		// xx
		return rand.Next(100);
	}

	private string toJsonStr(dynamic buf)
	{
		return  JsonConvert.SerializeObject(buf, Formatting.Indented);


		//var options = new JsonSerializerOptions
		//{
		//	Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		//	WriteIndented = false
		//};
		//return JsonSerializer.Serialize(buf, options);
	}


}
