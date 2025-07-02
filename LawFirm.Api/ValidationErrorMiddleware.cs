using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LawFirm.Api
{
    public class ValidationErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Buffer the response
            var originalBody = context.Response.Body;
            using var memStream = new System.IO.MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            if (
                context.Response.StatusCode == 400
                && context.Items.ContainsKey("__ValidationProblemDetails")
            )
            {
                memStream.Seek(0, System.IO.SeekOrigin.Begin);
                var problemDetails = context.Items["__ValidationProblemDetails"];
                context.Response.ContentType = "application/json";
                var errorResponse = new
                {
                    status = 400,
                    message = "Validation failed.",
                    errors = problemDetails,
                };
                var json = JsonSerializer.Serialize(errorResponse);
                context.Response.Body = originalBody;
                await context.Response.WriteAsync(json);
            }
            else
            {
                memStream.Seek(0, System.IO.SeekOrigin.Begin);
                await memStream.CopyToAsync(originalBody);
            }
        }
    }
}
