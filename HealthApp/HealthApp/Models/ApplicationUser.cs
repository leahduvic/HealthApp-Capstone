﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HealthApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(55, ErrorMessage = "Please enter your nickname")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(55, ErrorMessage = "Sorry, too long")]
        public string LastName { get; set; }

        // tables where the AU is the FK.
        public virtual ICollection<Measurement> Measurements { get; set; }

        public virtual ICollection<Routine> Routines { get; set; }

        public virtual ICollection<UserMeal> UserMeals { get; set; }
    }
}
