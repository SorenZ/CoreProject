using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Helpers.Linq;
using Alamut.Service;
using AutoMapper.QueryableExtensions;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ServiceContracts;

namespace Mobin.CoreProject.Service.AppServices
{
    public class LeafService : CrudService<Leaf>, ILeafService
    {
        public LeafService(IRepository<Leaf> repository) : base(repository)
        {
        }
    }
}
