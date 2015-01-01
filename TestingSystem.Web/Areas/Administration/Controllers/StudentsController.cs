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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace TestingSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "admin")]
    public class StudentsController : BaseController
    {
        public StudentsController(ITestingSystemData data)
            : base (data)
        {
        }

        // GET: Administration/Students
        public ActionResult Index()
        {
            var students = this.Data
                            .Students
                            .All()
                            .AsQueryable()
                            .Project()
                            .To<StudentViewModel>()
                            .ToList();

            return View(students);
        }

        // GET: Administration/Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = this.Data.Students.GetById(id);
            
            if (student == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<StudentViewModel>(student);

            return View(result);
        }

        // GET: Administration/Students/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name");
            return View();
        }

        // POST: Administration/Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewStudentBindingModel student)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Student>(student);

                var userStore = new UserStore<Student>(this.Data.Context);
                var userManager = new UserManager<Student>(userStore);


                try
                {
                    userManager.Create(result, student.Password);
                
                }
                catch (DbEntityValidationException e)
                {
                    var p = e.EntityValidationErrors.FirstOrDefault().Entry;
                    var q = e.EntityValidationErrors.FirstOrDefault().ValidationErrors;

                }

                return RedirectToAction("Index");
                

                //var result = await UserManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //    // Send an email with this link
                //    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                //    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                //}
       

                //var result = AutoMapper.Mapper.Map<Student>(student);

                //this.Data.Students.Add(result);
                //this.Data.SaveChanges();
            }

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", student.SpecialtyID);
            return View(student);
        }

        // GET: Administration/Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var student = this.Data.Students.GetById(id);
            
            if (student == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<StudentBindingModel>(student);

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", student.SpecialtyID);
            return View(result);
        }

        // POST: Administration/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentBindingModel student)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Student>(student);

                this.Data.Students.Update(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", student.SpecialtyID);
            
            return View(student);
        }

        // GET: Administration/Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = this.Data.Students.GetById(id);
            
            if (student == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<StudentViewModel>(student);

            return View(result);
        }

        // POST: Administration/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var student = this.Data.Students.GetById(id);
            this.Data.Students.Delete(student);
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
