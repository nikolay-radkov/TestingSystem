namespace TestingSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    
    using TestingSystem.Data;
    using TestingSystem.Models;
    using TestingSystem.Web.Controllers.Base;
    using TestingSystem.Web.InputModels;
    using TestingSystem.Web.Models;

    [Authorize]
    public class TestsController : BaseController
    {
        private Dictionary<int, int> previousQuestions;

        public TestsController(ITestingSystemData data)
            : base(data)
        {
            this.previousQuestions = new Dictionary<int, int>();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var studentID = this.User.Identity.GetUserId();
            var student = this.Data.Students.GetById(studentID);

            // TODO: Check the user results for current tests
            var tests = this.Data
                            .Tests
                            .All()
                            .Where(t => t.Course.SpecialtyID == student.SpecialtyID
                                && t.EndDate > DateTime.Now
                                && t.StartDate < DateTime.Now
                                && t.Course.Semester == student.Semester)
                            .AsQueryable()
                            .Project()
                            .To<TestViewModel>()
                            .ToList();

            return this.View(tests);
        }

        [HttpGet]
        [Authorize]
        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Questions(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (this.Session["Visited"] != null)
            {
                this.Session["Visited"] = null;
                return this.RedirectToAction("Index", "Tests", new { area = string.Empty });
            }
            else
            {
                this.Session["Visited"] = "True";
            }

            // TODO: Chech if is authorize
            var questions = this.Data
                                .Questions
                                .All()
                                .Where(q => q.TestID == id)
                                .AsQueryable()
                                .Project()
                                .To<QuestionViewModel>()
                                .ToList();

            return this.View(questions);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Result(int? id, AnswerBindingModel results)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

            return this.View(responseResult);
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