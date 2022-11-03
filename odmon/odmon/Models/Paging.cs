using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace odmon.Models
{
	public class Paging
	{
		public int page { get; set; }
		public int sizePerPage { get; set; }
		public string sortField { get; set; }
		public string sortOrder { get; set; }
		public string search { get; set; }
		public string fromDate { get; set; }
		public string toDate { get; set; }
	}
}
