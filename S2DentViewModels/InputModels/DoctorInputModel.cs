namespace S2Dent.ViewModels.InputModels
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class DoctorInputModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [StringLength(20, ErrorMessageResourceName = "Required", ErrorMessage = "NameIsTooLong")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [StringLength(20, ErrorMessageResourceName = "Required", ErrorMessage = "NameIsTooLong")]
        public string MiddleName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [StringLength(20, ErrorMessageResourceName = "Required", ErrorMessage = "NameIsTooLong")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [StringLength(20, ErrorMessageResourceName = "Required", ErrorMessage = "FieldIsNull")]
        public string Specialty { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [StringLength(450, ErrorMessageResourceName = "Required", ErrorMessage = "FieldIsNull")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [StringLength(15, ErrorMessageResourceName = "Required", ErrorMessage = "FieldIsNull")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessage = "Required")]
        [EmailAddress(ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        public IFormFile ProfilePicture { get; set; }

    }
}
