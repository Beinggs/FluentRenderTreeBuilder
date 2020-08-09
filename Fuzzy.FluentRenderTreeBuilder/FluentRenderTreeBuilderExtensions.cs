using System;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components;


namespace Fuzzy.Components
{
	/// <summary>
	/// Adds high-level methods to the <see cref="FluentRenderTreeBuilder"/> class.
	/// </summary>
	public static class FluentRenderTreeBuilderExtensions
	{
		/// <summary>
		/// Opens an element block, adding the given id and CSS class attributes and setting its
		/// key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="name">The element name, e.g. <c>ul</c>, <c>table</c>, etc.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenElement(this FluentRenderTreeBuilder frtb, string name,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement(name, prettyPrint, line)
					.SetKey(key)
					.Id(id, line)
					.Class(@class, line);

		/// <summary>
		/// Generates an element block containing the given markup, adding the given id and CSS
		/// class attributes to the element and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="name">The element name, e.g. <c>ul</c>, <c>table</c>, etc.</param>
		/// <param name="markup">The markup content to add in the block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint">
		/// <c>false</c> to prevent insertion of newline and indent whitespace before the
		/// element's opening and closing markup, even if pretty-printing is enabled (see the
		/// <see cref="FluentRenderTreeBuilder"/> overview for details on pretty-printing).
		/// <para>
		/// <c>true</c> to force insertion of newline and indent whitespace before the element's
		/// opening and closing markup and before the given markup content.
		/// </para><para>
		/// <c>null</c> to allow the default behaviour of inserting newline and indent whitespace
		/// before the element's opening and closing markup, but not before the given markup
		/// content.
		/// </para>
		/// </param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Element(this FluentRenderTreeBuilder frtb, string name,
				object markup, string? @class = null, string? id = null, object? key = null,
				bool? prettyPrint = null, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement(name, @class, id, key, prettyPrint ?? true, line)
					.Markup(markup, prettyPrint ?? false, line)
				.Close(prettyPrint ?? false, line);

		/// <summary>
		/// Generates an element block containing the given content, adding the given id and CSS
		/// class attributes to the element and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="name">The element name, e.g. <c>ul</c>, <c>table</c>, etc.</param>
		/// <param name="fragment">The <see cref="RenderFragment"/> to add.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ContentElement(this FluentRenderTreeBuilder frtb, string name,
				RenderFragment fragment, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement(name, @class, id, key, prettyPrint, line)
					.Content(fragment, prettyPrint, line)
				.Close(prettyPrint, line);

		/// <summary>
		/// Generates an element block containing a component of the given type, adding the given
		/// id and CSS class attributes to the element and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="name">The element name, e.g. <c>ul</c>, <c>table</c>, etc.</param>
		/// <param name="type">The <see cref="Type"/> of the component to add.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ComponentElement(this FluentRenderTreeBuilder frtb,
				string name, Type type, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb
				.OpenElement(name, @class, id, key, prettyPrint, line)
					.Component(type, prettyPrint: prettyPrint, line: line)
				.Close(prettyPrint, line);

		/// <summary>
		/// Generates an element block containing a component of the specified type, adding the
		/// given id and CSS class attributes to the element and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <typeparam name="TComponent">The <see cref="Type"/> of the component to add.</typeparam>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="name">The element name, e.g. <c>ul</c>, <c>table</c>, etc.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ComponentElement<TComponent>(this FluentRenderTreeBuilder frtb,
				string name, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb
				.OpenElement(name, @class, id, key, prettyPrint, line)
					.Component<TComponent>(prettyPrint: prettyPrint, line: line)
				.Close(prettyPrint, line);

		/// <summary>
		/// Opens a component block of the given type, adding the given id and CSS class
		/// attributes and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="type">The <see cref="Type"/> of the component to open.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenComponent(this FluentRenderTreeBuilder frtb, Type type,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb
				.OpenComponent(type, prettyPrint, line)
					.SetKey(key)
					.Id(id, line)
					.Class(@class, line);

		/// <summary>
		/// Opens a component block of the specified type, adding the given id and CSS class
		/// attributes and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <typeparam name="TComponent">The <see cref="Type"/> of the component to open.</typeparam>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenComponent<TComponent>(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb
				.OpenComponent<TComponent>(prettyPrint, line)
					.SetKey(key)
					.Id(id, line)
					.Class(@class, line);

