using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace LawFirm.Api
{
    public class DistributedRateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDatabase _redis;
        private const int LIMIT = 100;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(10);

        public DistributedRateLimitingMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            var redis = ConnectionMultiplexer.Connect(config["Redis:Configuration"]);
            _redis = redis.GetDatabase();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var key = $"ratelimit:{ip}";
            var count = await _redis.StringIncrementAsync(key);
            if (count == 1)
            {
                await _redis.KeyExpireAsync(key, WINDOW);
            }
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
