using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;

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

        public Task<ServiceResult> RenameUsername(string oldUsername, string newUsername)
        {
            throw new System.NotImplementedException();
        }
    }
}