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

namespace TestingSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "admin")]
    public class CoursesController : BaseController
    {
        public CoursesController(ITestingSystemData data)
            : base (data)
        {
        }

        // GET: Administration/Courses
        public ActionResult Index()
        {
            var courses = this.Data
                            .Courses
                            .All()
                            .AsQueryable()
                            .Project()
                            .To<CourseViewModel>()
                            .ToList(); ;
           
            return View(courses);
        }

        // GET: Administration/Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = this.Data.Courses.GetById(id);
            
            if (course == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<CourseViewModel>(course);

            return View(result);
        }

        // GET: Administration/Courses/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name");
            return View();
        }

        // POST: Administration/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseBindingModel course)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Course>(course);

                this.Data.Courses.Add(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", course.SpecialtyID);
            return View(course);
        }

        // GET: Administration/Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = this.Data.Courses.GetById(id);
           
            if (course == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<CourseBindingModel>(course);

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", course.SpecialtyID);
         
            return View(result);
        }

        // POST: Administration/Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseBindingModel course)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Course>(course);

                this.Data.Courses.Update(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", course.SpecialtyID);
            return View(course);
        }

        // GET: Administration/Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var course = this.Data.Courses.GetById(id);
            
            if (course == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<CourseViewModel>(course);

            return View(result);
        }

        // POST: Administration/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = this.Data.Courses.GetById(id);
            this.Data.Courses.Delete(course);
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
