using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models
{
    public class Measurement
    {
        [Key]
        public int MeasurementId { get; set; }
        [Required]

        public virtual ApplicationUser User { get; set; }

        public int BodyWeight { get; set; }

        public int BMI { get; set; }

        public DateTime Date { get; set; }
    }
}
