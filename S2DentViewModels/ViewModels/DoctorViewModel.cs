﻿namespace S2Dent.ViewModels.ViewModels
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class DoctorViewModel : IMapFrom<Doctor>, IMapTo<Doctor>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string ThirdName { get; set; }

        public string Email { get; set; }

        public SpecialityViewModel Speciality { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
    }
}
