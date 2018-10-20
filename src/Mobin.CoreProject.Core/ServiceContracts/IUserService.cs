using Alamut.Data.Paging;
using Alamut.Data.Service;
using Mobin.CoreProject.Core.DTOs.User;
using Mobin.CoreProject.Core.Entities;

namespace Mobin.CoreProject.Core.ServiceContracts
{
    public interface IUserService : ICrudService<User>
    {
        IPaginated<UserSummaryDTO> GetData(int page, int size);
    }
}
