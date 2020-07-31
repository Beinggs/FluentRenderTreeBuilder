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
		ILogger<FluentRenderTreeBuilder>? FrtbLogger { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
			=> builder.Build(logger: FrtbLogger)
				.H1("Fluent Counter")
				.NewLine()
				.OpenP().Content("Current count: ").Content(currentCount).CloseInline()
				.NewLine()
				.OpenElement("button", "btn btn-primary")
					.Attribute("onclick", EventCallback.Factory.Create<MouseEventArgs>(this, IncrementCount))
					.Content("Click me").CloseInline();

		int currentCount = 0;

		void IncrementCount()
			=> currentCount++;

	}
}
