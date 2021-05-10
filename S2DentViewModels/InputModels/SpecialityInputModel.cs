namespace S2Dent.ViewModels.InputModels
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;
    using System.ComponentModel.DataAnnotations;

    public class SpecialityInputModel : IMapFrom<Speciality>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMesssages.RequiredSpeciality)]
        [StringLength(20, ErrorMessage = ErrorMesssages.NameLenght, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
