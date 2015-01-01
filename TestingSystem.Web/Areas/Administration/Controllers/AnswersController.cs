namespace TestingSystem.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using TestingSystem.Data;
    using TestingSystem.Models;
    using TestingSystem.Web.Areas.Administration.InputModels;
    using TestingSystem.Web.Areas.Administration.ViewModels;
    using TestingSystem.Web.Controllers.Base;

    [Authorize(Roles = "admin")]
    public class AnswersController : BaseController
    {
        public AnswersController(ITestingSystemData data)
            : base (data)
        {
        }

        // GET: Administration/Answers
        public ActionResult Index()
        {
            var answers = this.Data
                            .Answers
                            .All()
                            .AsQueryable()
                            .Project()
                            .To<AnswerViewModel>()
                            .ToList();

            return View(answers);
        }

        // GET: Administration/Answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);

            if (answer == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<AnswerViewModel>(answer);

            return View(result);
        }

        // GET: Administration/Answers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(this.Data.Questions.All(), "ID", "Text");
            return View();
        }

        // POST: Administration/Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerBindingModel answer)
        {          
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Answer>(answer);

                this.Data.Answers.Add(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(this.Data.Questions.All(), "ID", "Text", answer.QuestionID);
            return View(answer);
        }

        // GET: Administration/Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);

            if (answer == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<AnswerBindingModel>(answer);

            ViewBag.QuestionID = new SelectList(this.Data.Questions.All(), "ID", "Text", result.QuestionID);

            return View(result);
        }

        // POST: Administration/Answers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnswerBindingModel answer)
        {
            if (ModelState.IsValid)
            {
                var result = AutoMapper.Mapper.Map<Answer>(answer);

                this.Data.Answers.Update(result);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.QuestionID = new SelectList(this.Data.Questions.All(), "ID", "Text", answer.QuestionID);
            
            return View(answer);
        }

        // GET: Administration/Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var answer = this.Data.Answers.GetById(id);

            if (answer == null)
            {
                return HttpNotFound();
            }

            var result = AutoMapper.Mapper.Map<AnswerViewModel>(answer);

            return View(result);
        }

        // POST: Administration/Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var answer = this.Data.Answers.GetById(id);
            this.Data.Answers.Delete(answer);
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
