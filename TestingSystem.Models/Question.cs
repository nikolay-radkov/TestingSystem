namespace TestingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Question
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }

        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(256)]
        public string Text { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int TestID { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get
            {
                return this.answers;
            }

            set
            {
                this.answers = value;
            }
        }
    }
}
