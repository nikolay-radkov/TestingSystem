﻿namespace TestingSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
   
    using TestingSystem.Data;
    using TestingSystem.Models;
    using TestingSystem.Web.Areas.Administration.InputModels;
    using TestingSystem.Web.Areas.Administration.ViewModels;
    using TestingSystem.Web.Controllers.Base;

    [Authorize(Roles = "admin")]
    public class QuestionsController : BaseController
    {
        public QuestionsController(ITestingSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Questions
        public ActionResult Index()
        {
            var questions = this.Data
                                    .Questions
                                    .All()
                                    .AsQueryable()
                                    .Project()
                                    .To<QuestionViewModel>()
                                    .ToList();

            return this.View(questions);
        }

        // GET: Administration/Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = this.Data.Questions.GetById(id);
            
            if (question == null)
            {
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<QuestionViewModel>(question);

            return this.View(result);
        }

        // GET: Administration/Questions/Create
        public ActionResult Create()
        {
            this.ViewBag.TestID = new SelectList(this.Data.Tests.All(), "ID", "Name");
            return this.View();
        }

        // POST: Administration/Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionBindingModel question)
        {
            var result = AutoMapper.Mapper.Map<Question>(question);

            if (ModelState.IsValid)
            {
                this.Data.Questions.Add(result);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index");
            }

            ViewBag.TestID = new SelectList(this.Data.Tests.All(), "ID", "Name", question.TestID);
            return this.View(question);
        }

        // GET: Administration/Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = this.Data.Questions.GetById(id);
            
            if (question == null)
            {
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<QuestionBindingModel>(question);

            ViewBag.TestID = new SelectList(this.Data.Tests.All(), "ID", "Name", question.TestID);
            
            return this.View(result);
        }

        // POST: Administration/Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionBindingModel question)
        {
            var result = AutoMapper.Mapper.Map<Question>(question);

            if (ModelState.IsValid)
            {
                this.Data.Questions.Update(result);
                this.Data.SaveChanges();
                return this.RedirectToAction("Index");
            }

            ViewBag.TestID = new SelectList(this.Data.Tests.All(), "ID", "Name", question.TestID);
           
            return this.View(question);
        }

        // GET: Administration/Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = this.Data.Questions.GetById(id);
            
            if (question == null)
            {
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<QuestionViewModel>(question);

            return this.View(result);
        }

        // POST: Administration/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var question = this.Data.Questions.GetById(id);
            this.Data.Questions.Delete(question);
            this.Data.SaveChanges();
            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
