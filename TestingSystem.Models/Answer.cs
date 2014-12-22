using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSystem.Models
{
    public class Answer
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionID { get; set; }

        public virtual Question Question { get; set; }
    }
}
