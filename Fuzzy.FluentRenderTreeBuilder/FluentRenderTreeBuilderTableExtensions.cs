using System;
using System.Runtime.CompilerServices;


namespace Fuzzy.Components
{
	public static class FluentRenderTreeBuilderTableExtensions
	{
		public static FluentRenderTreeBuilder Table(this FluentRenderTreeBuilder frtb,
				string? tableClass = null, string? tableId = null, bool prettyPrint = true,
				string? trClass = null, string? tdClass = null,
				[CallerLineNumber] int line = 0)
		{
			frtb.Element("table", tableClass, tableId, prettyPrint, line);
			frtb.Element("tr", trClass, null, prettyPrint, line);
			frtb.Element("td", tdClass, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder Row(this FluentRenderTreeBuilder frtb,
				string? rowClass = null, string? tdClass = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TD
			frtb.Close(prettyPrint, line); // TR
			frtb.Element("tr", rowClass, null, prettyPrint, line);
			frtb.Element("td", tdClass, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder Cell(this FluentRenderTreeBuilder frtb,
				string? @class = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TD
			frtb.Element("td", @class, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder CloseTable(this FluentRenderTreeBuilder frtb, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TD
			frtb.Close(prettyPrint, line); // TR
			frtb.Close(prettyPrint, line); // TABLE

			return frtb;
		}

		public static FluentRenderTreeBuilder TableHead(this FluentRenderTreeBuilder frtb,
				string? tableClass = null, string? id = null, bool prettyPrint = true,
				string? trClass = null, string? thClass = null,
				[CallerLineNumber] int line = 0)
		{
			frtb.Element("table", tableClass, id, prettyPrint, line);
			frtb.Element("thead", null, null, prettyPrint, line);
			frtb.Element("tr", trClass, null, prettyPrint, line);
			frtb.Element("th", thClass, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder HeadRow(this FluentRenderTreeBuilder frtb,
				string? trClass = null, string? thClass = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TH
			frtb.Close(prettyPrint, line); // TR

			frtb.Element("tr", trClass, null, prettyPrint, line);
			frtb.Element("th", thClass, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder HeadCell(this FluentRenderTreeBuilder frtb,
				string? @class = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TH
			frtb.Element("th", @class, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder TableBody(this FluentRenderTreeBuilder frtb,
				string? trClass = null, string? thClass = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TH
			frtb.Close(prettyPrint, line); // TR
			frtb.Close(prettyPrint, line); // THEAD

			frtb.Element("tbody", null, null, prettyPrint, line);
			frtb.Element("tr", trClass, null, prettyPrint, line);
			frtb.Element("td", thClass, null, prettyPrint, line);

			return frtb;
		}

		public static FluentRenderTreeBuilder CloseTableBody(this FluentRenderTreeBuilder frtb, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
		{
			frtb.Close(prettyPrint, line); // TD
			frtb.Close(prettyPrint, line); // TR
			frtb.Close(prettyPrint, line); // TBODY
			frtb.Close(prettyPrint, line); // TABLE

			return frtb;
		}
	}
}
