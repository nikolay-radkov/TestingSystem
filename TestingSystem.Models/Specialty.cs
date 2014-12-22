namespace TestingSystem.Models
{
    using System.Collections.Generic;

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
