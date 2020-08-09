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
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
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
		/// <see cref="OpenItem(FluentRenderTreeBuilder, string?, object?, int)">OpenItem</see>
		/// and sets the item's key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="listClass">The optional CSS class name for the list.</param>
		/// <param name="listId">The optional id attribute value.</param>
		/// <param name="itemClass">The optional CSS class name for the first list item.</param>
		/// <param name="itemKey">The optional key to set for the first item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenAutoList(this FluentRenderTreeBuilder frtb,
				string? listClass = null, string? listId = null,
				string? itemClass = null, object? itemKey = null, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("ul", listClass, listId, line: line)
					.OpenItem(itemClass, itemKey, line)
				.CloseHelper(l => frtb.Close(2, line: l)); // li, ul

		/// <summary>
		/// Opens a <c>&lt;ol&gt;</c> block, adding the given id and CSS class attributes if
		/// provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the list.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenOrderedList(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("ol", @class, id, line: line);

		/// <summary>
		/// Opens an <c>&lt;ol&gt;</c> block, adding the given id and CSS class attributes if
		/// provided, and automatically opens the first list item and sets the item's key, if
		/// provided, with a call to
		/// <see cref="OpenItem(FluentRenderTreeBuilder, string?, object?, int)">OpenItem</see>.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="listClass">The optional CSS class name for the list.</param>
		/// <param name="listId">The optional id attribute value.</param>
		/// <param name="itemClass">The optional CSS class name for the first list item.</param>
		/// <param name="itemKey">The optional key to set for the first item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenAutoOrderedList(this FluentRenderTreeBuilder frtb,
				string? listClass = null, string? listId = null,
				string? itemClass = null, object? itemKey = null, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("ol", listClass, listId, line: line)
					.OpenItem(itemClass, itemKey, line)
				.CloseHelper(l => frtb.Close(2, line: l)); // li, ol

		/// <summary>
		/// Opens an <c>&lt;li&gt;</c> block, adding the given CSS class attribute, and setting
		/// the key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="key">The optional key to set for this list item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenItem(this FluentRenderTreeBuilder frtb,
				string? @class = null, object? key = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("li", @class, null, key: key, line: line);

		/// <summary>
		/// Generates an <c>&lt;li&gt;</c> block containing the given markup, adding the given CSS
		/// class attribute, and setting the key, if provided.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;td&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="key">The optional key to set for this list item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Item(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, object? key = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("li", markup, @class, key: key, line: line);

		/// <summary>
		/// Opens a new <c>&lt;li&gt;</c> block, adding the given CSS class attribute, and setting
		/// the key, if provided, after first closing the currently open item.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the new list item.</param>
		/// <param name="key">The optional key to set for this list item.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder NewItem(this FluentRenderTreeBuilder frtb,
				string? @class = null, object? key = null, [CallerLineNumber] int line = 0)
			=> frtb
				.Close(line: line) // li
				.OpenItem(@class, key, line);
	}
}
