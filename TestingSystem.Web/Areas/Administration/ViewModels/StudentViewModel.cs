using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    public class StudentViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
          [Required]
         public string Id { get; set; }

        [Required]
        [Display(Name="Пълно име")]
        public string FullName { get; set; }

        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [Display(Name="ЕГН")]
        public string EGN { get; set; }

        [Display(Name="Факултетен №")]
        public long? FacultyNumber { get; set; }

        [Display(Name="Семестър")]
        public int? Semester { get; set; }

        [Display(Name="Специалност")]
        public string SpecialtyName{ get; set; }

        public string Email { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Student, StudentViewModel>()
                         .ForMember(svm => svm.SpecialtyName, opt => opt.MapFrom(s => s.Specialty.Name));
    
        }
    }
}