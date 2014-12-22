namespace TestingSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Result
    {
        public int ID { get; set; }

        [Range(0, 20)]
        public double Points { get; set; }

        [Required]
        public string StudentID { get; set; }

        public virtual Student Student { get; set; }

        public int TestID { get; set; }

        public virtual Test Test { get; set; }
    }
}
