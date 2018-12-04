using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Identity;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Models;
using Mobin.CoreProject.CrossCutting.Security.SSOT;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceResult<int>> CreateAsync(string name)
        {
            // normalize input
            name = name.ToLower();

            var newRole = new AppRole(name);
            var result = await _roleManager.CreateAsync(newRole);
            return result.AsServiceResult(newRole.Id);
        }

        public async Task<ServiceResult> UpdateAsync(int id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            { return ServiceResult.Error($"There is no role with name {name}"); }

            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(q => q.Id == id);

            if (role == null)
            { return ServiceResult.Error($"There is no role with id {id}"); }

            var result = await _roleManager.DeleteAsync(role);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> UpdatePermissions(int roleId, List<string> permissions)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            { return ServiceResult.Error($"There is no role with id {roleId}"); }

            var currentClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in currentClaims)
            { await _roleManager.RemoveClaimAsync(role, claim); }

            foreach (var claim in permissions)
            { await _roleManager.AddClaimAsync(role, new Claim(AlamutClaimTypes.Permission, claim)); }

            return ServiceResult.Okay();
        }

        public List<AppRole> GetAll()
        {
            return _roleManager.Roles.ToList();
        }

        public Task<AppRole> FindByIdAsync(int id)
        {
            return _roleManager.FindByIdAsync(id.ToString());
        }

        public Task<IList<Claim>> GetClaimsAsync(AppRole role)
        {
            return _roleManager.GetClaimsAsync(role);
        }
    }
}