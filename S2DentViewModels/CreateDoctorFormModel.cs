namespace S2Dent.ViewModels
{
    using System.Collections.Generic;
    
    using S2Dent.ViewModels.InputModels;
    using S2Dent.ViewModels.ViewModels;

    public class CreateDoctorFormModel
    {
        public DoctorInputModel Doctor { get; set; }

        public ICollection<SpecialityViewModel> Specialities { get; set; }
    }
}
