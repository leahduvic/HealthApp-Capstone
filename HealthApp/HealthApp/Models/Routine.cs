using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models
{
    public class Routine
    {
        [Key]
        public int RoutineId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
