using System;

using Fuzzy.Components.TestApp.Extensions;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

using Microsoft.Extensions.Logging;


namespace Fuzzy.Components.TestApp.Shared
{
	public partial class NavMenuFluent: ComponentBase
	{
		[Inject]
		ILogger<NavMenuFluent>? Logger { get; set; }

		string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

		const string _topRowClass = "top-row pl-4 navbar navbar-dark";
		const string _topLink = "<a class=\"navbar-brand\" href=\"\">Fuzzy.Components.TestApp</a>";
		const string _topContent = "<span class=\"navbar-toggler-icon\" />";
		const string _buttonClass = "navbar-toggler";
		const string _listClass = "nav flex-column";
		const string _itemClass = "nav-item px-3";

		bool _collapseNavMenu = true;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
			=> builder.Build(logger: Logger)
				.OpenDiv(_topRowClass)
					.Markup(_topLink)
					.OpenElement("button", _buttonClass)
						.OnClick(this, ToggleNavMenu)
						.Markup(_topContent, prettyPrint: true)
				.Close(2) // button, div
				.NewLine()
				.OpenElement("nav", NavMenuCssClass)
					.OnClick(this, ToggleNavMenu)
					.Menu(_listClass, _itemClass, "nav-link",
							("", "home", "Home"),
							("counter", "plus", "Counter"),
							("fluent-counter", "plus", "Fluent Counter"),
							("fetchdata", "list-rich", "Fetch data"),
							("fluent-fetchdata", "list-rich", "Fluent Fetch data"))
				.Close(); // div

		void ToggleNavMenu()
			=> _collapseNavMenu = !_collapseNavMenu;
	}
}
