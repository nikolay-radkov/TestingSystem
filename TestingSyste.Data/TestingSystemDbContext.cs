namespace TestingSystem.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using TestingSystem.Data.Migrations;
    using TestingSystem.Models;

    public class TestingSystemDbContext : IdentityDbContext<Student>, ITestingSystemDbContext
    {
        public TestingSystemDbContext()
            : base("TestingSystemConnectionAppHarbor", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TestingSystemDbContext, Configuration>());
        }

        public static TestingSystemDbContext Create()
        {
            return new TestingSystemDbContext();
        }

        public virtual IDbSet<Specialty> Specialties { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Test> Tests { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        public virtual IDbSet<Result> Results { get; set; }
    }
}
