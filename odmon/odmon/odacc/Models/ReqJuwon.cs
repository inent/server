namespace odacc.Models
{
	public class ReqJuwon
	{
		public string company { get; set; }
		public string name { get; set; }
		public string deviceId { get; set; }
		public string sendDt { get; set; }
		public string type { get; set; }

		public ioclass ioStat { get; set; }

	}

	public class ioclass
    {
		public inclass input { get; set; }
		public outclass output { get; set; }
	}

	public class inclass
	{
		public double exTmp { get; set; }
		public double exHum { get; set; }
		public double inTmp { get; set; }
		public double inHum { get; set; }
		public double dirAngle { get; set; }
		public double ovpVolt { get; set; }
		public double preVolt { get; set; }
		public double odorVolt { get; set; }
		public double h2sVolt { get; set; }
		public double nh3Volt { get; set; }
		public double vocVolt { get; set; }
		public double aVelCnt { get; set; }
		public double btStart { get; set; }
		public double btSample { get; set; }
		public double btClean { get; set; }
		public double limitSt { get; set; }
		public double door1St { get; set; }
		public double door2St { get; set; }
		public double pumpSt { get; set; }

	}

	public class outclass
	{
		public double solOut1 { get; set; }
		public double solOut2 { get; set; }
		public double solOut3 { get; set; }
		public double samplePumpOut { get; set; }
		public double sensorPumpOut { get; set; }
		public double fanOut { get; set; }
		public double heatOut { get; set; }
	}

}

//{
//company: "JTRON", 
// name: "client", 
// deviceId: dc: a6: 32:7b: 24:b4, 
// sendDt: 2021 - 07 - 14 06:54:25, 
// type: ioStat, 
// ioStat:
//	{
//	input:
//		{
//		exTmp: 51.0,
//		exHum: 24.3, 
//		inTmp: 46.0,
//		inHum: 25.5,
//		dirAngle: 45.714,
//		ovpVolt: 4.066,
//		preVolt: 0.271,
//		odorVolt: 0.025,
//		h2sVolt: -0.018,
//		nh3Volt: -0.397,
//		vocVolt: -0.346,
//		aVelCnt: 0,
//		btStart: 0,
//		btSample: 0,
//		btClean: 0,
//		limitSt: 0,
//		door1St: 0,
//		door2St: 0,
//		pumpSt: 0
//		 },
//	output:
//		{
//		solOut1: 0,
//		 solOut2: 0,
//		solOut3: 0,
//		samplePumpOut: 0,
//		sensorPumpOut: 0,
//		fanOut: 0,
//		heatOut: 0
//		 }
//	}
//}
