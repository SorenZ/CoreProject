using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Helper
{
    public class AppClaimsTransformer : IClaimsTransformation
    {
        private const int ClaimDuration = 10;

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

        public async Task<IList<Claim>> GetUserClaimsAsync(string userName) =>
            await _cache.GetOrCreateAsync($"UserClaims_{userName}", async entry =>
            {
                var claims = new List<Claim>();

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ClaimDuration);
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    { return claims; }
                
                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles
                    .Where(q => userRoles.Contains(q.NormalizedName))
                    .ToList();

                foreach (var role in roles)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    claims.AddRange(roleClaims);
                }

                var userClaims = await _userManager.GetClaimsAsync(user);
                claims.AddRange(userClaims);

                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); // update UserId

                return claims;
            });


        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var roleClaims = await GetUserClaimsAsync(principal.Identity.Name);

            (principal.Identity as ClaimsIdentity)?.AddClaims(roleClaims);

            return principal;
        }
    }
}
