using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TestingSystem.Models
{
    public class Test
    {
        private ICollection<Result> results;
        private ICollection<Question> questions;

        public Test()
        {
            this.Results = new HashSet<Result>();
            this.Questions = new HashSet<Question>();
        }

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CourseID { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Result> Results
        {
            get { return results; }
            set { results = value; }
        }

        public virtual ICollection<Question> Questions
        {
            get { return questions; }
            set { questions = value; }
        }
    }
}
