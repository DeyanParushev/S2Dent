namespace S2Dent.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DoctorAssisstant
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [Required]
        public string AssisstantId { get; set; }

        public virtual Assisstant Assisstant { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Treatment")]
        public string TreatmentId { get; set; }
    }
}