		/// <summary>
		/// Generates a component block of the given type, adding the given id and CSS class
		/// attributes and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="type">The <see cref="Type"/> of the component.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Component(this FluentRenderTreeBuilder frtb, Type type,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb
				.OpenComponent(type, prettyPrint, line)
					.SetKey(key)
					.Id(id, line)
					.Class(@class, line)
				.Close(prettyPrint, line);

		/// <summary>
		/// Generates a component block of the specified type, adding the given id and CSS class
		/// attributes and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <typeparam name="TComponent">The <see cref="Type"/> of the component.</typeparam>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Component<TComponent>(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb
				.OpenComponent<TComponent>(prettyPrint, line)
					.SetKey(key)
					.Id(id, line)
					.Class(@class, line)
				.Close(prettyPrint, line);

		/// <summary>
		/// Opens a <c>&lt;div&gt;</c> block, adding the given id and CSS class attributes, and
		/// setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenDiv(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.OpenElement("div", @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;div&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the <c>&lt;div&gt;</c> and setting its key, if provided.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;div&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Div(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb.Element("div", markup, @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;div&gt;</c> block containing the given content, adding the given id
		/// and CSS class attributes to the <c>&lt;div&gt;</c> and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="fragment">The <see cref="RenderFragment"/> to add.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ContentDiv(this FluentRenderTreeBuilder frtb,
				RenderFragment fragment, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb.ContentElement("div", fragment, @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;div&gt;</c> block containing a component of the given type, adding
		/// the given id and CSS class attributes to the <c>&lt;div&gt;</c> and setting its key,
		/// if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="type">The <see cref="Type"/> of the component to add.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ComponentDiv(this FluentRenderTreeBuilder frtb,
				Type type, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb.ComponentElement("div", type, @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;div&gt;</c> block containing a component of the specified type,
		/// adding the given id and CSS class attributes to the <c>&lt;div&gt;</c> and setting
		/// its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <typeparam name="TComponent">The <see cref="Type"/> of the component to add.</typeparam>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ComponentDiv<TComponent>(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb.ComponentElement<TComponent>("div", @class, id, key, prettyPrint, line);

		/// <summary>
		/// Adds a CSS <c>class</c> attribute.
		/// </summary>
		/// <remarks>
		/// Passing in a null or empty <paramref name="name"/> results in no attribute being generated.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="name">The class name.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Class(this FluentRenderTreeBuilder frtb, string? name,
				[CallerLineNumber] int line = 0)
			=> string.IsNullOrEmpty(name) ? frtb : frtb.Attribute("class", name, line);

		/// <summary>
		/// Adds an <c>id</c> attribute.
		/// </summary>
		/// <remarks>
		/// Passing in a null or empty <paramref name="value"/> results in no attribute being generated.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="value">The id value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Id(this FluentRenderTreeBuilder frtb, string? value,
				[CallerLineNumber] int line = 0)
			=> string.IsNullOrEmpty(value) ? frtb : frtb.Attribute("id", value, line);

		/// <summary>
		/// Adds a <c>data-[name]</c> attribute.
		/// </summary>
		/// <param name="frtb"></param>
		/// <param name="name">The data value name.</param>
		/// <param name="value">The data value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Data(this FluentRenderTreeBuilder frtb, string? name,
				object value, [CallerLineNumber] int line = 0)
			=> string.IsNullOrEmpty(name) ? frtb : frtb.Attribute($"data-{name}", value, line);

		/// <summary>
		/// Generates a heading block containing the given markup, adding the given id and CSS
		/// class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="level">The heading level, e.g. <c>1</c>, <c>2</c>, <c>3</c>, etc.</param>
		/// <param name="markup">The markup content to add in the heading block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Heading(this FluentRenderTreeBuilder frtb, int level,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Element($"h{level}", markup, @class, id, line: line);

		/// <summary>
		/// Generates an <c>&lt;h1&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;h1&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder H1(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(1, markup, @class, id, line);

		/// <summary>
		/// Generates an <c>&lt;h2&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;h2&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder H2(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(2, markup, @class, id, line);

		/// <summary>
		/// Generates an <c>&lt;h3&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;h3&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder H3(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(3, markup, @class, id, line);

		/// <summary>
		/// Generates an <c>&lt;h4&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;h4&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder H4(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(4, markup, @class, id, line);

		/// <summary>
		/// Generates an <c>&lt;h5&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;h5&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder H5(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(5, markup, @class, id, line);

