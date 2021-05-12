namespace S2Dent.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Status
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string Abreviation { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
