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

namespace TestingSystem.Web.Controllers.WebApi
{
    public class TestsController : BaseApiController
    {
        public TestsController(ITestingSystemData data)
            : base(data)
        {

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
        public IHttpActionResult Result(int id, ICollection<AnswerBindingModel> userAnswers)
        {
            var test = this.Data.Tests.GetById(id);

            var points = 0.0;

            foreach (var userAnswer in userAnswers)
            {
                var question = this.Data.Questions.GetById(userAnswer.QuestionID);

                foreach (var answerID in userAnswer.AnswerIDs)
                {
                    var answer = this.Data.Answers.GetById(answerID);

                    if (answer.IsCorrect)
                    {
                        points += 1.0 / question.CorrectAnswersCount;
                    }
                }
            }

            return Ok(points);
        }
    }
}
