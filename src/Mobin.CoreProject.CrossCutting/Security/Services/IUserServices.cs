using System.Collections.Generic;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public interface IUserService
    {
        Task<ServiceResult> CreateAsync(string username);

        Task<ServiceResult> DeleteAsync(int userId);
        Task<ServiceResult> DeleteAsync(string username);

        Task<ServiceResult> UpdateRoles(int userId, List<int> roleIds);

        Task<ServiceResult> RenameUsername(int id, string newUsername);

        IList<AppUser> GetAll();
        Task<AppUser> FindByIdAsync(int id);
        Task<IList<string>> GetRolesAsync(AppUser user);
    }
}