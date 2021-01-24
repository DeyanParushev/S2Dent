namespace S2Dent.DTOs
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class InsuranceCompanyDTO : IMapFrom<InsuranceCompany>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}