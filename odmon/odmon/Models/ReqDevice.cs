namespace odmon.Models
{
	public class ReqDevice : Device
	{
		public string company { get; set; }

		// insys
		public string measect { get; set; }
		public string meacycle { get; set; }
		public string flushsect { get; set; }
		public string restsect { get; set; }
		public string multiple { get; set; }
		public string ratio { get; set; }
		public string constant { get; set; }
		public string resolution { get; set; }
		public string deci { get; set; }

		// jtron
		public string rex_nh3 { get; set; }
		public string rex_h2s { get; set; }
		public string rex_odor { get; set; }
		public string rex_voc { get; set; }
		public string rex_ou { get; set; }
		public string min1 { get; set; }
		public string min2 { get; set; }
		public string min3 { get; set; }
		public string min4 { get; set; }
		public string min5 { get; set; }
		public string rsvtime { get; set; }
		public string rsvproc { get; set; }
		public string odorlev { get; set; }
		public string autoproc { get; set; }
	}
}
