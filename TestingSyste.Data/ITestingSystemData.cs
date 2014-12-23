namespace TestingSystem.Data
{
    using TestingSystem.Data;
    using TestingSystem.Data.Repositories.Base;
    using TestingSystem.Models;

    public interface ITestingSystemData
    {
        IRepository<Answer> Answers { get; }

        TestingSystemDbContext Context { get; }

        IRepository<Course> Courses { get; }

        IRepository<Question> Questions { get; }

        IRepository<Result> Results { get; }
        IRepository<Specialty> Specialties { get; }

        IRepository<Student> Students { get; }

        IRepository<Test> Tests { get; }

        int SaveChanges();

        void Dispose();
    }
}
