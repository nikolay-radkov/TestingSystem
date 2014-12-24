namespace TestingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        private ICollection<Test> tests;

        public Course()
        {
            this.Tests = new HashSet<Test>();
        }

        public int ID { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(256)]
        public string Name { get; set; }

        [Range(1, 20)]
        public int Semester { get; set; }

        public int SpecialtyID { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Test> Tests
        {
            get
            {
                return this.tests;
            }

            set
            {
                this.tests = value;
            }
        }
    }
}
