using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Areas.Administration.InputModels
{
    public class CourseBindingModel : IMapFrom<Course>
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Название")]
        [MinLength(5)]
        [MaxLength(256)]
        public string Name { get; set; }

        [Range(1, 20)]
        [Display(Name="Семестър")]
        public int Semester { get; set; }

        [Display(Name = "Специалност")]
        public int SpecialtyID { get; set; }
    }
}