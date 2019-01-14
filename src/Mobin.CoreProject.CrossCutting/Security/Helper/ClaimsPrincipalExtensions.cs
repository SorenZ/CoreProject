using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Mobin.CoreProject.CrossCutting.Security.SSOT;

namespace Mobin.CoreProject.CrossCutting.Security.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// check if user has any claim with value of provided array and type of 'permission'
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="stringedPermissions"></param>
        /// <returns></returns>
        public static bool HasPermissions(this ClaimsPrincipal principal, IEnumerable<string> stringedPermissions) =>
            principal.Claims.Any(q =>
                q.Type == AlamutClaimTypes.Permission
                && stringedPermissions.Contains(q.Value));

        /// <summary>
        /// provide current user Id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal) =>
            principal.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new NullReferenceException("there is no claim with type 'ClaimTypes.NameIdentifier'");


        public static string GetClaim(this ClaimsPrincipal principal, string claimType) =>
            principal.FindFirstValue(claimType);

        public static int GetClaimInt(this ClaimsPrincipal principal, string claimType) =>
            int.Parse(principal.FindFirstValue(claimType));

        public static bool HasPermission(this ClaimsPrincipal principal, Enum permission) =>
            principal.Claims.Any(q =>
                q.Type == AlamutClaimTypes.Permission
                && q.Value == permission.ToString());
    }
}