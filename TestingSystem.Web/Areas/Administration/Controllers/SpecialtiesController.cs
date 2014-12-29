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

namespace TestingSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpecialtiesController : Controller
    {
        private TestingSystemDbContext db = new TestingSystemDbContext();

        // GET: Administration/Specialties
        public ActionResult Index()
        {
            return View(db.Specialties.ToList());
        }

        // GET: Administration/Specialties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
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
        public ActionResult Create([Bind(Include = "ID,Name")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                db.Specialties.Add(specialty);
                db.SaveChanges();
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
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Administration/Specialties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialty).State = EntityState.Modified;
                db.SaveChanges();
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
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Administration/Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialty specialty = db.Specialties.Find(id);
            db.Specialties.Remove(specialty);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
