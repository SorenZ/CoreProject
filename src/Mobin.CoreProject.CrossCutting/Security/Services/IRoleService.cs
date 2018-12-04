using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Mobin.CoreProject.CrossCutting.Security.Models;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public interface IRoleService
    {
        Task<ServiceResult<int>> CreateAsync(string name);

        Task<ServiceResult> UpdateAsync(int id, string name);

        Task<ServiceResult> DeleteAsync(int id);

        Task<ServiceResult> UpdatePermissions(int roleId, List<string> permissions);

        List<AppRole> GetAll();

        Task<AppRole> FindByIdAsync(int id);

        Task<IList<Claim>> GetClaimsAsync(AppRole role);
    }
}
