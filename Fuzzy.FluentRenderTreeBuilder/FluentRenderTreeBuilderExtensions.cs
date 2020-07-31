using System;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components;


namespace Fuzzy.Components
{
	public static class FluentRenderTreeBuilderExtensions
	{
		public static FluentRenderTreeBuilder OpenElement(this FluentRenderTreeBuilder frtb, string name,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.OpenElement(name, prettyPrint, line);

			if (id != null)
				frtb.Id(id, line);

			if (@class != null)
				frtb.Class(@class, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder Element(this FluentRenderTreeBuilder frtb, string name,
				object markup, string? @class = null, string? id = null, bool? prettyPrint = null,
				[CallerLineNumber] int line = 0)
		{
			frtb.OpenElement(name, prettyPrint ?? true, line);

			if (id != null)
				frtb.Id(id, line);

			if (@class != null)
				frtb.Class(@class, line);

			return frtb
				.Markup(markup, prettyPrint ?? false, line)
				.Close(prettyPrint ?? false, line);
		}

		public static FluentRenderTreeBuilder ContentElement(this FluentRenderTreeBuilder frtb, string name,
				RenderFragment content, string? @class = null, string? id = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
		{
			frtb.OpenElement(name, prettyPrint, line);

			if (id != null)
				frtb.Id(id, line);

			if (@class != null)
				frtb.Class(@class, line);

			return frtb
				.Content(content, prettyPrint, line)
				.Close(prettyPrint, line);
		}

		public static FluentRenderTreeBuilder ComponentElement(this FluentRenderTreeBuilder frtb,
				string name, Type type, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement(name, @class, id, prettyPrint, line: line)
				.Component(type, prettyPrint, line)
				.Close(prettyPrint, line);

		public static FluentRenderTreeBuilder ComponentElement<TComponent>(this FluentRenderTreeBuilder frtb,
				string name, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb
				.OpenElement(name, @class, id, prettyPrint, line: line)
				.Component<TComponent>(prettyPrint, line)
				.Close(prettyPrint, line);

		public static FluentRenderTreeBuilder Component(this FluentRenderTreeBuilder frtb, Type type,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenComponent(type, prettyPrint, line)
				.Close(prettyPrint, line);

		public static FluentRenderTreeBuilder Component<TComponent>(this FluentRenderTreeBuilder frtb,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb
				.OpenComponent<TComponent>(prettyPrint, line)
				.Close(prettyPrint, line);

		public static FluentRenderTreeBuilder OpenDiv(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.OpenElement("div", @class, id, prettyPrint, line: line);

		public static FluentRenderTreeBuilder Div(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("div", markup, @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder ContentDiv(this FluentRenderTreeBuilder frtb,
				RenderFragment content, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.ContentElement("div", content, @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder ComponentDiv(this FluentRenderTreeBuilder frtb,
				Type type, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.ComponentElement("div", type, @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder ComponentDiv<TComponent>(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb.ComponentElement<TComponent>("div", @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder Class(this FluentRenderTreeBuilder frtb, string value,
				[CallerLineNumber] int line = 0)
			=> frtb.Attribute("class", value, line);

		public static FluentRenderTreeBuilder Data(this FluentRenderTreeBuilder frtb, string name,
				object value, [CallerLineNumber] int line = 0)
			=> frtb.Attribute($"data-{name}", value, line);

		public static FluentRenderTreeBuilder Heading(this FluentRenderTreeBuilder frtb, int level,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element($"h{level}", markup, @class, id, line: line);

		public static FluentRenderTreeBuilder H1(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(1, markup, @class, id, line);

		public static FluentRenderTreeBuilder H2(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(2, markup, @class, id, line);

		public static FluentRenderTreeBuilder H3(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(3, markup, @class, id, line);

		public static FluentRenderTreeBuilder H4(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(4, markup, @class, id, line);

		public static FluentRenderTreeBuilder H5(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(5, markup, @class, id, line);

		public static FluentRenderTreeBuilder H6(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(6, markup, @class, id, line);

		public static FluentRenderTreeBuilder OpenP(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.OpenElement("p", @class, id, prettyPrint, line: line);

		public static FluentRenderTreeBuilder P(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("p", markup, @class, id, line: line);

		public static FluentRenderTreeBuilder ContentP(this FluentRenderTreeBuilder frtb,
				RenderFragment content, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.ContentElement("p", content, @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder ComponentP(this FluentRenderTreeBuilder frtb,
				Type type, string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.ComponentElement("p", type, @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder ComponentP<TComponent>(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb.ComponentElement<TComponent>("p", @class, id, prettyPrint, line);

		public static FluentRenderTreeBuilder CloseInline(this FluentRenderTreeBuilder frtb,
				[CallerLineNumber] int line = 0)
			=> frtb.Close(prettyPrint: false, line);
	}
}
