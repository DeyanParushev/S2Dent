using System;
using System.ComponentModel.DataAnnotations;

namespace S2Dent.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TitleInBulgarian { get; set; }

        [Required]
        public string TitleInEnglish { get; set; }

        [Required]
        public string ContentInEnglish { get; set; }

        [Required]
        public string ContentInBulgarian { get; set;}

        public DateTime Date { get; set; }

        public string DescriptionInEnglish { get; set; }

        public string DescriptionInBulgarian { get; set; }

        public string PictureUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}
