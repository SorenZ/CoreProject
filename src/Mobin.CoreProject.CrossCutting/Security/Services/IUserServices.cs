using System.Collections.Generic;
using System.Security.Claims;
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

        Task<ServiceResult> SetClaimAsync(int userId, string type, string value);
        
        Task<IList<Claim>> GetClaimsAsync(int userId);
        
        Task<string> GetClaimValue(int userId, string type);
        
        Task<ServiceResult> RemoveClaimAsync(int userId, string type, string value);
        Task<ServiceResult> RemoveClaimAsync(int userId, Claim userClaim);
    }
}