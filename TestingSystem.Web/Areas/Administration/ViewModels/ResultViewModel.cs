using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    public class ResultViewModel : IMapFrom<Result>, IHaveCustomMappings
    {
        public double Points { get; set; }

        public string StudentFullName { get; set; }

        public string TestName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Result, ResultViewModel>()
                .ForMember(rvm => rvm.StudentFullName, opt => opt.MapFrom(r => r.Student.FullName))
                .ForMember(rvm => rvm.TestName, opt => opt.MapFrom(r => r.Test.Name));
        }
    }
}