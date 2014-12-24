namespace TestingSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Student : IdentityUser
    {
        private ICollection<Result> results;

        public Student()
        {
            this.Results = new HashSet<Result>();
        }

        [Required]
        [MinLength(5)]
        [MaxLength(256)]
        public string FullName { get; set; }

        [Required]
        public string EGN { get; set; }

        public long? FacultyNumber { get; set; }

        [Range(1, 20)]
        public int? Semester { get; set; }

        public int? SpecialtyID { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Result> Results
        {
            get
            {
                return this.results;
            }

            set
            {
                this.results = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Student> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
  
            return userIdentity;
        }
    }
}
