namespace TestingSystem.Web.Models
{
    using System.Collections.Generic;
    
    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public int CorrectAnswersCount { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}