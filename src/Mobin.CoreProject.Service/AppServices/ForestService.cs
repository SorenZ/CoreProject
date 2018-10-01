using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Helpers.Linq;
using Alamut.Service;
using AutoMapper.QueryableExtensions;
using Mobin.CoreProject.Core.DTOs.Forest;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.SearchCriteria.Forest;
using Mobin.CoreProject.Core.ServiceContracts;

namespace Mobin.CoreProject.Service.AppServices
{
    public class ForestService : CrudService<Forest>, IForestService
    {
        public ForestService(IRepository<Forest> repository) : base(repository)
        {
        }

        public IPaginated<ForestSummaryDTO> GetData(ForestGetDataSC criteria, int page = 1)
        {
            var model = this.Repository
                .Queryable

                .WhereIf(! string.IsNullOrWhiteSpace(criteria.Title),
                    q => q.Title.Contains(criteria.Title))

                .ProjectTo<ForestSummaryDTO>()

                .ToPaginated(new PaginatedCriteria(page));

            return model;
        }
    }
}
