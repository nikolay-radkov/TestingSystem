namespace TestingSystem.Web.Controllers.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;

    using TestingSystem.Data;
    using TestingSystem.Models;
    using TestingSystem.Web.Controllers.WebApi.Base;
    using TestingSystem.Web.InputModels;
    using TestingSystem.Web.Models;

    public class TestsController : BaseApiController
    {
        private Dictionary<int, int> previousQuestions;

        public TestsController(ITestingSystemData data)
            : base(data)
        {
            this.previousQuestions = new Dictionary<int, int>();
        }

        public TestsController()
            : this(new TestingSystemData())
        {
        }

        // api/Tests/All
        [HttpPost]
        [Authorize]
        public IHttpActionResult All()
        {
            var studentID = this.User.Identity.GetUserId();
            var student = this.Data.Students.GetById(studentID);

            var tests = this.Data
                            .Tests
                            .All()
                            .Where(t => t.Course.SpecialtyID == student.SpecialtyID
                                && t.EndDate > DateTime.Now
                                && t.StartDate < DateTime.Now
                                && t.Course.Semester == student.Semester
                                && t.Results.Count(r => r.StudentID == student.Id) == 0)
                            .AsQueryable()
                            .Project()
                            .To<TestViewModel>()
                            .ToList();

            return this.Ok(tests);
        }

        // api/Tests/Questions/5
        [HttpPost]
        [Authorize]
        public IHttpActionResult Questions(int? id)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var questions = this.Data
                                .Questions
                                .All()
                                .Where(q => q.TestID == id)
                                .AsQueryable()
                                .Project()
                                .To<QuestionViewModel>()
                                .ToList();

            return this.Ok(questions);
        }

        // api/Tests/Result/5
        [HttpPost]
        [Authorize]
        public IHttpActionResult Result(int? id, AnswerBindingModel results)
        {
            if (id == null)
            {
                return this.BadRequest();
            }

            var points = 0.0;

            if (results.Answers != null)
            {
                var userAnswers = results.Answers.Split(',');

                var test = this.Data.Tests.GetById(id);

                foreach (var userAnswer in userAnswers)
                {
                    var currentAnswer = this.Data.Answers.GetById(int.Parse(userAnswer));

                    var questionCorrectAnswers = 0;

                    if (this.previousQuestions.ContainsKey(currentAnswer.QuestionID))
                    {
                        questionCorrectAnswers = this.previousQuestions[currentAnswer.QuestionID];
                    }
                    else
                    {
                        questionCorrectAnswers = this.Data.Questions.GetById(currentAnswer.QuestionID).CorrectAnswersCount;
                        this.previousQuestions[currentAnswer.QuestionID] = questionCorrectAnswers;
                    }

                    if (currentAnswer.IsCorrect)
                    {
                        points += 1.0 / questionCorrectAnswers;
                    }
                }
            }

            this.SaveResult((int)id, points);

            var responseResult = new FinalResultViewModel
            {
                Points = points,
                Grade = (points / 5) + 2
            };

            return this.Ok(responseResult);
        }

        private void SaveResult(int testID, double points)
        {
            var userID = this.User.Identity.GetUserId();

            var result = new Result
            {
                StudentID = userID,
                Points = points,
                TestID = testID
            };

            this.Data.Results.Add(result);

            this.Data.SaveChanges();
        }
    }
}
