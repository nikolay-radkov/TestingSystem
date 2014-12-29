using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Data;
using TestingSystem.Web.Controllers.Base;
using TestingSystem.Web.Models;
using Microsoft.AspNet.Identity;
using AutoMapper.QueryableExtensions;
using TestingSystem.Web.InputModels;
using TestingSystem.Models;

namespace TestingSystem.Web.Controllers
{
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

            return View(tests);
        }


        [HttpGet]
        [Authorize]
        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Questions(int id)
        {
            if (Session["Visited"] != null)
            {
                Session["Visited"] = null;
                return RedirectToAction("Index", "Tests", new { area = "" });
            }
            else
            {
                Session["Visited"] = "True";
            }
            //TODO: Chech if is authorize
            var questions = this.Data
                                .Questions
                                .All()
                                .Where(q => q.TestID == id)
                                .AsQueryable()
                                .Project()
                                .To<QuestionViewModel>()
                                .ToList();

            return View(questions);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Result(int id, AnswerBindingModel results)
        {
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

            this.SaveResult(id, points);

            return View(points);
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