﻿namespace S2Dent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Doctor : ApplicationUser
    {
        public Doctor()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DoctorsAssistants = new HashSet<DoctorAssisstant>();
            this.Treatments = new HashSet<Treatment>();
        }

        [Required]
        [MaxLength(100)]
        public string Specialty { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
