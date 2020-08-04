using System;
using System.Runtime.CompilerServices;


namespace Fuzzy.Components
{
	/// <summary>
	/// Adds high-level opinionated list-specific methods to the
	/// <see cref="FluentRenderTreeBuilder"/> class.
	/// </summary>
	public static class FluentRenderTreeBuilderListExtensions
	{
		/// <summary>
		/// Opens a <c>&lt;ul&gt;</c> block, adding the given id and CSS class attributes if
		/// provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the list.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenList(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("ul", @class, id, line: line);

		/// <summary>
		/// Opens a <c>&lt;ul&gt;</c> block, adding the given id and CSS class attributes if
		/// provided, and automatically opens the first list item with a call to
		/// <see cref="OpenItem(FluentRenderTreeBuilder, string?, int)">OpenItem</see>.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="listClass">The optional CSS class name for the list.</param>
		/// <param name="listId">The optional id attribute value.</param>
		/// <param name="itemClass">The optional CSS class name for the first list item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenAutoList(this FluentRenderTreeBuilder frtb,
				string? listClass = null, string? listId = null,
				string? itemClass = null, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("ul", listClass, listId, line: line)
				.OpenItem(itemClass, line);


		/// <summary>
		/// Opens a <c>&lt;ol&gt;</c> block, adding the given id and CSS class attributes if
		/// provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the list.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenOrderedList(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("ol", @class, id, line: line);

		/// <summary>
		/// Opens a <c>&lt;ol&gt;</c> block, adding the given id and CSS class attributes if
		/// provided, and automatically opens the first list item with a call to
		/// <see cref="OpenItem(FluentRenderTreeBuilder, string?, int)">OpenItem</see>.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="CloseAutoList">CloseAutoList</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="listClass">The optional CSS class name for the list.</param>
		/// <param name="listId">The optional id attribute value.</param>
		/// <param name="itemClass">The optional CSS class name for the first list item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenAutoOrderedList(this FluentRenderTreeBuilder frtb,
				string? listClass = null, string? listId = null,
				string? itemClass = null, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("ol", listClass, listId, line: line)
				.OpenItem(itemClass, line);

		/// <summary>
		/// Opens an <c>&lt;li&gt;</c> block, adding the given CSS class attribute if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenItem(this FluentRenderTreeBuilder frtb,
				string? @class = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("li", @class, line: line);

		/// <summary>
		/// Generates an <c>&lt;li&gt;</c> block containing the given markup, adding the given CSS
		/// class attribute if provided.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;td&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Item(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("li", markup, @class, null, line: line);

		/// <summary>
		/// Opens a new <c>&lt;li&gt;</c> block, adding the given CSS class attribute if provided,
		/// after first closing the currently open item.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the new list item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder NewItem(this FluentRenderTreeBuilder frtb,
				string? @class = null, [CallerLineNumber] int line = 0)
			=> frtb
				.Close(line: line) // LI
				.OpenItem(@class, line);

		/// <summary>
		/// Closes the currently open list item and the currently open list.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder CloseAutoList(this FluentRenderTreeBuilder frtb,
				[CallerLineNumber] int line = 0)
			=> frtb
				.Close(line: line) // LI
				.Close(line: line); // UL/OL
	}
}
