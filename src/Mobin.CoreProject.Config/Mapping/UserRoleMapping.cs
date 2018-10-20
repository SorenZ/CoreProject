using AutoMapper;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ViewModels.UserRole;

namespace Mobin.CoreProject.Config.Mapping
{
    public class UserRoleMapping : Profile
    {
        public UserRoleMapping()
        {
            CreateMap<UserRoleCreateVM, UserRole>();
        }
    }
}
