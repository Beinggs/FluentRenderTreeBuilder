using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

using Fuzzy.Components.TestApp.Data;
using System.Threading.Tasks;

namespace Fuzzy.Components.TestApp.Shared
{
	[Route("/fluent-fetchdata")]
	public partial class FetchDataFluent: ComponentBase
	{
		[Inject]
		ILogger<FluentRenderTreeBuilder>? FrtbLogger { get; set; }

		[Inject]
		WeatherForecastService ForecastService { get; set; } = default!;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			var frtb = builder.Build(logger: FrtbLogger)
				.H1("Weather forecast")
				.P("This component demonstrates fetching data from a service.");

			if (forecasts == null)
			{
				frtb.P("<em>Loading...</em>");
			}
			else
			{
				frtb.TableHead("table")
					.HeadCell("Date")
					.HeadCell("Temp. (C)")
					.HeadCell("Temp. (F)")
					.HeadCell("Summary");

				frtb.OpenTableBody();

				foreach (var forecast in forecasts)
					frtb.OpenRow()
							.Cell(forecast.Date.ToShortDateString())
							.Cell(forecast.TemperatureC)
							.Cell(forecast.TemperatureF)
							.Cell(forecast.Summary)
						.Close();

				frtb.CloseTableBody();
			}
		}

		WeatherForecast[]? forecasts;

		protected override async Task OnInitializedAsync()
			=> forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
	}
}
