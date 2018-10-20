using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Service;
using AutoMapper.QueryableExtensions;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ServiceContracts;

namespace Mobin.CoreProject.Service.AppServices
{
    public class UserRoleService : CrudService<UserRole>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRole> repository) : base(repository)
        {

        }

        public IPaginated<UserRole> GetData(int page = 1, int size = 10)
        {
            var model = this.Repository
                .Queryable

                .ToPaginated(new PaginatedCriteria(page, size));

            return model;
        }
    }
}
