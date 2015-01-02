namespace TestingSystem.Web.Areas.Administration.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class NewStudentBindingModel : IMapFrom<Student>
    {
        [Required(ErrorMessage = "{0} е задължително поле!")]
        [MinLength(5, ErrorMessage = "{0} трябва да бъде поне 5 символа")]
        [MaxLength(256, ErrorMessage = "{0} трябва да бъде  символа")]
        [Display(Name = "Пълно име")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле!")]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле!")]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; }

        [Display(Name = "Факултетен №")]
        public long? FacultyNumber { get; set; }

        [Range(1, 20)]
        [Display(Name = "Семестър")]
        public int? Semester { get; set; }

        [Display(Name = "Специалност")]
        public int? SpecialtyID { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле!")]
        [Display(Name = "Парола")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторете паролата")]
        [Compare("Password", ErrorMessage = "Паролата и потвърждението не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}