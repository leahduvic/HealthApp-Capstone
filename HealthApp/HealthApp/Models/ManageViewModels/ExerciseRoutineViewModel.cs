using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models.ManageViewModels
{
    public class ExerciseRoutineViewModel
    {
        [Required]
        public int RoutineId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
