using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	public string UserID = "id000";
	public string UserPW = "id000";

	public string UserToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczpcL1wvY3ljbGVhbi5pbyIsImlhdCI6MTUzMTIxMDU0NSwibmJmIjoxNTMxMjEwNTQ1LCJleHAiOjE1MzE4MTUzNDUsImRhdGEiOnsidXNlciI6eyJpZCI6IjYwMzcifX19.KM_65O_Du-x0puNUybrUArjwg2p1Kc7EOveHhUL5oak";

	public void setServer(int No)
	{
		switch (No)
		{
			case 0:
				addr = "http://175.208.89.113:8800/";
				break;
			case 1:
				addr = "http://58.225.62.101:8800/";
				break;
			case 2:
				addr = "http://192.168.0.21/";
				break;
			case 3:
				addr = "http://localhost:48769/";
				break;
			case 4:
				addr = "https://rest.surem.com/";
				//addr = "https://api.surem.com/";
				break;

		}
	}

	public void addReqItem(ListBox _list)
	{
		_list.Items.Add("SureM_SMS");

		_list.Items.Add("---User");

		_list.Items.Add("User_Create");
		_list.Items.Add("User_Confirm");
		_list.Items.Add("User_List");
		_list.Items.Add("User_Info");
		_list.Items.Add("User_Update");
		_list.Items.Add("User_Remove");

		_list.Items.Add("Set_Role");
		_list.Items.Add("Set_GeoCode");
		_list.Items.Add("getGeoCode");
		_list.Items.Add("New_Password");
		_list.Items.Add("Login");
		_list.Items.Add("Logout");

		_list.Items.Add("---Odacc");
		_list.Items.Add("AccumJuwon");
		_list.Items.Add("AccumInsys");

		_list.Items.Add("---Test");
		_list.Items.Add("Test_setAlert");
		_list.Items.Add("CheckFormula");
		_list.Items.Add("getenv");

		_list.Items.Add("---Menu");
		_list.Items.Add("Monitoring");
		_list.Items.Add("Products");
		_list.Items.Add("ProductInfo");
		_list.Items.Add("DeviceAllList");
		_list.Items.Add("BoundsDevice");
		_list.Items.Add("BoundsChart");
		_list.Items.Add("AlertUsage");
		_list.Items.Add("Chart");
		_list.Items.Add("GeoCode");
		_list.Items.Add("users4geo");
		_list.Items.Add("devices4geo");
		_list.Items.Add("curr_cond");

		_list.Items.Add("---Alert");

		_list.Items.Add("Alert_List");
		_list.Items.Add("Alert_Info");
		_list.Items.Add("Alert_Create");
		_list.Items.Add("Alert_Update");
		_list.Items.Add("Alert_Remove");

		_list.Items.Add("Alert_History");
		_list.Items.Add("Alert_todoList");

		_list.Items.Add("Alert_userlist");
		_list.Items.Add("Alert_useradd");
		_list.Items.Add("Alert_userdel");

		_list.Items.Add("---Product");

		_list.Items.Add("Prod_Create");
		_list.Items.Add("Prod_List");
		_list.Items.Add("Prod_Update");
		_list.Items.Add("Prod_Info");
		_list.Items.Add("Prod_Remove");

		_list.Items.Add("---Attrib");

		_list.Items.Add("Attr_AllList");
		_list.Items.Add("Attr_List");
		_list.Items.Add("Attr_Info");
		_list.Items.Add("Attr_Create");
		_list.Items.Add("Attr_Update");
		_list.Items.Add("Attr_Remove");

		_list.Items.Add("---Device");

		_list.Items.Add("Device_AllList");
		_list.Items.Add("Device_List");
		_list.Items.Add("Device_Info");
		_list.Items.Add("Device_Create");
		_list.Items.Add("Device_Update");
		_list.Items.Add("Device_Remove");



	}

	public void setReqItem(string _id)
	{
		dynamic data = null;

		switch (_id)
		{
			case "Device_Create":
				apicmd = String.Format("api/Devices/register");
				data = new
				{
					id = "device-id-01",
					productid = "product-id-01",
					name = "장비-01",
					control = "환풍기",
					macaddr = "246F28DAE704",
					addr = "전라북도 전주시 완산구 산월1길 32-5",
					depart = "아무개",
					geocode = "1002",
					lati = "35.81166060326689",
					longi = "127.12076258525872",
					status = "on",
					memo = "진안 -1",
					firmware = "0.1",
					serverip = "175.208.89.113",
					serverport = "7000",
					measect = "2",
					meacycle = "5",
					flushsect = "2",
					restsect = "56"
				};
				break;
			case "Device_AllList":
				apicmd = String.Format("api/Devices/alllist");
				break;
			case "Device_List":
				apicmd = String.Format("api/Devices/list");
				data = new
				{
					productid = "product-id-01"
				};
				break;
			case "Device_Update":
				apicmd = String.Format("api/Devices/update");
				data = new
				{
					id = "device-id-01",
					productid = "product-id-01",
					name = "장비-01",
					control = "환풍기",
					macaddr = "246F28DAE704",
					addr = "전라북도 전주시 완산구 산월1길 32-5",
					depart = "아무개",
					geocode = "1002",
					lati = "35.81166060326689",
					longi = "127.12076258525872",
					status = "on",
					memo = "진안 -1",
					firmware = "0.1",
					serverip = "175.208.89.113",
					serverport = "7000",
					measect = "2",
					meacycle = "5",
					flushsect = "2",
					restsect = "56"
				};
				break;
			case "Device_Remove":
				apicmd = String.Format("api/Devices/remove");
				data = new
				{
					id = "device-id-01"
				};
				break;
			case "Device_Info":
				apicmd = String.Format("api/Devices/info");
				data = new
				{
					id = "device-id-01"
				};
				break;



			case "Attr_Create":
				apicmd = String.Format("api/Attribs/register");
				data = new
				{
					productid = "product-id-01",
					type = "Sensor",
					alias = "nh3",
					name = "NH3",
					onoff = "off",
					label = "암모니아",
					spec = "",
					chemiunit = "ppm",
					threshold = "30",
					min = "2.5",
					max = "50",
					elecunit = "mV",
					note = "..."
				};
				break;
			case "Attr_Update":
				apicmd = String.Format("api/Attribs/update");
				data = new
				{
					id = 1,
					productid = "product-id-01",
					type = "Sensor",
					alias = "nh3",
					name = "NH3",
					onoff = "off",
					label = "암모니아",
					spec = "",
					chemiunit = "ppm",
					threshold = "30",
					min = "2.5",
					max = "50",
					elecunit = "mV",
					note = "..."
				};
				break;
			case "Attr_AllList":
				apicmd = String.Format("api/Attribs/alllist");
				break;
			case "Attr_List":
				apicmd = String.Format("api/Attribs/list");
				data = new
				{
					productid = "product-id-01"
				};
				break;
			case "Attr_Info":
				apicmd = String.Format("api/Attribs/info");
				data = new
				{
					id = 1
				};
				break;
			case "Attr_Remove":
				apicmd = String.Format("api/Attribs/remove");
				data = new
				{
					id = 1
				};
				break;


			case "Prod_Create":
				apicmd = String.Format("api/Products/register");
				data = new
				{
					id = "product-id-01",
					name = "제품 이름 001",
					regist = DateTime.Now.ToShortDateString(),
					release = "2020-12-21",
					purpose = "용도",
					note = "비고"
				};
				break;
			case "Prod_Update":
				apicmd = String.Format("api/Products/update");
				data = new
				{
					id = "product-id-01",
					name = "제품 이름 001",
					regist = DateTime.Now.ToShortDateString(),
					release = "2020-12-21",
					purpose = "용도",
					note = "비고"
				};
				break;
			case "Prod_List":
				apicmd = String.Format("api/Products/list");
				break;
			case "Prod_Info":
				apicmd = String.Format("api/Products/info");
				data = new
				{
					id = "product-id-01"
				};
				break;
			case "Prod_Remove":
				apicmd = String.Format("api/Products/remove");
				data = new
				{
					id = "product-id-01"
				};
				break;


			case "User_Create":
				apicmd = String.Format("api/Users/register");
				data = new
				{
					userid = "id001",
					username = "이름001",
					depart = "회사001",
					position = "팀장",
					email = "firesaba@inent.co.kr",
					phone = "010-1234-5678",
				};
				break;
			case "User_Confirm":
				apicmd = String.Format("api/Users/confirm");
				data = new
				{
					userid = "id001"
				};
				break;
			case "User_List":
				apicmd = String.Format("api/Users/UserList");
				break;

			case "User_Info":
				apicmd = String.Format("api/Users/info");
				data = new
				{
					userid = UserID
				};
				break;
			case "User_Update":
				apicmd = String.Format("api/Users/update");
				data = new
				{
					userpw = "id001",
					username = "이름001",
					depart = "회사001",
					position = "팀장",
					email = "firesaba@inent.co.kr",
					phone = "010-1234-5678",
					onweb = false,
					onemail = false,
					onsms = false,
					onpush = false,
				};
				break;
			case "User_Remove":
				apicmd = String.Format("api/Users/remove");
				data = new
				{
					userid = "id001",
				};
				break;

			case "Set_Role":
				apicmd = String.Format("api/Users/setRole");
				data = new
				{
					userid = UserID,
					role = "super"
				};
				break;
			case "Set_GeoCode":
				apicmd = String.Format("api/Users/setGeoCode");
				data = new
				{
					userid = UserID,
					geocode = "1003"
				};
				break;
			case "getGeoCode":
				apicmd = String.Format("api/Users/getGeocode");
				break;
			case "New_Password":
				apicmd = String.Format("api/Users/newpassword");
				data = new
				{
					userid = UserID,
					username = "이름001",
					email = "firesaba@inent.co.kr"
				};
				break;
			case "Login":
				apicmd = String.Format("api/Users/Login");
				data = new
				{
					userid = UserID,
					userpw = UserPW,
				};
				break;
			case "Logout":
				apicmd = String.Format("api/Users/Logout");
				break;


			case "Monitoring":
				apicmd = String.Format("api/v2/Menu/Monitoring");
				break;
			case "Products":
				apicmd = String.Format("api/Menu/Products");
				break;
			case "ProductInfo":
				apicmd = String.Format("api/Menu/ProductInfo");
				data = new
				{
					id = "product-id-01"
				};
				break;
			case "DeviceAllList":
				apicmd = String.Format("api/Menu/DeviceAllList");
				break;
			case "BoundsDevice":
				apicmd = String.Format("api/v2/Menu/BoundsDevice");
				break;
			case "BoundsChart":
				apicmd = String.Format("api/v2/Menu/BoundsChart");
				break;
			case "AlertUsage":
				apicmd = String.Format("api/Menu/alertusage");
				break;
			case "Chart":
				apicmd = String.Format("api/Menu/chart");
				data = new
				{
					deviceid = new List<string>() { "dc:a6:32:e4:e6:1f", "246F28DAE6EC" },
					sect = "rt",
					arith = "sum",
					limit = 10,
					fromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"),
					toDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
				};
				break;
			case "GeoCode":
				apicmd = String.Format("api/Menu/GeoCodeList");
				break;
			case "users4geo":
				apicmd = String.Format("api/Menu/users4geo");
				break;
			case "devices4geo":
				apicmd = String.Format("api/Menu/devices4geo");
				break;
			case "curr_cond":
				apicmd = String.Format("api/Menu/currcond");
				break;









			case "CheckFormula":
				apicmd = String.Format("api/Test/CheckFormula");
				data = new
				{
					rex_nh3 = "0.2*10*0.3*POW( x ,0.4)"
				};
				break;


			case "Test_setAlert":
				apicmd = String.Format("api/Test/setAlert");
				data = new
				{
					deviceid = "device-id-06",
					odor = "-1",
					silution = "-1",
					solidity = "-1",
					h2s = "10001",
					nh3 = "-1",
					voc = "-1",
					airt = "-1",
					spd = "-1",
					tmp = "-1",
					hum = "-1",
					status = "-1",
					alert = "-1"
				};
				break;
			case "getenv":
				apicmd = String.Format("api/Test/getenv");
				break;




			case "Alert_List":
				apicmd = String.Format("api/Alerts/list");
				break;
			case "Alert_Info":
				apicmd = String.Format("api/Alerts/info");
				data = new
				{
					id = 1
				};
				break;
			case "Alert_Create":
				apicmd = String.Format("api/Alerts/register");
				data = new
				{
					part = "소속-01",
					name = "장비이름-01",
					type = "센서종류-01",
					warn = "경고수치",
					err = "위험수치",
					setup = "off"
				};
				break;
			case "Alert_Update":
				apicmd = String.Format("api/Alerts/update");
				data = new
				{
					id = 1,
					part = "소속-01",
					name = "장비이름-01",
					type = "센서종류-01",
					warn = "경고수치",
					err = "위험수치",
					setup = "off"
				};
				break;
			case "Alert_Remove":
				apicmd = String.Format("api/Alerts/remove");
				data = new
				{
					id = 1
				};
				break;
			case "Alert_History":
				apicmd = String.Format("api/Alerts/History");
				data = new
				{
					page = 1,
					sizePerPage = 3,
					sortField = "",
					sortOrder = "desc",
					search = "",
					fromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"),
					toDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
				};
				break;
			case "Alert_todoList":
				apicmd = String.Format("api/Alerts/todoList");
				break;
			case "Alert_userlist":
				apicmd = String.Format("api/Alerts/userlist");
				break;
			case "Alert_useradd":
				apicmd = String.Format("api/Alerts/useradd");
				data = new
				{
					userid = UserID,
					name = "홍길동",
					telno = "01012345678"
				};
				break;
			case "Alert_userdel":
				apicmd = String.Format("api/Alerts/userdel");
				data = new
				{
					id = 1
				};
				break;



			case "AccumJuwon":
				apicmd = String.Format("api/Odacc/AccumJuwon");
				data = makeJuwon();
				break;

			case "AccumInsys":
				apicmd = String.Format("api/Odacc/AccumInsys");
				data = makeInsys();
				break;



			case "SureM_SMS":
				apicmd = String.Format("sms/v1/json");
				data = new
				{
					usercode = "lomadata",
					deptcode = "U6-2W8-03",
					messages = new ArrayList() {
						new {
							message_id = "1001",
							to =  "01087869755"
						}
					},
					text = "님, 안녕하세요. 알림 테스트 중입니다.",
					from = "01054340460",
					//reserved_time = "209912310000"
				};
				break;

			default:
				apicmd = "";
				break;
		}

		if (data != null)
		{
			jsonstr = JsonConvert.SerializeObject(data, Formatting.Indented);
		}
		else
		{
			jsonstr = "";
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

}
