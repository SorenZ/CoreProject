using AutoMapper;
using Mobin.CoreProject.Core.DTOs.Forest;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ViewModels.Forest;

namespace Mobin.CoreProject.Config.Mapping
{
    public class ForestMapping : Profile
    {
        public ForestMapping()
        {
            CreateMap<ForestCreateVM, Forest>();
            CreateMap<ForestEditVM, Forest>();

            CreateMap<Forest, ForestSummaryDTO>();
        }
    }
}
