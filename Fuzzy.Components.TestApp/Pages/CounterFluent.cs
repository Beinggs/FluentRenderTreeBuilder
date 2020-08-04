using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;


namespace Fuzzy.Components.TestApp.Shared
{
	[Route("/fluent-counter")]
	public partial class CounterFluent: ComponentBase
	{
		[Inject]
		ILogger<CounterFluent>? Logger { get; set; }

		int _currentCount = 0;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
			=> builder.Build(logger: Logger)
				.H1("Fluent Counter")
				.NewLine()
				.OpenP().Content("Current count: ").Content(_currentCount).CloseInline()
				.NewLine()
				.OpenElement("button", "btn btn-primary")
					.Attribute("onclick", EventCallback.Factory.Create<MouseEventArgs>(this, IncrementCount))
					.Content("Click me").CloseInline();

		void IncrementCount()
			=> _currentCount++;
	}
}
