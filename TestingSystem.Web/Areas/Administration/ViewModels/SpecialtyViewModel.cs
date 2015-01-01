using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestingSystem.Models;
using TestingSystem.Web.Infrastructure.Mapping;

namespace TestingSystem.Web.Areas.Administration.ViewModels
{
    public class SpecialtyViewModel : IMapFrom<Specialty>
    {
        public int ID { get; set; }

        [Display(Name="Специалност")]
        public string Name { get; set; }
    }
}