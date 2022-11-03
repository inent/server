using System;

namespace odmon.Models
{
	public class DevChart
	{
		public string deviceid { get; set; }
		public string nh3 { get; set; }
		public string h2s { get; set; }
		public string odor { get; set; }
		public string voc { get; set; }
		public DateTime sensingDt { get; set; }
	}
}
