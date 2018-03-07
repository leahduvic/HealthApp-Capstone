using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        public string Title { get; set; }

        public int Weight { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }

        public ICollection<Routine> Routines { get; set; }
    }
}
