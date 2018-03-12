using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models.ManageViewModels
{
    public class PersonalSettingsViewModel
    {
        public ApplicationUser User { get; set; }

        [Display(Name = "Body Weight")]
        public int BodyWeight { get; set; }

        public int BMI { get; set; }

        public string StatusMessage { get; set; }
    }
}
