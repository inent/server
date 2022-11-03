using System;

namespace odmon.Models
{
	public class AlertList
	{
		public int id { get; set; }
		public int alertid { get; set; }
		public string userid { get; set; }
		public string status { get; set; }
		public string type { get; set; }
		public string name { get; set; }
		public string kind { get; set; }
		public string content { get; set; }
		public string value { get; set; }
		public DateTime times { get; set; }
	}
}
