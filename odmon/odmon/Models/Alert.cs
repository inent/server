using System.ComponentModel.DataAnnotations;

namespace odmon.Models
{
	public class Alert
	{
		[Key]
		public int id { get; set; }
		public string part { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string warn { get; set; }
		public string err { get; set; }
		public string setup { get; set; }
	}
}
