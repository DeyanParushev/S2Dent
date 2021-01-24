namespace S2Dent.DTOs
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class AssisstantDTO : IMapFrom<Assisstant>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string ThirdName { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public bool IsDeleted { get; set; }
    }
}
