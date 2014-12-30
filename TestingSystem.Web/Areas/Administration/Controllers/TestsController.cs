namespace TestingSystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using TestingSystem.Data;
    using TestingSystem.Models;
    using TestingSystem.Web.Controllers.Base;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using TestingSystem.Web.Areas.Administration.ViewModels;
    using TestingSystem.Web.Areas.Administration.InputModels;

    [Authorize(Roles = "admin")]
    public class TestsController : BaseController
    {
        public TestsController(ITestingSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Tests
        public ActionResult Index()
        {
            var tests = this.Data
                            .Tests
                            .All()
                            .AsQueryable()
                            .Project()
                            .To<TestViewModel>()
                            .ToList();

            return View(tests);
        }

        // GET: Administration/Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var  test = this.Data.Tests.GetById(id);

            if (test == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<TestViewModel>(test);

            return View(result);
        }

        // GET: Administration/Tests/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name");
            return View();
        }

        // POST: Administration/Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestBindingModel test)
        {
            var result = AutoMapper.Mapper.Map<Test>(test);

            if (ModelState.IsValid)
            {
                this.Data.Tests.Add(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name", test.CourseID);
            return View(test);
        }

        // GET: Administration/Tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var test = this.Data.Tests.GetById(id);
            
            if (test == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<TestBindingModel>(test);

            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name", test.CourseID);

            return View(result);
        }

        // POST: Administration/Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestBindingModel test)
        {
            var result = AutoMapper.Mapper.Map<Test>(test);

            if (ModelState.IsValid)
            {
                this.Data.Tests.Update(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name", test.CourseID);
            
            return View(test);
        }

        // GET: Administration/Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var test = this.Data.Tests.GetById(id);
            
            if (test == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<TestViewModel>(test);

            return View(result);
        }

        // POST: Administration/Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var test = this.Data.Tests.GetById(id);
            this.Data.Tests.Delete(test);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
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
