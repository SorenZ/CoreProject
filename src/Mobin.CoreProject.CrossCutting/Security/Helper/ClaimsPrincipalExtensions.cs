using System.Linq;
using System.Security.Claims;
using Mobin.CoreProject.CrossCutting.Security.SSOT;

namespace Mobin.CoreProject.CrossCutting.Security.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasPermissions(this ClaimsPrincipal principal, string[] stringedPermissions) =>
            principal.Claims.Any(q =>
                q.Type == AlamutClaimTypes.Permission
                && stringedPermissions.Contains(q.Value));

        public static string GetUserId(this ClaimsPrincipal principal) => 
            principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}