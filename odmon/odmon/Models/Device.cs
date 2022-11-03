using System;

namespace odmon.Models
{
	public class Device
	{
		public string id { get; set; }
		public string productid { get; set; }
		public string name { get; set; }
		public string macaddr { get; set; }
		public string addr { get; set; }
		public string depart { get; set; }
		public string geocode { get; set; }
		public string lati { get; set; }
		public string longi { get; set; }
		public string firmware { get; set; }
		public string serverip { get; set; }
		public string serverport { get; set; }
		public string memo { get; set; }
		public string control { get; set; }
		public string status { get; set; }
		public Boolean on_nh3 { get; set; }
		public Boolean on_h2s { get; set; }
		public Boolean on_odor { get; set; }
		public Boolean on_voc { get; set; }
		public Boolean on_indol { get; set; }
		public Boolean on_temp { get; set; }
		public Boolean on_humi { get; set; }
		public Boolean on_sen1 { get; set; }
		public Boolean on_sen2 { get; set; }
		public Boolean on_sen3 { get; set; }


	}
}
