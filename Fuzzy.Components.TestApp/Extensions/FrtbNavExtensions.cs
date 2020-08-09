using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;


namespace Fuzzy.Components.TestApp.Extensions
{
	/// <summary>
	/// Provides extension methods to help generate navigation content.
	/// </summary>
	public static class FrtbNavExtensions
	{
		const string _menuItemMarkup = "<span class=\"oi oi-{0}\" aria-hidden=\"true\"></span> {1}";

		/// <summary>
		/// Generates a <see cref="NavLink"/> component, adding the given href, Match, CSS class
		/// (defaulting to "nav-link") and ActiveClass attributes, if given; then adding a
		/// <c>ChildContent</c> attribute for the given markup text.
		/// </summary>
		/// <param name="fluentBuilder"></param>
		/// <param name="href"></param>
		/// <param name="markup"></param>
		/// <param name="class"></param>
		/// <param name="activeClass"></param>
		/// <returns></returns>
		public static FluentRenderTreeBuilder NavLink(this FluentRenderTreeBuilder fluentBuilder,
				string href, object markup, string? @class = null, string? activeClass = null)
			=> fluentBuilder
				.OpenComponent<NavLink>(@class ?? "nav-link")
					.MultipleAttributes(new (string, object) []
						{
							("href", href),
							("Match", href == "" ? NavLinkMatch.All : NavLinkMatch.Prefix),
							("ActiveClass", activeClass!)
						})
					.ChildContent((MarkupString) markup.ToString(), prettyPrint: true)
				.Close();

		public static FluentRenderTreeBuilder Menu(this FluentRenderTreeBuilder fluentBuilder,
				string? listClass = null, string? itemClass = null, string? navLinkClass = null,
				params (string url, string icon, string content)[] items)
		{
			fluentBuilder
				.OpenRegion() // start a new sequence scope, as this can be called from many places
					.OpenList(listClass);

					foreach (var (url, icon, content) in items)
						fluentBuilder.OpenItem(itemClass, key: content)
								.NavLink(url, string.Format(_menuItemMarkup, icon, content), navLinkClass)
							.Close();

			return fluentBuilder.Close(2); // list, region
		}
	}
}
