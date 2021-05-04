namespace S2Dent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [MaxLength(20)]
        public string FirstName { get; set; }
        
        [MaxLength(20)]
        public string MiddleName { get; set; }

        [MaxLength(20)]
        public string ThirdName { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<DoctorAssisstant> DoctorsAssistants { get; set; }
    }
}
