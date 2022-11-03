using System.ComponentModel.DataAnnotations;

namespace odmon.Models
{
	public class User
	{
		[Key]
		public string userid { get; set; }
		public string userpw { get; set; }
		public string username { get; set; }
		public string depart { get; set; }
		public string position { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string role { get; set; }
		public string geocode { get; set; }
		public string token { get; set; }
		public string pushid { get; set; }
		public bool onweb { get; set; } = false;
		public bool onmail { get; set; } = false;
		public bool onsms { get; set; } = false;
		public bool onpush { get; set; } = false;

	}
}
