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
    public class ResultsController : Controller
    {
        private TestingSystemDbContext db = new TestingSystemDbContext();

        // GET: Administration/Results
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.Student).Include(r => r.Test);
            return View(results.ToList());
        }

        // GET: Administration/Results/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Administration/Results/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Users, "Id", "FullName");
            ViewBag.TestID = new SelectList(db.Tests, "ID", "Name");
            return View();
        }

        // POST: Administration/Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Points,StudentID,TestID")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Users, "Id", "FullName", result.StudentID);
            ViewBag.TestID = new SelectList(db.Tests, "ID", "Name", result.TestID);
            return View(result);
        }

        // GET: Administration/Results/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Users, "Id", "FullName", result.StudentID);
            ViewBag.TestID = new SelectList(db.Tests, "ID", "Name", result.TestID);
            return View(result);
        }

        // POST: Administration/Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Points,StudentID,TestID")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Users, "Id", "FullName", result.StudentID);
            ViewBag.TestID = new SelectList(db.Tests, "ID", "Name", result.TestID);
            return View(result);
        }

        // GET: Administration/Results/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Administration/Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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
