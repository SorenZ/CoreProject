using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using Alamut.Service;
using AutoMapper.QueryableExtensions;
using Mobin.CoreProject.Core.DTOs.Forest;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.SearchCriteria.Forest;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.ViewModels.Leaf;

namespace Mobin.CoreProject.Service.AppServices
{
    public class ForestService : CrudService<Forest>, IForestService
    {
        private readonly IRepository<Leaf> _leafRepository;

        public ForestService(IRepository<Forest> repository,
            IRepository<Leaf> leafRepository) : base(repository)
        {
            _leafRepository = leafRepository;
        }

        public IPaginated<ForestSummaryDTO> GetData(ForestGetDataSC criteria, int page = 1, int size = 10)
        {
            var model = this.Repository
                .Queryable

                .WhereIf(! string.IsNullOrWhiteSpace(criteria.Q),
                    q => q.Title.Contains(criteria.Q))

                .ProjectTo<ForestSummaryDTO>()

                .ToPaginated(new PaginatedCriteria(page, size));

            return model;
        }

        public ServiceResult UpdateLeafs(int id, ICollection<Leaf> leafs)
        {
            var newLeafs = leafs.Select(leaf =>
            {
                leaf.ForestId = id;
                return leaf;
            }).Where(q => ! string.IsNullOrWhiteSpace(q.Title));

            _leafRepository.DeleteMany(q => q.ForestId == id, false);
            _leafRepository.AddRange(newLeafs);

            return ServiceResult.Okay();
        }
    }
}
