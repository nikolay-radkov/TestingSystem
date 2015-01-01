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
using TestingSystem.Web.Areas.Administration.ViewModels;
using TestingSystem.Web.Controllers.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TestingSystem.Web.Areas.Administration.InputModels;

namespace TestingSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "admin")]
    public class SpecialtiesController : BaseController
    {
        public SpecialtiesController(ITestingSystemData data)
            : base (data)
        {
        }

        // GET: Administration/Specialties
        public ActionResult Index()
        {
            var specialties = this.Data
                           .Specialties
                           .All()
                           .AsQueryable()
                           .Project()
                           .To<SpecialtyViewModel>()
                           .ToList();

            return View(specialties);
        }

        // GET: Administration/Specialties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var specialty = this.Data.Specialties.GetById(id);
            
            if (specialty == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<SpecialtyViewModel>(specialty);

            return View(result);
        }

        // GET: Administration/Specialties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Specialties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpecialtyBindingModel specialty)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Specialty>(specialty);

                this.Data.Specialties.Add(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        // GET: Administration/Specialties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var specialty = this.Data.Specialties.GetById(id);
           
            if (specialty == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<SpecialtyBindingModel>(specialty);

            return View(result);
        }

        // POST: Administration/Specialties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecialtyBindingModel specialty)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Specialty>(specialty);

                this.Data.Specialties.Update(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        // GET: Administration/Specialties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var specialty = this.Data.Specialties.GetById(id);

            if (specialty == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<SpecialtyViewModel>(specialty);

            return View(result);
        }

        // POST: Administration/Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialty specialty = this.Data.Specialties.GetById(id);
            this.Data.Specialties.Delete(specialty);
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
