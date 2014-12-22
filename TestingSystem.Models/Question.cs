using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSystem.Models
{
    public class Question
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int ID { get; set; }

        public string Content { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int TestID { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get { return answers; }
            set { answers = value; }
        }
    }
}
