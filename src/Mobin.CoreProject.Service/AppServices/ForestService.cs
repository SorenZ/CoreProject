using System;
using System.Collections.Generic;
using System.Text;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Service;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ServiceContracts;

namespace Mobin.CoreProject.Service.AppServices
{
    public class ForestService : CrudService<Forest>, IForestService
    {
        public ForestService(IRepository<Forest> repository) : base(repository)
        {
            
        }
    }
}
