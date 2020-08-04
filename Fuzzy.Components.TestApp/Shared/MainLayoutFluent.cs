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
				.ComponentDiv<NavMenu>("sidebar")
				.NewLine()
				.OpenDiv("main", "body")
					.Div(_aboutLink, "top-row px-4")
					.ContentDiv(Body, "content px-4")
				.Close();
	}
}
