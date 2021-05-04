namespace S2Dent.DTOs
{
    using System.Collections.Generic;
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class DoctorDTO : IMapFrom<Doctor>, IMapTo<Doctor>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string Specialty { get; set; }

        public string PictureUrl { get; set; }

        public ICollection<TreatmentDTO> Treatments { get; set; }
    }
}
