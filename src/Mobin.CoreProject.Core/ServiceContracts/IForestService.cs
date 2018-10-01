using Alamut.Data.Paging;
using Alamut.Data.Service;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.SearchCriteria.Forest;

namespace Mobin.CoreProject.Core.ServiceContracts
{
    public interface IForestService : ICrudService<Forest>
    {
        IPaginated<Forest> GetData(ForestGetDataSC criteria, int page);
    }
}
