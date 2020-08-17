using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using Fuzzy.Components.TestApp.Shared;


namespace Fuzzy.Components.TestApp.Pages
{
	[Route("/fluent-callback")]
	public class CallbackFluent: ComponentBase
	{
		string? _message;

		protected override void BuildRenderTree(RenderTreeBuilder builder)
			=> builder.Build()
				.H1("Callback Page")
				.Break()
				.P($"Message: {_message}").NewLine(2)
				.Markup("<hr>").NewLine()
				.OpenComponent<CallbackComponent>()
					.Attribute("Name", "Bob")
					.Callback(SayHello)
				.Close();

		public void SayHello(string message)
			=> _message = message;
	}
}
