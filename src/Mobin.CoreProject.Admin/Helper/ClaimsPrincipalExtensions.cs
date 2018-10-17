using System;
using System.Security.Claims;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.Admin.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        private static readonly string SystemSupervisor = AuthorityCode.SystemSupervisor.ToString();

        public static bool HasRole(this ClaimsPrincipal principal, AuthorityCode code)
        {
            //if (principal.IsInRole(SystemSupervisor)) return true;
            //if (principal.IsInRole(code.ToString())) return true;
            if (principal.HasClaim(ClaimTypes.Role, SystemSupervisor))
                {return true;}

            if (principal.HasClaim(ClaimTypes.Role, code.ToString()))
                {return true;}

            try
            {
                var fullAccessCode = ((int)code / 1000) * 1000;
                AuthorityCode fullAccessCodeEnum = (AuthorityCode)fullAccessCode;
                return principal.HasClaim(ClaimTypes.Role, fullAccessCodeEnum.ToString());
                //var hasFullAccessCode = principal.IsInRole(fullAccessCodeEnum.ToString());
                //return hasFullAccessCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsSystemSupervisor(this ClaimsPrincipal principal) => principal.IsInRole(SystemSupervisor);

    }
}
