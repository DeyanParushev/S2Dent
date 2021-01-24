namespace S2Dent.DTOs
{
    using System.Collections.Generic;
    
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class ToothDTO : IMapFrom<Tooth>, IMapTo<Tooth>
    {
        public string Id { get; set; }

        public int Number { get; set; }


        public virtual StatusDTO Status { get; set; }

        public virtual ICollection<TreatmentDTO> Treatments { get; set; }
    }
}