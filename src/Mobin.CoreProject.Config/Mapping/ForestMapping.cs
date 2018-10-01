using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Mobin.CoreProject.Core.Entities;
using Mobin.CoreProject.Core.ViewModels;
using Mobin.CoreProject.Core.ViewModels.Forest;

namespace Mobin.CoreProject.Config.Mapping
{
    public class ForestMapping : Profile
    {
        public ForestMapping()
        {
            CreateMap<ForestCreateViewModel, Forest>();
        }
    }
}
