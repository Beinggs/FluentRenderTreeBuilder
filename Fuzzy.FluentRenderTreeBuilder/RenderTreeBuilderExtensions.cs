using System;

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;


namespace Fuzzy.Components
{
	public static class RenderTreeBuilderExtensions
	{
		public static FluentRenderTreeBuilder Build(this RenderTreeBuilder builder,
				bool prettyPrint = true, int initialIndent = 0, int maxPerLine = 10, ILogger? logger = null)
			=> new FluentRenderTreeBuilder(builder, prettyPrint, initialIndent, maxPerLine, logger);
	}
}