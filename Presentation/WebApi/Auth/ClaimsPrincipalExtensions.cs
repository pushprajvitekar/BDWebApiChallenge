using System.Security.Claims;

namespace WebApi.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var id = -1;
            var claim = claimsPrincipal;
            if (claim != null)
            {
                var claimid = Convert.ToInt32(claim.FindFirstValue(ClaimTypes.NameIdentifier));
                id = claimid > 0 ? claimid : id;
            }
            return id;
        }
    }
}
