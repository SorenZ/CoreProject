using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Identity;
using Mobin.CoreProject.CrossCutting.Security.Helper;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public class UserServiceWindowsAuth : IUserService
    {
        // TODO : put it in config
        private const string DomainName = "MOBINNET";
        private const string DomainEMail = "mobinnet.net";

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserServiceWindowsAuth(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResult> CreateAsync(string username)
        {
            var user = new AppUser {UserName = $"{DomainName}\\{username}", Email = $"{username}@{DomainEMail}"};
            var result = await _userManager.CreateAsync(user);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> DeleteAsync(string username)
        {
            var user = await _userManager.FindByNameAsync($"{DomainName}\\{username}");

            if (user == null)
                { return ServiceResult.Error($"There is no user with username {username}"); }
            
            var result = await _userManager.DeleteAsync(user);

            return result.AsServiceResult();
        }

        public async Task<ServiceResult> UpdateRoles(int userId, List<int> roleIds)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            { return ServiceResult.Error($"There is no user with userId {userId}"); }

            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = _roleManager.Roles
                .Where(q => roleIds.Contains(q.Id))
                .Select(s => s.Name);

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!result.Succeeded)
                { return result.AsServiceResult(); }

            result = await _userManager.AddToRolesAsync(user, roles);

            return result.AsServiceResult();
        }

        public Task<ServiceResult> RenameUsername(string oldUsername, string newUsername)
        {
            throw new System.NotImplementedException();
        }
    }
}