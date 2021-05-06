namespace S2Dent.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using S2Dent.DTOs;
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class DoctorInputModel : IMapFrom<Doctor>, IMapTo<Doctor>
    {
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(20, ErrorMessage = "NameLenght", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(20, ErrorMessage = "NameLenght", MinimumLength = 3)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(20, ErrorMessage = "NameLenght", MinimumLength = 3)]
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
