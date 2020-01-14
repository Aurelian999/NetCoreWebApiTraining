using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Middlewares
{
    public class ApiKeyValidationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public ApiKeyValidationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var apiKey = configuration.GetValue<string>("ApiKey");
            var keyReceivedInHeader = context.Request.Headers.Where(h => h.Key == "x-api-key").FirstOrDefault().Value;

            
            if (keyReceivedInHeader != apiKey)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                using var responseBody = new MemoryStream();

                responseBody.Seek(0, SeekOrigin.Begin);
                var bodyText = new StreamReader(responseBody).ReadToEnd();

                bodyText += "Invalid API Key";

                await context.Response.WriteAsync(bodyText);
            }
            else
            {
                // proceed only if apy key is valid
                await next(context);
            }
        }
    }
}
