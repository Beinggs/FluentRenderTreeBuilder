using System;

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;


namespace Fuzzy.Components
{
	/// <summary>
	/// Adds the <see cref="Build(RenderTreeBuilder, bool, int, int, ILogger?)">Build</see> method
	/// to the <see cref="RenderTreeBuilder"/> class to start the fluent call chain of
	/// <see cref="FluentRenderTreeBuilder"/> methods.
	/// </summary>
	public static class RenderTreeBuilderExtensions
	{
		/// <summary>
		/// Starts the call chain of <see cref="FluentRenderTreeBuilder"/> methods using the given
		/// <see cref="RenderTreeBuilder"/>.
		/// </summary>
		/// <param name="builder">The <see cref="RenderTreeBuilder"/> to use for building the render tree.</param>
		/// <param name="prettyPrint"><c>true</c> to add whitespace (newlines and indents) around block elements.</param>
		/// <param name="initialIndent">If <paramref name="prettyPrint"/> is <c>true</c>, the initial indentation level for block elements.</param>
		/// <param name="maxPerLine">The maximum number of frames that can be generated per source code line (including pretty printing whitespace).</param>
		/// <param name="logger">The optional <see cref="ILogger{TCategoryName}"/> implementation to use for logging.</param>
		public static FluentRenderTreeBuilder Build(this RenderTreeBuilder builder,
				bool prettyPrint = true, int initialIndent = 0,
				int maxPerLine = FluentRenderTreeBuilder.MAX_FRAMES_PER_LINE,
				ILogger? logger = null)
			=> new FluentRenderTreeBuilder(builder, prettyPrint, initialIndent, maxPerLine, logger);
	}
}
