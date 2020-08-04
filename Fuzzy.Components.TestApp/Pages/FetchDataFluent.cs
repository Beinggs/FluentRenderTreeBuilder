using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

using Fuzzy.Components.TestApp.Data;


namespace Fuzzy.Components.TestApp.Shared
{
	[Route("/fluent-fetchdata")]
	public partial class FetchDataFluent: ComponentBase
	{
		[Inject]
		ILogger<FetchDataFluent>? Logger { get; set; }

		[Inject]
		WeatherForecastService ForecastService { get; set; } = default!;

		WeatherForecast[]? _forecasts;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			var frtb = builder.Build(logger: Logger)
				.H1("Weather forecast")
				.P("This component demonstrates fetching data from a service.");

			if (_forecasts == null)
			{
				frtb.P("<em>Loading...</em>");
				return;
			}

			frtb.TableHead("table")
				.HeadCell("Date")
				.HeadCell("Temp. (C)")
				.HeadCell("Temp. (F)")
				.HeadCell("Summary");

			frtb.OpenTableBody();

			foreach (var forecast in _forecasts)
				frtb.OpenRow()
						.Cell(forecast.Date.ToShortDateString())
						.Cell(forecast.TemperatureC)
						.Cell(forecast.TemperatureF)
						.Cell(forecast.Summary)
					.Close(); // row

			frtb.CloseTableBody();

			frtb.OpenDiv()
					.P("")
					.H3("Basic table example:")
					.OpenTable()
						.OpenRow()
							.Cell("Row1, Cell1;")
							.Cell("Row1, Cell2;")
							.Cell("Row1, Cell3;")
						.NewRow()
							.Cell("Row2, Cell1;")
							.Cell("Row2, Cell2;")
							.Cell("Row2, Cell3;")
						.NewRow()
							.Cell("Row3, Cell1;")
							.Cell("Row3, Cell2;")
							.Cell("Row3, Cell3;")
						.Close() // row
					.Close() // table
				.Close(); // div

			frtb.OpenDiv()
					.P("")
					.H3("Auto-row table example:")
					.OpenAutoTable()
						.Cell("Row1, Cell1;")
						.Cell("Row1, Cell2;")
						.Cell("Row1, Cell3;")
					.NewRow()
						.Cell("Row2, Cell1;")
						.Cell("Row2, Cell2;")
						.Cell("Row2, Cell3;")
					.NewRow()
						.Cell("Row3, Cell1;")
						.Cell("Row3, Cell2;")
						.Cell("Row3, Cell3;")
					.CloseAutoTable()
				.Close(); // div

			frtb.OpenDiv()
					.P("")
					.H3("Looping table example:")
					.OpenTable();

					foreach (var row in Enumerable.Range (1, 4))
					{
						frtb.OpenRow();

						foreach (var cell in Enumerable.Range(1, 4))
							frtb.Cell($"Row{row}, Cell{cell};").SameLine();

						frtb.Close(); // row
					}

				frtb.Close() // table
				.Close(); // div
		}

		protected override async Task OnInitializedAsync()
			=> _forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
	}
}
