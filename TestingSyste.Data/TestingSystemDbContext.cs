namespace TestingSyste.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TestingSystem.Models;

    public class TestingSystemDbContext : IdentityDbContext<Student>
    {
        public TestingSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TestingSystemDbContext Create()
        {
            return new TestingSystemDbContext();
        }
    }
}
