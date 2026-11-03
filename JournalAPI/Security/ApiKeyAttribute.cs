using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace JournalAPI.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public const string HeaderName = "X-Api-Key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var expected = config["ApiKey"];

            if (string.IsNullOrEmpty(expected))
            {
                context.Result = new ObjectResult("API key is not configured on the server.") { StatusCode = 500 };
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue(HeaderName, out var provided)
                || !KeysMatch(provided.ToString(), expected))
            {
                context.Result = new UnauthorizedObjectResult("Missing or invalid API key.");
                return;
            }

            await next();
        }

        private static bool KeysMatch(string a, string b) =>
            CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
    }
}