using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;


namespace Fuzzy.Components.TestApp.Shared
{
	public partial class MainLayoutFluent: LayoutComponentBase
	{
		[Inject]
		ILogger<FluentRenderTreeBuilder>? FrtbLogger { get; set; }

		const string _aboutLink = "<a href='https://docs.microsoft.com/aspnet/' target='_blank'>About</a>";

		protected override void BuildRenderTree(RenderTreeBuilder builder)
			=> builder.Build(logger: FrtbLogger)
				.Div("sidebar")
					.Component<NavMenu>()
				.Close()
				.NewLine()
				.DivId("body", "main")
					.Div("top-row px-4 auth")
						.Markup(_aboutLink, prettyPrint: true)
					.Close()
					.NewLine()
					.Div("content px-4")
						.Content(Body)
					.Close()
				.Close();
	}
}
