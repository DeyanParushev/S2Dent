namespace S2Dent.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Patient
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ThirdName { get; set; }

        [Required]
        [MaxLength(10)]
        public string IdentityNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [Required]
        public string MedicalBookNumber { get; set; }

        public bool AdditionalInsurance { get; set; }

        public string InsuranceCompanyId { get; set; }

        public virtual InsuranceCompany InsuranceCompany { get; set; }

        public virtual ICollection<Tooth> Teeth { get; set; }
    }
}
