namespace S2Dent.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InsuranceCompany
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
