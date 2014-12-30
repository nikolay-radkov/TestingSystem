namespace TestingSystem.Web.Areas.Administration.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class AnswerBindingModel : IMapFrom<Answer>
    {
        public int ID { get; set; }

        [Required(ErrorMessage="{0} е задължително поле")]
        [Display(Name="Текст")]
        [MinLength(2)]
        [MaxLength(256)]
        public string Text { get; set; }

        [Display(Name="Правилен отгоговор")]
        public bool IsCorrect { get; set; }

        [Display(Name="Въпрос")]
        public int QuestionID { get; set; }
    }
}