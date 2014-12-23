using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Models
{
    public class QuestionViewModel : IMapFrom<Question>
    {
        public int ID { get; set; }

        public string Text { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}