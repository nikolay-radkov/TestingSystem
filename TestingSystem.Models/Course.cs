using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSystem.Models
{
    public class Course
    {
        private ICollection<Test> tests;

        public Course()
        {
            this.Tests = new HashSet<Test>();
        }

        public int ID { get; set; }

        public string Name { get; set; }
        public int Semester { get; set; }

        public int SpecialtyID { get; set; }
   
        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Test> Tests
        {
            get { return tests; }
            set { tests = value; }
        }
    }
}
