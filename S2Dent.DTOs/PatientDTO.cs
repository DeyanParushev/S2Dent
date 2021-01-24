namespace S2Dent.DTOs
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;
    using System.Collections.Generic;

    public class PatientDTO : IMapFrom<Patient>, IMapTo<Patient>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string IdentityNumber { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string MedicalBookNumber { get; set; }

        public bool AdditionalInsurance { get; set; }

        public InsuranceCompanyDTO InsuranceCompany { get; set; }

        public double InsuranceLimit { get; set; }

        public ICollection<Tooth> Teeth { get; set; }
    }
}