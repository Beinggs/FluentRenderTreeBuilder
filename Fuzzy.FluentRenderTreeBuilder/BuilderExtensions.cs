using System;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;


namespace Fuzzy.Components
{
	public static class BuilderExtensions
	{
		public static FluentRenderTreeBuilder Build (this RenderTreeBuilder builder,
				bool prettyPrint = true, int initialIndent = 0, int maxPerLine = 10, ILogger? logger = null)
			=> new FluentRenderTreeBuilder(builder, prettyPrint, initialIndent, maxPerLine, logger);

		public static FluentRenderTreeBuilder Element(this FluentRenderTreeBuilder frtb, string name,
				string? @class = null, bool prettyPrint = true, [CallerLineNumber] int line = 0)
		{
			frtb.Element(name, prettyPrint, line);
			return @class == null ? frtb : Class(frtb, @class, line);
		}

		public static FluentRenderTreeBuilder Div(this FluentRenderTreeBuilder frtb, string? @class = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> Element(frtb, "div", @class, prettyPrint, line);

		public static FluentRenderTreeBuilder DivId(this FluentRenderTreeBuilder frtb, string id,
				string? @class = null, bool prettyPrint = true, [CallerLineNumber] int line = 0)
		{
			Div (frtb, @class, prettyPrint, line);
			return frtb.Attribute("id", id, line);
		}

		public static FluentRenderTreeBuilder Class(this FluentRenderTreeBuilder frtb, string value,
				[CallerLineNumber] int line = 0)
			=> frtb.Attribute("class", value, line);

		public static FluentRenderTreeBuilder Data(this FluentRenderTreeBuilder frtb, string name,
				object value, [CallerLineNumber] int line = 0)
			=> frtb.Attribute($"data-{name}", value, line);

		public static FluentRenderTreeBuilder Heading(this FluentRenderTreeBuilder frtb, int level,
				string content, [CallerLineNumber] int line = 0)
			=> frtb.Markup($"<h{level}>{content}</h{level}>", prettyPrint: true, line);

		public static FluentRenderTreeBuilder H1(this FluentRenderTreeBuilder frtb, string content,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(1, content, line);

		public static FluentRenderTreeBuilder H2(this FluentRenderTreeBuilder frtb, string content,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(2, content, line);

		public static FluentRenderTreeBuilder H3(this FluentRenderTreeBuilder frtb, string content,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(3, content, line);

		public static FluentRenderTreeBuilder H4(this FluentRenderTreeBuilder frtb, string content,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(4, content, line);

		public static FluentRenderTreeBuilder H5(this FluentRenderTreeBuilder frtb, string content,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(5, content, line);

		public static FluentRenderTreeBuilder H6(this FluentRenderTreeBuilder frtb, string content,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(6, content, line);

		public static FluentRenderTreeBuilder P(this FluentRenderTreeBuilder frtb, string? content = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
		{
			frtb.Element("p", prettyPrint, line);
			return content == null ? frtb : frtb.Content(content, line: line);
		}

		public static FluentRenderTreeBuilder CloseInline(this FluentRenderTreeBuilder frtb,
				[CallerLineNumber] int line = 0)
			=> frtb.Close(prettyPrint: false, line);
	}
}
