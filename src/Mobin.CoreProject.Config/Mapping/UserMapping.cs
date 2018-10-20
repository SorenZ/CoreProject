using AutoMapper;
using Mobin.CoreProject.Core.DTOs.User;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ViewModels.User;

namespace Mobin.CoreProject.Config.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserCreateVM, User>();

            CreateMap<User, UserSummaryDTO>();
        }
    }
}
