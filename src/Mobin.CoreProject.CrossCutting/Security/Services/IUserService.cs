using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public interface IUserService
    {
        /// <summary>
        /// for windows authentication app
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<ServiceResult<AppUser>> CreateDomainUserAsync(string username);

        /// <summary>
        /// for form authentication app
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="emailConformationCallbackUrl"></param>
        /// <returns></returns>
        Task<ServiceResult<AppUser>> CreatePublicUserAsync(string username, string password, 
            string email = null, string emailConformationCallbackUrl = null);

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
        Task<List<string>> GetClaimValues(int userId, object type);
        
        Task<ServiceResult> RemoveClaimAsync(int userId, string type, string value);
        Task<ServiceResult> RemoveClaimAsync(int userId, Claim userClaim);
        Task<ServiceResult> UpdateClaims(int id, List<KeyValuePair<string, string>> claims);
    }
}