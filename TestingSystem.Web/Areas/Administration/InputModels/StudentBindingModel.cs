using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Areas.Administration.InputModels
{
    public class StudentBindingModel : IMapFrom<Student>
    {
          [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле!")]
        [MinLength(5)]
        [MaxLength(256)]
        [Display(Name="Пълно име")]
        public string FullName { get; set; }

        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [Display(Name="ЕГН")]
        public string EGN { get; set; }

        [Required]
        [Display(Name="Факултетен №")]
        public long? FacultyNumber { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name="Семестър")]
        public int? Semester { get; set; }

        [Required]
        [Display(Name="Специалност")]
        public int? SpecialtyID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}