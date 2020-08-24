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
	public class FetchDataFluent: ComponentBase
	{
		//[Inject]
		//ILogger<FetchDataFluent>? Logger { get; set; }

		[Inject]
		WeatherForecastService ForecastService { get; set; } = default!;

		WeatherForecast[]? _forecasts;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			var fluentBuilder = builder.Build();//logger: Logger);

			// conditional steps example
			fluentBuilder
				.H1 ("Weather forecast")
				.P("This component demonstrates fetching data from a service.")
				.If(_forecasts == null,
					fluentBuilder2 => // if true
						fluentBuilder2.P("<em>Loading...</em>"),
					fluentBuilder2 => // else
					{
						fluentBuilder2
							.TableHead()
							.HeadCell("Date")
							.HeadCell("Temp. (C)")
							.HeadCell("Temp. (F)")
							.HeadCell("Summary")
							.OpenTableBody();

							foreach (var forecast in _forecasts!) // we're only here if _forecasts != null
								fluentBuilder2
									.OpenRow()
										.Cell(forecast.Date.ToShortDateString())
										.Cell(forecast.TemperatureC)
										.Cell(forecast.TemperatureF)
										.Cell(forecast.Summary)
									.Close(); // row

							fluentBuilder2.Close(); // table body (which closes table too, via OpenTableBody's CloseHelper)
					})
				.H5("End of conditional steps example.");

			// basic table example
			fluentBuilder
				.OpenDiv()
					.Break()
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
				.Close(3) // row, table, div
				.H5("End of basic table example.");

			// auto-row table example
			fluentBuilder
				.OpenDiv()
					.Break()
					.H3("Auto-row table example:")
					.OpenAutoTable()
						.Cell("Row1, Cell1;")
						.Cell("Row1, Cell2;")
						.Cell("Row1, Cell3;")
					.NewAutoRow()
						.Cell("Row2, Cell1;")
						.Cell("Row2, Cell2;")
						.Cell("Row2, Cell3;")
					.NewAutoRow ()
						.Cell("Row3, Cell1;")
						.Cell("Row3, Cell2;")
						.Cell("Row3, Cell3;")
				.Close(2) // auto-table, div
				.H5("End of auto-row table example.");

			// looping table example
			fluentBuilder
				.OpenDiv()
					.Break()
					.H3("Looping table example:")
					.OpenTable();

					var rowNum = 1;
					foreach (var row in Enumerable.Range (1, 4))
					{
						fluentBuilder.OpenRow();

						var cellNum = 1;
						foreach (var cell in Enumerable.Range(1, 4))
						{
							// typically a key would be a meaningful cell identifier,
							// e.g. name, order number, etc., but for demonstration
							// purposes we're just generating it from row and cell numbers
							var key = $"r{rowNum}c{cellNum++}";

							fluentBuilder.Cell($"Row{row}, Cell{cell};", $"cell-{key}", key);
						}

						fluentBuilder.Close(); // row
					}

					fluentBuilder.Close(2) // table, div
				.H5("End of looping table example.");
		}

		protected override async Task OnInitializedAsync()
			=> _forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
	}
}
