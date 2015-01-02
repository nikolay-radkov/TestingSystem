namespace TestingSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
  
    using TestingSystem.Data;
    using TestingSystem.Models;
    using TestingSystem.Web.Areas.Administration.InputModels;
    using TestingSystem.Web.Areas.Administration.ViewModels;
    using TestingSystem.Web.Controllers.Base;

    [Authorize(Roles = "admin")]
    public class StudentsController : BaseController
    {
        public StudentsController(ITestingSystemData data)
            : base(data)
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

            return this.View(students);
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
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<StudentViewModel>(student);

            return this.View(result);
        }

        // GET: Administration/Students/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name");
            return this.View();
        }

        // POST: Administration/Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewStudentBindingModel student)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Student>(student);

                var userStore = new UserStore<Student>(this.Data.Context);
                var userManager = new UserManager<Student>(userStore);
                userManager.Create(result, student.Password);

                return this.RedirectToAction("Index");
            }

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", student.SpecialtyID);
            return this.View(student);
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
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<StudentBindingModel>(student);

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", student.SpecialtyID);
            return this.View(result);
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
                return this.RedirectToAction("Index");
            }

            ViewBag.SpecialtyID = new SelectList(this.Data.Specialties.All(), "ID", "Name", student.SpecialtyID);

            return this.View(student);
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
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<StudentViewModel>(student);

            return this.View(result);
        }

        // POST: Administration/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var student = this.Data.Students.GetById(id);
            this.Data.Students.Delete(student);
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
