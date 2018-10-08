using System.Collections.Generic;
using Alamut.Data.Paging;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Mobin.CoreProject.Core.DTOs.Forest;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.SearchCriteria.Forest;
using Mobin.CoreProject.Core.ViewModels.Leaf;

namespace Mobin.CoreProject.Core.ServiceContracts
{
    public interface IForestService : ICrudService<Forest>
    {
        IPaginated<ForestSummaryDTO> GetData(ForestGetDataSC criteria, int page, int size);
        ServiceResult UpdateLeafs(int id, ICollection<Leaf> leafs);
    }
}
