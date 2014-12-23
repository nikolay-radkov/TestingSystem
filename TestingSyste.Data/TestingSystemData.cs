namespace TestingSystem.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TestingSystem.Data.Repositories.Base;
    using TestingSystem.Models;

    public class TestingSystemData : ITestingSystemData
    {
        private readonly TestingSystemDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public TestingSystemData()
            : this(new TestingSystemDbContext())
        {
        }

        public TestingSystemData(TestingSystemDbContext context)
        {
            this.context = context;
        }

        public TestingSystemDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<Student> Students
        {
            get { return this.GetRepository<Student>(); }
        }

        public IRepository<Specialty> Specialties
        {
            get { return this.GetRepository<Specialty>(); }
        }

        public IRepository<Course> Courses
        {
            get { return this.GetRepository<Course>(); }
        }

        public IRepository<Test> Tests
        {
            get { return this.GetRepository<Test>(); }
        }

        public IRepository<Question> Questions
        {
            get { return this.GetRepository<Question>(); }
        }

        public IRepository<Answer> Answers
        {
            get { return this.GetRepository<Answer>(); }
        }

        public IRepository<Result> Results
        {
            get { return this.GetRepository<Result>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
