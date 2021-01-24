namespace S2Dent.DTOs
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class StatusDTO : IMapFrom<Status>, IMapTo<Status>
    {
        public string Id { get; set; }

        public string Abreviation { get; set; }

        public string Name { get; set; }
    }
}