﻿using System;
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

namespace Mobin.CoreProject.Service.AppServices
{
    public class ForestService : CrudService<Forest>, IForestService
    {
        public ForestService(IRepository<Forest> repository) : base(repository)
        {
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
    }
}
