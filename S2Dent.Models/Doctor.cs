namespace S2Dent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Doctor : ApplicationUser
    {
        public Doctor()
            : base()
        {
            Id = Guid.NewGuid().ToString();
            DoctorsAssistants = new HashSet<DoctorAssisstant>();
            Treatments = new HashSet<Treatment>();
        }

        [Required]
        public Speciality Specialty { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
