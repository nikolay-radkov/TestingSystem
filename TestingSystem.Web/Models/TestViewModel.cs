using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Models
{
    public class TestViewModel : IMapFrom<Test>, IHaveCustomMappings
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CourseName { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Test, TestViewModel>()
               .ForMember(tvm => tvm.CourseName, opt => opt.MapFrom(t => t.Course.Name))
               .ReverseMap();
        }
    }
}