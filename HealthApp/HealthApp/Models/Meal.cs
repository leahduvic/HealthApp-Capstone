using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }

        [Required]
        public string Title { get; set; }

        public int Protein { get; set; }

        public int Carbohydrates { get; set; }

        public int Sugar { get; set; }

        public int Sodium { get; set; }


        public ICollection<UserMeal> UserMeals { get; set; }
    }
}
