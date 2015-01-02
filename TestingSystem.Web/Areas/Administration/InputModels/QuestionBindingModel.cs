namespace TestingSystem.Web.Areas.Administration.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class QuestionBindingModel : IMapFrom<Question>
    {
        public int ID { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Брой правилни отговори")]
        public int CorrectAnswersCount { get; set; }

        [Display(Name = "Име на тест")]
        public int TestID { get; set; }
    }
}