namespace S2Dent.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
   
    using Microsoft.AspNetCore.Http;
    
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class DoctorInputModel : IMapFrom<Doctor>, IMapTo<Doctor>
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        [StringLength(20, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(String))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        [StringLength(20, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(String))]
        public string MiddleName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        [StringLength(20, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(String))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        [StringLength(100, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(String))]
        public string Specialty { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        [StringLength(450, ErrorMessageResourceName = "DescriptionIsTooLong", ErrorMessageResourceType = typeof(String))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(String))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(String))]
        public string Email { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}
