using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Alamut.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.SSOT;

namespace Mobin.CoreProject.Service.SecurityServices
{
    public class AppClaimsTransformer : IClaimsTransformation
    {
        private const int ClaimDuration = 60; // minutes
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMemoryCache _cache;

        public AppClaimsTransformer(IMemoryCache cache, 
            RoleManager<AppRole> roleManager, 
            UserManager<AppUser> userManager)
        {
            _cache = cache;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IList<Claim> GetUserClaims(string userName) =>
            _cache.GetOrCreate($"UserClaims_{userName}", entry =>
            {
                var claims = new List<Claim>();

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ClaimDuration);
                var user = _userManager.FindByNameAsync(userName).Result;
                
                var userRoles = _userManager.GetRolesAsync(user).Result;
                var roles = _roleManager.Roles
                    .Where(q => userRoles.Contains(q.NormalizedName))
                    .ToList();

                foreach (var role in roles)
                    { claims.AddRange(_roleManager.GetClaimsAsync(role).Result); }

                claims.AddRange(_userManager.GetClaimsAsync(user).Result);

                return claims;
            });


        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var roleClaims = GetUserClaims(principal.Identity.Name);

            (principal.Identity as ClaimsIdentity)?.AddClaims(roleClaims);

            return Task.FromResult(principal);
        }
    }

    
}