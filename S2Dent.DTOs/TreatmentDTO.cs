namespace S2Dent.DTOs
{
    using System;
    
    using S2Dent.Models;
    using S2Dent.Services.Automapper;

    public class TreatmentDTO : IMapFrom<Treatment>, IMapTo<Treatment>
    {
        public string Id { get; set; }

        public string Diagnosis { get; set; }

        public StatusDTO Status { get; set; }

        public DoctorDTO Doctor { get; set; }

        public ToothDTO Tooth { get; set; }

        public PatientDTO Patient { get; set; }

        public DateTime Date { get; set; }

        public bool IsFinal { get; set; }
    }
}