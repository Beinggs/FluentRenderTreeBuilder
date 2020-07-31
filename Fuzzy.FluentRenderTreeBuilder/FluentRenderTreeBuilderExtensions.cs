using System;
using System.Runtime.CompilerServices;


namespace Fuzzy.Components
{
	public static class FluentRenderTreeBuilderExtensions
	{
		public static FluentRenderTreeBuilder Element(this FluentRenderTreeBuilder frtb, string name,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Element(name, prettyPrint, line);

			if (id != null)
				frtb.Attribute("id", id, line);

			return @class == null ? frtb : Class(frtb, @class, line);
		}

		public static FluentRenderTreeBuilder Div(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> Element(frtb, "div", @class, id, prettyPrint, line: line);

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

		public static FluentRenderTreeBuilder P(this FluentRenderTreeBuilder frtb,
				string? content = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Element("p", prettyPrint, line: line);
			return content == null ? frtb : frtb.Content(content, line: line);
		}

		public static FluentRenderTreeBuilder CloseInline(this FluentRenderTreeBuilder frtb,
				[CallerLineNumber] int line = 0)
			=> frtb.Close(prettyPrint: false, line);
	}
}
