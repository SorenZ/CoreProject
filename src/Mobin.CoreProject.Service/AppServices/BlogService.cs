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
    public class BlogService : CrudService<Blog>, IBlogService
    {
        public BlogService(IRepository<Blog> repository) : base(repository)
        {
        }
    }
}
