namespace TestingSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TestingSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TestingSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TestingSystemDbContext context)
        {
            if (!context.Specialties.Any())
            {
                this.SeedSpecialties(context);
            }

            if (!context.Users.Any())
            {
                this.SeedUsers(context);
            }
        }

        private void SeedUsers(TestingSystemDbContext context)
        {
            var specialties = context.Specialties.Select(x => x).ToList();

            var ivanPassword = "qwerty";

            var georgiPassword = "123456";

            var userStore = new UserStore<Student>(context);
            var userManager = new UserManager<Student>(userStore);

            var ivan = new Student
            {
                FullName = "Иван Иванов",
                FacultyNumber = 1234567,
                Semester = 6,
                UserName = "qwerty",
                Email = "ivan@ivanov.com",
                EGN = "9202020222",
                SpecialtyID = specialties[0].ID
            };

            userManager.Create(ivan, ivanPassword);

            var georgi = new Student
            {
                FullName = "Георги Георгиев",
                FacultyNumber = 2345678,
                Semester = 7,
                UserName = "123456",
                Email = "ivan@123456.com",
                EGN = "9202050222",
                SpecialtyID = specialties[0].ID
            };

            userManager.Create(georgi, georgiPassword);
        }

        private void SeedSpecialties(TestingSystemDbContext context)
        {
            context.Specialties.AddOrUpdate(
                s => s.Name,
                new Specialty { Name = "Компютърни системи и технологии" },
                new Specialty { Name = "Телекомуникации" }
            );

            context.SaveChanges();
            this.SeedCourses(context);
        }

        private void SeedCourses(TestingSystemDbContext context)
        {
            var specialties = context.Specialties.Select(x => x).ToList();

            context.Courses.AddOrUpdate(
                c => c.Name,
                new Course
                {
                    Name = "Високопроизводителни компютърни системи",
                    SpecialtyID = specialties[0].ID,
                    Semester = 6
                },
                new Course
                {
                    Name = "Паралелно програмиране",
                    SpecialtyID = specialties[0].ID,
                    Semester = 7
                }
            );

            context.SaveChanges();
            this.SeedTests(context);
        }

        private void SeedTests(TestingSystemDbContext context)
        {
            var courses = context.Courses.Select(x => x).ToList();

            context.Tests.AddOrUpdate(
                t => t.StartDate,
                new Test
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddYears(1),
                    CourseID = courses[0].ID
                },
                new Test
                {
                    StartDate = DateTime.Now.AddYears(-1),
                    EndDate = DateTime.Now,
                    CourseID = courses[1].ID
                }
            );

            context.SaveChanges();
            this.SeedQuestions(context);
        }

        private void SeedQuestions(TestingSystemDbContext context)
        {
            var tests = context.Tests.Select(x => x).ToList();

            var answer1 = new Answer()
            {
                Text = "Високопроизводително компютърни сгради",
                IsCorrect = false
            };

            var answer2 = new Answer()
            {
                Text = "Високопроизводително компютърни системи",
                IsCorrect = true
            };

            var question1 = new Question
            {
                Text = "Какво е ВПКС?",
                CorrectAnswersCount = 1,
                TestID = tests[0].ID
            };

            context.Questions.Add(question1);
            context.SaveChanges();

            question1.Answers.Add(answer1);
            question1.Answers.Add(answer2);
            context.SaveChanges();
           
            var answer3 = new Answer()
            {
                Text = "ФКСУ",
                IsCorrect = true
            };

            var answer4 = new Answer()
            {
                Text = "Автоматика",
                IsCorrect = false
            };

            var answer5 = new Answer()
            {
                Text = "Телекомуникации",
                IsCorrect = false
            };

            var answer6 = new Answer()
            {
                Text = "Всички",
                IsCorrect = false
            };

            var question2 = new Question
            {
                Text = "ВПКС е специалност от кой факултет?",
                CorrectAnswersCount = 1,
                TestID = tests[0].ID
            };

            context.Questions.Add(question2);
            context.SaveChanges();

            question2.Answers.Add(answer3);
            question2.Answers.Add(answer4);
            question2.Answers.Add(answer5);
            question2.Answers.Add(answer6);

            context.SaveChanges();
        }
    }
}
