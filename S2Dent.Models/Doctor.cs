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
            Treatments = new HashSet<Treatment>();
        }

        [Required]
        public int SpecialityId { get; set; }

        public virtual Speciality Speciality { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
