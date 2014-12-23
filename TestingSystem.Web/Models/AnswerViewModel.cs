namespace TestingSystem.Web.Models
{
    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public int ID { get; set; }

        public string Text { get; set; }
    }
}