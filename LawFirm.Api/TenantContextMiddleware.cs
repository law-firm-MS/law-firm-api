using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LawFirm.Api
{
    public class TenantContextMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var orgIdClaim = context.User.FindFirst("OrganizationId");
            if (orgIdClaim != null && int.TryParse(orgIdClaim.Value, out var orgId))
            {
                context.Items["OrganizationId"] = orgId;
            }
            await _next(context);
        }
    }
}
