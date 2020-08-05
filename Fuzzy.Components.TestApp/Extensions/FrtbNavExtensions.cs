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
		public static FluentRenderTreeBuilder NavLink(this FluentRenderTreeBuilder frtb,
				string url, string markup, NavLinkMatch match = NavLinkMatch.All,
				string? @class = null, string? id = null)
			=> frtb.OpenComponent<NavLink>(@class ?? "nav-link", id)
					.Attribute("href", url)
					.Attribute("Match", match)
					.ChildContent((MarkupString) markup, prettyPrint: true)
					.NewLine(prettyPrint: true)
				.Close();

		public static FluentRenderTreeBuilder Menu(this FluentRenderTreeBuilder frtb,
				string? listClass = null, string? itemClass = null,
				params (string url, string icon, string content)[] items)
		{
			frtb.OpenList(listClass);

			foreach (var (url, icon, content) in items)
				frtb.OpenItem(itemClass)
					.NavLink(url, $"<span class=\"oi oi-{icon}\" aria-hidden=\"true\"></span> {content}")
					.Close();

			return frtb.Close();
		}
	}
}
