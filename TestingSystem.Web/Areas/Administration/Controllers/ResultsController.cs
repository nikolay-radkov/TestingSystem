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
using AutoMapper.QueryableExtensions;
using TestingSystem.Web.Areas.Administration.ViewModels;

namespace TestingSystem.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "admin")]
    public class ResultsController : BaseController
    {
        public ResultsController(ITestingSystemData data)
            : base(data)
        {
        }

        // GET: Administration/Results
        public ActionResult Index()
        {
            var results = this.Data
                                .Results
                                .All()
                                .Project()
                                .To<ResultViewModel>()
                                .ToList();

            return View(results);
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
