using System.Collections.Generic;

namespace odmon.Models
{
	public class ReqChart
	{
		public List<string> deviceid { get; set; }
		public string sect { get; set; }
		public string arith { get; set; }
		public string fromDate { get; set; }
		public string toDate { get; set; }
		public int limit { get; set; }
	}
}
