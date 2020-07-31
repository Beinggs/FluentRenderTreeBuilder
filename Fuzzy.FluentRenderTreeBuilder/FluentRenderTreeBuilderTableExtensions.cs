using System;
using System.Runtime.CompilerServices;


namespace Fuzzy.Components
{
	public static class FluentRenderTreeBuilderTableExtensions
	{
		public static FluentRenderTreeBuilder OpenTable(this FluentRenderTreeBuilder frtb,
				string? tableClass = null, string? tableId = null,
				string? rowClass = null, string? cellClass = null,
				bool autoRow = false, bool autoCell = false, [CallerLineNumber] int line = 0)
		{
			frtb.OpenElement("table", tableClass, tableId, line: line);

			if (autoRow)
				frtb.OpenRow(autoCell, rowClass, cellClass, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder OpenRow(this FluentRenderTreeBuilder frtb,
				bool autoCell = false, string? rowClass = null, string? cellClass = null,
				[CallerLineNumber] int line = 0)
		{
			frtb.OpenElement("tr", rowClass, line: line);

			if (autoCell)
				frtb.OpenElement("td", cellClass, line: line);

			return frtb;
		}

		public static FluentRenderTreeBuilder NewRow(this FluentRenderTreeBuilder frtb,
				bool autoCell = false, string? rowClass = null, string? cellClass = null,
				[CallerLineNumber] int line = 0)
		{
			if (autoCell)
				frtb.Close(line: line); // TD

			return frtb
				.Close(line: line) // TR
				.OpenRow(autoCell, rowClass, cellClass, line: line);
		}

		public static FluentRenderTreeBuilder Cell(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("td", markup, @class, null, line: line);

		public static FluentRenderTreeBuilder NewCell(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null,
				[CallerLineNumber] int line = 0)
			=> frtb
				.CloseInline(line) // TD
				.Cell(markup, @class, line: line);

		public static FluentRenderTreeBuilder CloseTable(this FluentRenderTreeBuilder frtb,
				bool autoRow = false, bool autoCell = false, [CallerLineNumber] int line = 0)
		{
			if (autoCell)
				frtb.Close(line: line); // TD

			if (autoRow)
				frtb.Close(line: line); // TR

			return frtb.Close(line: line); // TABLE
		}

		/// <summary>
		/// Opens a <c>&lt;table&gt;</c> element with a <c>&lt;thead&gt;</c> section.
		/// </summary>
		/// <remarks>
		/// Note: this method MUST be used with a subsequent <see cref="OpenTableBody"/> call and a
		/// matching <see cref="CloseTableBody"/> call.
		/// </remarks>
		/// <param name="frtb"></param>
		/// <param name="tableClass"></param>
		/// <param name="id"></param>
		/// <param name="rowClass"></param>
		/// <param name="cellClass"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		public static FluentRenderTreeBuilder TableHead(this FluentRenderTreeBuilder frtb,
				string? tableClass = null, string? id = null, string? rowClass = null,
				[CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement("table", tableClass, id, line: line)
				.OpenElement("thead", null, null, line: line)
				.OpenElement("tr", rowClass, null, line: line);

		public static FluentRenderTreeBuilder HeadCell(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("th", markup, @class, null, line: line);

		/// <summary>
		/// Opens a <c>&lt;tbody&gt;</c> element after first closing the existing <c>&lt;thead&gt;</c>
		/// element opened by a previous call to <see cref="TableHead"/>.
		/// </summary>
		/// <remarks>
		/// Note: This method MUST be called subsequent to a previous <see cref="TableHead"/> call.
		/// </remarks>
		/// <param name="frtb"></param>
		/// <param name="rowClass"></param>
		/// <param name="cellClass"></param>
		/// <param name="autoRow"></param>
		/// <param name="autoCell"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		public static FluentRenderTreeBuilder OpenTableBody(this FluentRenderTreeBuilder frtb,
				string? rowClass = null, string? cellClass = null, 
				bool autoRow = false, bool autoCell = false, [CallerLineNumber] int line = 0)
		{
			frtb
				.Close(line: line) // TR
				.Close(line: line); // THEAD

			frtb.OpenElement("tbody", null, null, line: line);

			if (autoRow)
				frtb.OpenRow(autoCell, rowClass, cellClass, line);

			return frtb;
		}

		/// <summary>
		/// Closes a <c>&lt;tbody&gt;</c> element previously opened by a call to <see cref="OpenTableBody"/>.
		/// </summary>
		/// <param name="frtb"></param>
		/// <param name="autoRow"></param>
		/// <param name="autoCell"></param>
		/// <param name="line"></param>
		/// <returns></returns>
		public static FluentRenderTreeBuilder CloseTableBody(this FluentRenderTreeBuilder frtb,
				bool autoRow = false, bool autoCell = false, [CallerLineNumber] int line = 0)
		{
			if (autoCell)
				frtb.Close(line: line); // TD

			if (autoRow)
				frtb.Close(line: line); // TR

			return frtb
				.Close(line: line) // TBODY
				.Close(line: line); // TABLE
		}
	}
}
