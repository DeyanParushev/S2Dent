namespace S2Dent.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Treatment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Diagnosis { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public string StatusId { get; set; }

        public virtual Status Status { get; set; }

        [Required]
        [ForeignKey("Tooth")]
        public string ToothId { get; set; }

        public virtual Tooth Tooth { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public bool IsFinal { get; set; }
    }
}
