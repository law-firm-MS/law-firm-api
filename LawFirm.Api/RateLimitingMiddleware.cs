using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LawFirm.Api
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<
            string,
            (int Count, DateTime WindowStart)
        > _requests = new();
        private const int LIMIT = 100;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(10);

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;
            var (count, windowStart) = _requests.GetOrAdd(ip, _ => (0, now));
            if (now - windowStart > WINDOW)
            {
                count = 0;
                windowStart = now;
            }
            count++;
            _requests[ip] = (count, windowStart);
            if (count > LIMIT)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }
            await _next(context);
        }
    }
}