		/// <summary>
		/// Generates an <c>&lt;h6&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the heading, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the <c>&lt;h6&gt;</c> block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder H6(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null,
				[CallerLineNumber] int line = 0)
			=> frtb.Heading(6, markup, @class, id, line);

		/// <summary>
		/// Generates a <c>&lt;br&gt;</c> element.
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder Break(this FluentRenderTreeBuilder frtb,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb.Markup("<br />", prettyPrint, line);

		/// <summary>
		/// Opens a <c>&lt;p&gt;</c> block, adding the given id and CSS class attributes, and
		/// setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: Each call to this method must be matched with a call to
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see>.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder OpenP(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			=> frtb.OpenElement("p", @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;p&gt;</c> block containing the given markup, adding the given id
		/// and CSS class attributes to the <c>&lt;p&gt;</c> and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="markup">The markup content to add in the block.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="paragraphPrettyPrint"><c>false</c> to prevent insertion of newline and
		/// indent whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="markupPrettyPrint"><c>true</c> to insert newline and indent whitespace
		/// before the markup, as long as <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is
		/// enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder P(this FluentRenderTreeBuilder frtb,
				object markup, string? @class = null, string? id = null, object? key = null,
				bool paragraphPrettyPrint = true, bool markupPrettyPrint = false,
				[CallerLineNumber] int line = 0)
			=> frtb.Element("p", markup, @class, id, key,
					paragraphPrettyPrint && !markupPrettyPrint ? null : (bool?) paragraphPrettyPrint,
					line);

		/// <summary>
		/// Generates a <c>&lt;p&gt;</c> block containing the given content, adding the given id
		/// and CSS class attributes to the <c>&lt;p&gt;</c> and setting its key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="fragment">The <see cref="RenderFragment"/> to add.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ContentP(this FluentRenderTreeBuilder frtb,
				RenderFragment fragment, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb.ContentElement("p", fragment, @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;p&gt;</c> block containing a component of the given type, adding
		/// the given id and CSS class attributes to the <c>&lt;p&gt;</c> and setting its key,
		/// if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="type">The <see cref="Type"/> of the component to add.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ComponentP(this FluentRenderTreeBuilder frtb,
				Type type, string? @class = null, string? id = null, object? key = null,
				bool prettyPrint = true, [CallerLineNumber] int line = 0)
			=> frtb.ComponentElement("p", type, @class, id, key, prettyPrint, line);

		/// <summary>
		/// Generates a <c>&lt;p&gt;</c> block containing a component of the specified type,
		/// adding the given id and CSS class attributes to the <c>&lt;p&gt;</c> and setting its
		/// key, if provided.
		/// </summary>
		/// <remarks>
		/// Note: This block is automatically closed, so calling
		/// <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> is unnecessary.
		/// </remarks>
		/// <typeparam name="TComponent">The <see cref="Type"/> of the component to add.</typeparam>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="class">The optional CSS class name.</param>
		/// <param name="id">The optional id attribute value.</param>
		/// <param name="key">The optional key to set for this element.</param>
		/// <param name="prettyPrint"><c>false</c> to prevent insertion of newline and indent
		/// whitespace before the markup for this element, even if
		/// <see cref="FluentRenderTreeBuilder.PrettyPrinting"/> is enabled.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder ComponentP<TComponent>(this FluentRenderTreeBuilder frtb,
				string? @class = null, string? id = null, object? key = null, bool prettyPrint = true,
				[CallerLineNumber] int line = 0)
			where TComponent : IComponent
			=> frtb.ComponentElement<TComponent>("p", @class, id, key, prettyPrint, line);

		/// <summary>
		/// Calls <see cref="FluentRenderTreeBuilder.Close(bool, int)">Close</see> with the
		/// <c>prettyPrint</c> parameter set to <c>false</c>, to generate markup to close the
		/// last opened <c>Region</c>, <c>Element</c> or <c>Component</c> block without any
		/// newline or indent whitespace, even if pretty-printing is enabled (see the
		/// <see cref="FluentRenderTreeBuilder"/> overview for details on pretty-printing).
		/// </summary>
		/// <param name="frtb">The <see cref="FluentRenderTreeBuilder"/>.</param>
		/// <param name="line">The source code line number used to generate the sequence number.</param>
		public static FluentRenderTreeBuilder CloseInline(this FluentRenderTreeBuilder frtb,
				[CallerLineNumber] int line = 0)
			=> frtb.Close(prettyPrint: false, line);
	}
}
