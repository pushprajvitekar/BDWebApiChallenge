using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Auth
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetUser(this IHttpContextAccessor context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var principal = context.HttpContext.User;
            var username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
            return username;
        }
    }
}
