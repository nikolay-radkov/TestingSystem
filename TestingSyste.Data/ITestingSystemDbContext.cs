namespace TestingSystem.Data
{
    using System;
    using System.Data.Entity;
    using TestingSystem.Models;

    public interface ITestingSystemDbContext
    {
        IDbSet<Answer> Answers { get; set; }

        IDbSet<Course> Courses { get; set; }
        
        IDbSet<Question> Questions { get; set; }
        
        IDbSet<Result> Results { get; set; }
        
        IDbSet<Specialty> Specialties { get; set; }
        
        IDbSet<Test> Tests { get; set; }
    }
}
