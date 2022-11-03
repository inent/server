using System;

namespace odmon.Models
{
	public class Monitor
	{
		public int id { get; set; }
		public string deviceid { get; set; }
		public string odor { get; set; }
		public string silution { get; set; }
		public string solidity { get; set; }
		public string h2s { get; set; }
		public string nh3 { get; set; }
		public string voc { get; set; }
		public string airt { get; set; }
		public string spd { get; set; }
		public string tmp { get; set; }
		public string hum { get; set; }
		public string status { get; set; }
		public string alert { get; set; }
		public DateTime sensingDt { get; set; }

	}
}
