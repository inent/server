namespace odacc.Models
{
    public class ReqInsys
    {
        public string company { get; set; }
        public string model { get; set; }
        public string name { get; set; }
        public string timestamp { get; set; }
        public string deviceId { get; set; }
        public string firmVer { get; set; }
        public string type { get; set; }

        public statclass status { get; set; }
        public sensorClass sensorData { get; set; }
    }

    public class statclass
    {
        public string pump { get; set; }
        public string in_valve { get; set; }
        public string out_valve { get; set; }
    }

    public class sensorClass
    {
        public string h2s { get; set; }
        public string nh3 { get; set; }
        public string odor { get; set; }
        public string voc { get; set; }
        public string indol { get; set; }
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string acCurrent { get; set; }
    }
}


//company = "insys" ,
//	 name = "sensor" ,
//	 timestamp = "2021-07-30 15:23:45" ,
//	 deviceId = "24:6F:28:DA:E7:74" ,
//	 type = "data" ,
//	 status = {
//	pump = "on" ,
//		 valve = "on"
//	},
//	 sensorData = {
//	h2s = 78 ,
//		 nh3 = 78 ,
//		 odor = -1 ,
//		 voc = -1 ,
//		 indol = -1 ,
//		 temperature = 0.0 ,
//		 humidity = 0 ,
//		 acCurrent = 75
//	},