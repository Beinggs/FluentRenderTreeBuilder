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

		public static FluentRenderTreeBuilder Element(this FluentRenderTreeBuilder frtb, string name, string? @class = null,
				[CallerLineNumber] int line = 0)
		{
			frtb.Element(name, line);

			if (@class != null)
				Class(frtb, @class, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder Div(this FluentRenderTreeBuilder frtb, string? @class = null, [CallerLineNumber] int line = 0)
			=> Element(frtb, "div", @class, line);

		public static FluentRenderTreeBuilder DivId(this FluentRenderTreeBuilder frtb, string id, string? @class = null, [CallerLineNumber] int line = 0)
		{
			Div (frtb, @class, line);
			return frtb.Attribute("id", id, line);
		}

		public static FluentRenderTreeBuilder Class(this FluentRenderTreeBuilder frtb, string value, [CallerLineNumber] int line = 0)
			=> frtb.Attribute("class", value, line);

		public static FluentRenderTreeBuilder Data(this FluentRenderTreeBuilder frtb, string name, object value, [CallerLineNumber] int line = 0)
			=> frtb.Attribute($"data-{name}", value, line);
	}
}
