﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthApp.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public int RedBloodCount { get; set; }

        public int WhiteBloodCount { get; set; }

        public int BloodGlucose { get; set; }

        public int Cholestorol { get; set; }

        public int Hemoglobin { get; set; }

        public int Iron { get; set; }

        public int B12 { get; set; }
    }
}
