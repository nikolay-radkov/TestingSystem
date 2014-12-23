using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestingSystem.Data;
using TestingSystem.Web.Controllers.WebApi.Base;
using Microsoft.AspNet.Identity;

using AutoMapper.QueryableExtensions;
using TestingSystem.Web.Models;

namespace TestingSystem.Web.Controllers.WebApi
{
    [Authorize]
    public class TestsController : BaseApiController
    {
        public TestsController(ITestingSystemData data)
            : base(data)
        {

        }

        public TestsController()
            : this(new TestingSystemData()) 
        {

        }


        // api/Tests/All
        [HttpPost]
        public IHttpActionResult All()
        {
            var studentID = this.User.Identity.GetUserId();
            
            var student = this.Data.Students.GetById(studentID);

            var tests = this.Data
                            .Tests.All()
                            .Where(t => t.Course.SpecialtyID == student.SpecialtyID 
                                && t.EndDate > DateTime.Now 
                                && t.StartDate < DateTime.Now 
                                && t.Course.Semester == student.Semester)
                            .AsQueryable()
                            .Project()
                            .To<TestViewModel>()
                            .ToList();

            return Ok(tests);
        }

        // api/Tests/Questions/5
        [HttpPost]
        public IHttpActionResult Questions(int id)
        {

            return Ok();
        }

        // api/Tests/Result/5
        [HttpPost]
        public IHttpActionResult Result(int id)
        {

            return Ok();
        }
    }
}
