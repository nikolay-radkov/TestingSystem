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
    [Authorize(Roles = "admin")]
    public class StudentsController : Controller
    {
        private TestingSystemDbContext db = new TestingSystemDbContext();

        // GET: Administration/Students
        public ActionResult Index()
        {
            var students = db.Users.Include(s => s.Specialty);
            return View(students.ToList());
        }

        // GET: Administration/Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Administration/Students/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "ID", "Name");
            return View();
        }

        // POST: Administration/Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,EGN,FacultyNumber,Semester,SpecialtyID,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyID = new SelectList(db.Specialties, "ID", "Name", student.SpecialtyID);
            return View(student);
        }

        // GET: Administration/Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "ID", "Name", student.SpecialtyID);
            return View(student);
        }

        // POST: Administration/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,EGN,FacultyNumber,Semester,SpecialtyID,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "ID", "Name", student.SpecialtyID);
            return View(student);
        }

        // GET: Administration/Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Users.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Administration/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Users.Find(id);
            db.Users.Remove(student);
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
