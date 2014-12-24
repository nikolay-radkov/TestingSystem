using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestingSystem.Data;
using TestingSystem.Web.Controllers.WebApi.Base;
using Microsoft.AspNet.Identity;

using AutoMapper.QueryableExtensions;
using TestingSystem.Web.Models;
using TestingSystem.Web.InputModels;
using TestingSystem.Models;

namespace TestingSystem.Web.Controllers.WebApi
{
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
                            .Tests.All()
                            .Where(t => t.Course.SpecialtyID == student.SpecialtyID
                                && t.EndDate > DateTime.Now
                                && t.StartDate < DateTime.Now
                                && t.Course.Semester == student.Semester)
                            .AsQueryable()
                            .Project()
                            .To<TestViewModel>()
                            .ToList();

            return Ok(tests);
        }

        // api/Tests/Questions/5
        [HttpPost]
        [Authorize]
        public IHttpActionResult Questions(int id)
        {
            // TODO: number of correct answer
            var questions = this.Data
                                .Questions
                                .All()
                                .Where(q => q.TestID == id)
                                .AsQueryable()
                                .Project()
                                .To<QuestionViewModel>()
                                .ToList();

            return Ok(questions);
        }

        // api/Tests/Result/5
        [HttpPost]
        [Authorize]
        public IHttpActionResult Result(int id, AnswerBindingModel results)
        {
            var userAnswers = results.Answers.Split(',');

            var test = this.Data.Tests.GetById(id);

            var points = 0.0;

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

            this.SaveResult(id, points);

            return Ok(points);
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
