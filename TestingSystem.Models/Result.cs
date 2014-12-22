using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingSystem.Models
{
    public class Result
    {
        public int ID { get; set; }

        public double Points { get; set; }

        public string StudentID { get; set; }

        public virtual Student Student { get; set; }

        public int TestID { get; set; }

        public virtual Test Test { get; set; }
    }
}
