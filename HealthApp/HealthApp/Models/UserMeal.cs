using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models
{
    public class UserMeal
    {
        [Key]
        public int UserMealId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
