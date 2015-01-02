namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class TestViewModel : IMapFrom<Test>, IHaveCustomMappings
    {
        public int ID { get; set; }

        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Начална дата")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Крайна дата")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Име на курса")]
        public string CourseName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Test, TestViewModel>()
                            .ForMember(tvm => tvm.CourseName, opt => opt.MapFrom(t => t.Course.Name));
        }
    }
}