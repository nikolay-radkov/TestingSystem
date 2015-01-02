namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;

    public class SpecialtyViewModel : IMapFrom<Specialty>
    {
        public int ID { get; set; }

        [Display(Name = "Специалност")]
        public string Name { get; set; }
    }
}