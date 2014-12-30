namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    
    using AutoMapper;
    
    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>, IHaveCustomMappings
    {
        public int ID { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Правилен отгоговор")]
        public bool IsCorrect { get; set; }

        [Display(Name = "Въпрос")]
        public string QuestionText { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Answer, AnswerViewModel>()
                         .ForMember(avm => avm.QuestionText, opt => opt.MapFrom(a => a.Question.Text));
        }
    }
}