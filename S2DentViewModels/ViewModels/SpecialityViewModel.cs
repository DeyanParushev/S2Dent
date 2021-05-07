namespace S2Dent.ViewModels.ViewModels
{
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class SpecialityViewModel : IMapTo<Speciality>, IMapFrom<Speciality>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}