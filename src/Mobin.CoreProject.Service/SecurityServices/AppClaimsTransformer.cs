using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Alamut.Data.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Mobin.CoreProject.Core.Entities;

namespace Mobin.CoreProject.Service.SecurityServices
{
    public class AppClaimsTransformer : IClaimsTransformation
    {
        private const int ClaimDuration = 1; // minutes
        private readonly IRepository<UserRole> _useRepository;
        private readonly IMemoryCache _cache;

        public AppClaimsTransformer(IRepository<UserRole> useRepository,
            IMemoryCache cache)
        {
            _useRepository = useRepository;
            _cache = cache;
        }

        public List<Claim> GetUserRoles(string userName) =>
            _cache.GetOrCreate($"UserRoles_{userName}", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ClaimDuration);

                return _useRepository.Queryable
                    .Where(q => q.UserName == userName &&
                                q.IsActive)
                    .Select(s => new Claim(ClaimTypes.Role, s.RoleName))
                    .ToList();
            });


        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var roleClaims = GetUserRoles(principal.Identity.Name);

            (principal.Identity as ClaimsIdentity)?.AddClaims(roleClaims);

            return Task.FromResult(principal);
        }
    }
}