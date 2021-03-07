namespace S2Dent.ViewModels.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Resources;
    using Microsoft.AspNetCore.Http;
    
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class DoctorInputModel : IMapFrom<DoctorDTO>, IMapTo<DoctorDTO>
    {
        [Required]
        //[StringLength(20, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(ResourceSet))]
        public string FirstName { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ResourceSet))]
        //[StringLength(20, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(ResourceSet))]
        public string MiddleName { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ResourceSet))]
        //[StringLength(20, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(ResourceSet))]
        public string LastName { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ResourceSet))]
        //[StringLength(100, ErrorMessageResourceName = "NameIsTooLong", ErrorMessageResourceType = typeof(ResourceSet))]
        public string Specialty { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ResourceSet))]
        //[StringLength(450, ErrorMessageResourceName = "DescriptionIsTooLong", ErrorMessageResourceType = typeof(ResourceSet))]
        public string Description { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ResourceSet))]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ResourceSet))]
        //[EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(ResourceSet))]
        public string Email { get; set; }

        public IFormFile ProfilePicture { get; set; }

        public string Password { get; set; }
    }
}
