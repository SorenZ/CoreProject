using System;
using System.Security.Claims;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.AdminAdmin.Helper
{
    public static class ClaimsPrincipalExtensions
    {

        public static bool HasPermission(this ClaimsPrincipal principal, Permissions permission)
        {
            return true;
        }

    }
}
