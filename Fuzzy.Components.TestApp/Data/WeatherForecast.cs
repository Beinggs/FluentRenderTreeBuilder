using System;

namespace Fuzzy.Components.TestApp.Data
{
	public class WeatherForecast
	{
		public DateTime Date { get; set; }

		public int TemperatureC { get; set; }

		public int TemperatureF => (TemperatureC * 2 + 1) * 9 / 10 + 32; // +1 for rounding to nearest degree F

		public string Summary { get; set; } = "";
	}
}
