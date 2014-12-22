﻿namespace TestingSystem.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class Student : IdentityUser
    {
        private ICollection<Result> results;

        public Student()
        {
            this.Results = new HashSet<Result>();
        }

        public string Name { get; set; }

        public string EGN { get; set; }

        public long? FacultyNumber { get; set; }

        public int? Semester { get; set; }

        public int? SpecialtyID { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Result> Results
        {
            get { return results; }
            set { results = value; }
        }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Student> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
