namespace TestingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Specialty
    {
        private ICollection<Student> students;
        private ICollection<Course> course;

        public Specialty()
        {
            this.Students = new HashSet<Student>();
            this.Courses = new HashSet<Course>();
        }

        public int ID { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return course; }
            set { course = value; }
        }

        public virtual ICollection<Student> Students
        {
            get { return students; }
            set { students = value; }
        }
    }
}
