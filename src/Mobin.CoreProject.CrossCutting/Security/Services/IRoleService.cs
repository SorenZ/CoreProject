using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alamut.Data.Structure;

namespace Mobin.CoreProject.CrossCutting.Security.Services
{
    public interface IRoleService
    {
        Task<ServiceResult> CreateAsync(string name);

        Task<ServiceResult> UpdateAsync(int id, string name);

        Task<ServiceResult> DeleteAsync(int id);

        Task<ServiceResult> UpdatePermissions(int roleId, List<string> permissions);
    }
}
