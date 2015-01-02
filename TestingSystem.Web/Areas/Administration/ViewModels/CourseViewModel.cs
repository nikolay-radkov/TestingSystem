namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int ID { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Семестър")]
        public int Semester { get; set; }

        [Display(Name = "Специалност")]
        public string SpecialtyName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                         .ForMember(cvm => cvm.SpecialtyName, opt => opt.MapFrom(c => c.Specialty.Name));
        }
    }
}