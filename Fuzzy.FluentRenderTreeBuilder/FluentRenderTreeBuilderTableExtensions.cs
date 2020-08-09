using System;
using System.Runtime.CompilerServices;


namespace Fuzzy.Components
{
	/// <summary>
	/// Adds high-level opinionated table-specific methods to the
	/// <see cref="FluentRenderTreeBuilder"/> class.
	/// </summary>
	public static class FluentRenderTreeBuilderTableExtensions
	{
		/// <summary>
		/// Opens a <c>&lt;table&gt;</c> block, adding the given id and CSS class attributes if
		/// provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the table.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenTable(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("table", @class, id, line: line);

		/// <summary>
		/// Opens a <c>&lt;table&gt;</c> block, adding the given id and CSS class attributes if
		/// provided, and automatically opens the first row with a call to
		/// <see cref="OpenRow(FluentRenderTreeBuilder, string?, int)">OpenRow</see>.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="tableClass">The optional CSS class name for the table.</param>
		/// <param name="tableId">The optional id attribute value.</param>
		/// <param name="rowClass">The optional CSS class name for the first table row.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenAutoTable(this FluentRenderTreeBuilder frtb,
				string? tableClass = null, string? tableId = null,
				string? rowClass = null, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("table", tableClass, tableId, line: line)
					.OpenRow(rowClass, line)
				.CloseHelper(l => frtb.Close(2, line: l)); // tr, table

		/// <summary>
		/// Opens a <c>&lt;tr&gt;</c> block, adding the given CSS class attribute if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the table row.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenRow(this FluentRenderTreeBuilder frtb,
				string? @class = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("tr", @class, line: line);

		/// <summary>
		/// Opens a <c>&lt;td&gt;</c> block, adding the given CSS class attribute, and setting the
		/// key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(int, bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="key">The optional key to set for this table cell.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenCell(this FluentRenderTreeBuilder frtb,
				string? @class = null, object? key = null, [CallerLineNumber] int line = 0)
			=> frtb.OpenElement("td", @class, key: key, line: line);

		/// <summary>
		/// Generates a <c>&lt;td&gt;</c> block containing the given markup, adding the given CSS
		/// class attribute, and setting the key, if provided.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;td&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="key">The optional key to set for this table cell.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Cell(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, object? key = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("td", markup, @class, key: key, line: line);

		/// <summary>
		/// Opens a new <c>&lt;tr&gt;</c> block, adding the given CSS class attribute if provided,
		/// after first closing the currently open row.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name for the new table row.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder NewRow(this FluentRenderTreeBuilder frtb,
				string? @class = null, [CallerLineNumber] int line = 0)
			=> frtb
				.Close(line: line) // tr
				.OpenRow(@class, line);

		/// <summary>
		/// Opens a <c>&lt;table&gt;</c> block with a <c>&lt;thead&gt;</c> section.
		/// </summary>
		/// <remarks>
		/// Note: this method must be used with a subsequent
		/// <see cref="OpenTableBody">OpenTableBody</see> call and a matching
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> call for the table
		/// body.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="tableClass">The optional CSS class name for the table.</param>
		/// <param name="tableId">The optional id attribute value.</param>
		/// <param name="rowClass">The optional CSS class name for the table head row.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder TableHead(this FluentRenderTreeBuilder frtb,
				string? tableClass = null, string? tableId = null, string? rowClass = null,
				[CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("table", tableClass, tableId, line: line)
					.OpenElement("thead", null, null, line: line)
						.OpenElement("tr", rowClass, null, line: line)
				.CloseHelper(l => frtb.Close(2, line: l)); // tr, thead

		/// <summary>
		/// Generates a <c>&lt;th&gt;</c> block containing the given markup, adding the given CSS
		/// class attribute if provided.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;div&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder HeadCell(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("th", markup, @class, line: line);

		/// <summary>
		/// Opens a <c>&lt;tbody&gt;</c> block after first closing the existing
		/// <c>&lt;thead&gt;</c> block opened by a previous call to
		/// <see cref="TableHead(FluentRenderTreeBuilder, string?, string?, string?, int)">TableHead</see>.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenTableBody(this FluentRenderTreeBuilder frtb,
				[CallerLineNumber] int line = 0)
			=> frtb
				.Close(line: line) // table-head
				.OpenElement("tbody", null, null, line: line)
				.CloseHelper(l => frtb.Close(2, line: l)); // tr, thead
	}
}
