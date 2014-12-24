namespace TestingSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
            get
            {
                return this.results;
            }

            set
            {
                this.results = value;
            }
        }

        public virtual ICollection<Question> Questions
        {
            get
            {
                return this.questions;
            }

            set
            {
                this.questions = value;
            }
        }
    }
}
