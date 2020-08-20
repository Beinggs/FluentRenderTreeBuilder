using System;
using System.Linq;
using System.Threading.Tasks;


namespace Fuzzy.Components.TestApp.Data
{
	public class WeatherForecastService
	{
		const int _minTemp = -20;
		const int _maxTemp = 55;
		const int _numTemps = 7;

		static readonly Random _random = new Random();

		static readonly int[] _temps = new[]
		{
			_minTemp + 20, 10,   15,       20,     23,     27,     30,      35,    45,           _maxTemp
		};

		static readonly string[] _summaries = new[]
		{
			"Yikes!", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorchio!"
		};

		public async Task<WeatherForecast[]> GetForecastAsync (DateTime startDate)
		{
			await Task.Delay (1000); // simulate server round-trip

			return
				(
					from index in Enumerable.Range (1, _numTemps)
					let temp = _random.Next (_minTemp, _maxTemp)
					let tempIndex = Array.IndexOf (_temps, _temps.First (t => t > temp))
					let summary = _summaries [tempIndex]
					select new WeatherForecast
					{
						Date = startDate.AddDays (index),
						TemperatureC = temp,
						Summary = summary
					}
				).ToArray();
		}
	}
}
