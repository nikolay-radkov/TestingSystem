namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        public int ID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionID { get; set; }

        public virtual Question Question { get; set; }
    }
}
