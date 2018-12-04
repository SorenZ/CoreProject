using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public class UserServiceFormAuth : IUserService
    {
        public Task<ServiceResult> CreateAsync(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> UpdateRoles(int userId, List<int> roleIds)
        {
            throw new System.NotImplementedException();
        }


        public Task<ServiceResult> RenameUsername(int id, string newUsername)
        {
            throw new System.NotImplementedException();
        }

        public IList<AppUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(AppUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> SetClaimAsync(int userId, string type, string value)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetClaimValue(int userId, string type)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> RemoveClaimAsync(int userId, string type, string value)
        {
            throw new System.NotImplementedException();
        }
    }
}