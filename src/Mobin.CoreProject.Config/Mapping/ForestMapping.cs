using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ViewModels;

namespace Mobin.CoreProject.Config.Mapping
{
    public class ForestMapping : Profile
    {
        public ForestMapping()
        {
            CreateMap<Forest, ForestCreateViewModel>().ReverseMap();
        }
    }
}
