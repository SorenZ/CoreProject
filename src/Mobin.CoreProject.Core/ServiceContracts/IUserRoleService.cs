using Alamut.Data.Paging;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Mobin.CoreProject.Core.DTOs.User;
using Mobin.CoreProject.Core.Entities;

namespace Mobin.CoreProject.Core.ServiceContracts
{
    public interface IUserRoleService : ICrudService<UserRole>
    {
        IPaginated<UserRole> GetData(int page, int size);
        ServiceResult Delete(string userName, string roleName);
    }
}
