//using System.Collections.Generic;
//using System.Linq;
//using Alamut.Data.Linq;
//using Alamut.Data.Paging;
//using Alamut.Data.Repository;
//using Alamut.Data.Structure;
//using Alamut.Helpers.Linq;
//using Alamut.Service;
//using AutoMapper.QueryableExtensions;
//using Mobin.CoreProject.Core.DTOs.User;
//using Mobin.CoreProject.Core.Entities;
//using Mobin.CoreProject.Core.ServiceContracts;

//namespace Mobin.CoreProject.Service.AppServices
//{
//    public class UserService : CrudService<User>, IUserService
//    {
//        public UserService(IRepository<User> repository) : base(repository)
//        {

//        }

//        public IPaginated<UserSummaryDTO> GetData(int page = 1, int size = 10)
//        {
//            var model = this.Repository
//                .Queryable

//                .ProjectTo<UserSummaryDTO>()

//                .ToPaginated(new PaginatedCriteria(page, size));

//            return model;
//        }

//        public ServiceResult Delete(string userName)
//        {
//            var result = this.Repository
//                .DeleteMany(q => q.UserName == userName);

//            return result;
//        }

//        public List<IdTitle> GetAll()
//        {
//            var model = this.Repository
//                .Queryable
//                .Select(q => new IdTitle
//                {
//                    Id = q.UserName,
//                    Title = q.UserName,
//                });

//            return model.ToList();
//        }
//    }
//}
