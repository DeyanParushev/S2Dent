namespace S2Dent.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class DoctorInputModel : IMapFrom<Doctor>, IMapTo<Doctor>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorMesssages.RequiredName)]
        [StringLength(20, ErrorMessage = ErrorMesssages.NameLenght, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ErrorMesssages.RequiredMiddleName)]
        [StringLength(20, ErrorMessage = ErrorMesssages.NameLenght, MinimumLength = 3)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = ErrorMesssages.RequiredLastName)]
        [StringLength(20, ErrorMessage = ErrorMesssages.NameLenght, MinimumLength = 3)]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = ErrorMesssages.RequiredSpeciality)]
        public int SpecialityId { get; set; }

        [Required(ErrorMessage = ErrorMesssages.RequiredDescription)]
        [StringLength(450, ErrorMessage = ErrorMesssages.DescriptionLenght, MinimumLength = 10)]
        public string Description { get; set; }

        [Phone(ErrorMessage = ErrorMesssages.Phone)]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = ErrorMesssages.Email)]
        public string Email { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}
