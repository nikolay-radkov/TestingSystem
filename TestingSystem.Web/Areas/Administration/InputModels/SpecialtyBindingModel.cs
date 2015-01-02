namespace TestingSystem.Web.Areas.Administration.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class SpecialtyBindingModel : IMapFrom<Specialty>
    {
        public int ID { get; set; }

        [MinLength(2, ErrorMessage = "Полето {0} трябва да е поне 2 символа")]
        [MaxLength(256, ErrorMessage = "Полето {0} трябва да е с най-мого 256 символа")]
        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Специалност")]
        public string Name { get; set; }
    }
}