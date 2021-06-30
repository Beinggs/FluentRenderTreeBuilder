using System;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using Fuzzy.Components.TestApp.Shared;


namespace Fuzzy.Components.TestApp.Pages
{
	[Route ("/fluent-callback")]
	public class CallbackFluent: ComponentBase
	{
		string _message = "Message: ";
		int _messageCount = 0;
		CallbackComponent? _callbackComponent;

		protected override void BuildRenderTree (RenderTreeBuilder builder)
			=> builder.Build()
				.H1 ("Fluent Callback")
				.Break()
				.P ($"{_message}").NewLine (2)
				.Markup ("<hr>").NewLine()
				.OpenComponent<CallbackComponent>()
					.Attribute ("Name", "Bob")
					.Callback (SayHelloToBob, "SayHello") // "SayHello" is the component's attribute name for the callback
					.Ref<CallbackComponent> (value => _callbackComponent = value)
				.Close();

		public void SayHelloToBob (string message)
			=> _message = $"Message {++_messageCount}: '{message}' from component named '{_callbackComponent?.Name}'";
	}
}
