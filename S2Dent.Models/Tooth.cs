namespace S2Dent.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tooth
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int Number { get; set; }

        public string StatusId { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
