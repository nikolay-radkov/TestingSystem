namespace TestingSystem.Web.Areas.Administration.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Foolproof;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class TestBindingModel : IMapFrom<Test>
    {
        public TestBindingModel()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now.AddDays(1);
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Начална дата")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Крайна дата")]
        [GreaterThan("StartDate", ErrorMessage = "{0} трябва да е по-голяма от Начална дата")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Курс")]
        public int CourseID { get; set; }
    }
}