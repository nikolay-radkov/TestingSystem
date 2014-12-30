namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    using AutoMapper;
    using System.ComponentModel.DataAnnotations;
    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class QuestionViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public int ID { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Брой правилни отговори")]
        public int CorrectAnswersCount { get; set; }

        [Display(Name = "Име на тест")]
        public string TestName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                         .ForMember(qvm => qvm.TestName, opt => opt.MapFrom(q => q.Test.Name));
        }
    }
}