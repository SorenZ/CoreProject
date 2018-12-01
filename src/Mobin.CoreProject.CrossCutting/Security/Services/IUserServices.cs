using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public interface IUserService
    {
        Task<ServiceResult> CreateAsync(string username);

        Task<ServiceResult> DeleteAsync(string username);

        Task<ServiceResult> UpdateRoles(int userId, List<int> roleIds);

        Task<ServiceResult> RenameUsername(string oldUsername, string newUsername);
    }
}