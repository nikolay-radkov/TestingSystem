using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TestingSystem.Models
{
    public class Answer
    {
        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(256)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionID { get; set; }

        public virtual Question Question { get; set; }
    }
}
