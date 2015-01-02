namespace TestingSystem.Web.Areas.Administration.Controllers
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

            return this.View(tests);
        }

        // GET: Administration/Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var test = this.Data.Tests.GetById(id);

            if (test == null)
            {
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<TestViewModel>(test);

            return this.View(result);
        }

        // GET: Administration/Tests/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name");
            return this.View();
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
                return this.RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name", test.CourseID);
            return this.View(test);
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
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<TestBindingModel>(test);

            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name", test.CourseID);

            return this.View(result);
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
                return this.RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(this.Data.Courses.All(), "ID", "Name", test.CourseID);

            return this.View(test);
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
                return this.HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<TestViewModel>(test);

            return this.View(result);
        }

        // POST: Administration/Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var test = this.Data.Tests.GetById(id);
            this.Data.Tests.Delete(test);
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
