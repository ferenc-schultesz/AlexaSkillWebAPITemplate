using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;

namespace MyHomeAlexaApp.Controllers
{

	[Route("api/[controller]")]
	public class AlexaController : Controller
	{
		/// <summary>
		/// This is the entry point for the Alexa skill
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[HttpPost]
		public SkillResponse HandleResponse([FromBody]SkillRequest input)
		{
			var requestType = input.GetRequestType();

			// return a welcome message
			if (requestType == typeof(LaunchRequest))
			{
                var response = ResponseBuilder.Tell("Welcome to your awesome calculator! Currently you can only add numbers, but we are working very hard to improve.", null);
                response.Response.ShouldEndSession = false;
                return response;
			}

			// return information from an intent
			else if (requestType == typeof(IntentRequest))
			{
				// do some intent-based stuff
				var intentRequest = input.Request as IntentRequest;

				// check the name to determine what you should do
				if (intentRequest.Intent.Name.Equals("add_numbers"))
				{
					// get the slots
					var a = intentRequest.Intent.Slots["a"].Value;
                    var b = intentRequest.Intent.Slots["b"].Value;

                    float first_number;
                    float second_number;


                    if (a == null || b == null || !float.TryParse(a, out first_number) || !float.TryParse(b, out second_number))
						return ResponseBuilder.Ask("I didn't understand the number. Please try again", null);

                    var result = first_number + second_number;
					return ResponseBuilder.Tell($"That's {result}");
				}
			}

			return ResponseBuilder.Ask("I didn't understand that, please try again!", null);
		}
	}
}