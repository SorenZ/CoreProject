using AutoMapper;
using Mobin.CoreProject.Core.DTOs.Leaf;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ViewModels.Leaf;

namespace Mobin.CoreProject.Config.Mapping
{
    public class LeafMapping : Profile
    {
        public LeafMapping()
        {
            CreateMap<LeafEditVM, Leaf>();

            CreateMap<Leaf, LeafEditDTO>();
        }
    }
}
