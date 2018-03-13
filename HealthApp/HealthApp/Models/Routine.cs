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
        public virtual ApplicationUser User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
