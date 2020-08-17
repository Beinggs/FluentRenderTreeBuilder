#  FluentRenderTreeBuilder

The [`FluentRenderTreeBuilder`](https://github.com/Fuzzy-Work/FluentRenderTreeBuilder) library extends Blazor's built-in [`RenderTreeBuilder`](https://docs.microsoft.com/dotnet/api/microsoft.aspnetcore.components.rendering.rendertreebuilder) with a clean and fluent API for building Blazor components in pure C# code, and automatically generates source-code-line-based sequence numbers, whilst still allowing specific sequence numbers to be specified if desired.

The resulting markup code is identical to the razor compiler output, with minor whitespace differences, and optionally output can be automatically minified by disabling the built-in 'pretty printing' feature, which is enabled by default to match the behaviour of Blazor's razor page compiler.

##  Fluent API

Developers don't have to hand-write repetitive calls to the built-in `RenderTreeBuilder` as the `FluentRenderTreeBuilder` allows fluent chaining of calls.
 
Taking an example from [Chris Sainty's blog](https://chrissainty.com/building-components-via-rendertreebuilder/), this code:

```csharp
protected override void BuildRenderTree(
    RenderTreeBuilder builder)
{
  base.BuildRenderTree(builder);
  builder.OpenElement(0, "nav");
  builder.AddAttribute(1, "class", "menu");
  
  builder.OpenElement(2, "ul");
  builder.OpenElement(3, "li");
  builder.OpenComponent<NavLink>(4);
  builder.AddAttribute(5, "href", "/");
  builder.AddAttribute(6, "Match", NavLinkMatch.All);
  builder.AddAttribute(7, "ChildContent", (RenderFragment)((builder2) => {
    builder2.AddContent(8, "Home");
  }));
  builder.CloseComponent();
  builder.CloseElement();
  
  builder.OpenElement(9, "li");
  builder.OpenComponent<NavLink>(10);
  builder.AddAttribute(11, "href", "/contact");
  builder.AddAttribute(12, "ChildContent", (RenderFragment)((builder2) => {
    builder2.AddContent(13, "Contact");
  }));
  builder.CloseComponent();
  builder.CloseElement();
  builder.CloseElement();
  builder.CloseElement();
}
```

can now be written as:

```csharp
protected override void BuildRenderTree(
    RenderTreeBuilder builder)
  => builder.Build()
    .OpenElement("nav", "menu")
      .OpenAutoList()
        .OpenComponent<NavLink>()
          .Attribute("href", "/")
          .Attribute("Match", NavLinkMatch.All)
          .ChildContent("Home")
        .Close()
      .NewItem()
        .OpenComponent<NavLink>()
          .Attribute("href", "/contact")
          .Attribute("Match", NavLinkMatch.All)
          .ChildContent("Contact")
    .CloseAll();
```

There are many new convenience methods and parameters provided, such as automatically generating `id` and `class` attributes from optional parameters to `OpenElement`, as shown above with the `menu` CSS class.

See the pages and components in the test app provided in the source code [repo on GitHub](https://github.com/Fuzzy-Work/FluentRenderTreeBuilder) for more usage examples.

##  Extensibility

Extension methods can be used to add new high-level functionality, and many are already included to help with `table`, `list` and `attribute` generation. In the above code snippet the `OpenElement` method which takes optional CSS `class` and `id` attributes extends the built-in `OpenElement` method.

For example, an extension method to automatically generate [`NavLink`](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-3.1#navlink-component) content might look like this (an example if this is included in the test app in the source repo):

```csharp
public static FluentRenderTreeBuilder NavLink(
    this FluentRenderTreeBuilder fluentbuilder,
    string href, string markup,
    NavLinkMatch match = NavLinkMatch.Prefix,
    string? @class = null, string? id = null)
  => fluentbuilder
    .OpenComponent<NavLink>(@class ?? "nav-link", id)
      .Attribute("href", href)
      .Attribute("Match", match)
      .ChildContent((MarkupString) markup, prettyPrint: true)
      .NewLine(prettyPrint: true)
    .Close();
```

This now allows the above example to be written as:

```csharp
protected override void BuildRenderTree(
    RenderTreeBuilder builder)
  => builder.Build()
    .OpenElement("nav", "menu")
      .OpenAutoList()
        .NavLink("/", "Home", NavLinkMatch.All)
      .NewItem()
        .NavLink("/contact", "Contact")
    .CloseAll();
```

This makes navigation list generation possible with just a few lines of code. However, going one step further by using the `Menu()` extensibility example included in the test app in the source repo, it can be reduced even further to:

```csharp
protected override void BuildRenderTree(
    RenderTreeBuilder builder)
  => builder.Build()
    .Menu(
      ("", "home", "Home"),
      ("contacts", "people", "Counter"),
```
 
The `Menu()` extensibility example above also includes an `icon` parameter for each menu item which sets the nav item's icon from Blazor's built-in [Open Iconic](https://useiconic.com/open) icon set.

##  Source code line-based sequence numbers

When using `FluentRenderTreeBuilder` developers don't have to manually provide and maintain the sequence numbers for each call to `RenderTreeBuilder`.
 
Unlike variable-based or calculated sequence numbers, which [must not be used](https://docs.microsoft.com/aspnet/core/blazor/advanced-scenarios#sequence-numbers-relate-to-code-line-numbers-and-not-execution-order) in Blazor components, `FluentRenderTreeBuilder` makes use of the `[CallerLineNumber]` attribute to generate each tree frame's sequence number based on the calling source code line number.
