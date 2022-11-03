using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odmon.Models
{
	public class DevMoni
	{
		public string deviceid { get; set; }
		public string name { get; set; }
		public string company { get; set; }
		public string lati { get; set; }
		public string longi { get; set; }
		public string odor { get; set; }
		public string silution { get; set; }
		public string solidity { get; set; }
		public string h2s { get; set; }
		public string nh3 { get; set; }
		public string voc { get; set; }
		public string winddirect { get; set; }
		public string windspeed { get; set; }
		public string temperature { get; set; }
		public string humidity { get; set; }
		public string status { get; set; }
		public string alert { get; set; }
		public string sensingDt { get; set; }
	}
}
