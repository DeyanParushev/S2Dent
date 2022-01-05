using S2Dent.Models;
using S2Dent.Services.Automapper;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace S2Dent.ViewModels.InputModels
{
    public class NewsInputModel : IMapTo<News>, IMapFrom<News>
    {
        [Required]
        [DisplayName("Title in Bulgarian")]
        [StringLength(120, ErrorMessage = ErrorMesssages.NameLenght, MinimumLength = 3)]
        public string TitleInBulgarian { get; set; }

        [Required]
        [DisplayName("Title in English")]
        [StringLength(120, ErrorMessage = ErrorMesssages.NameLenght, MinimumLength = 3)]
        public string TitleInEnglish { get; set; }

        [Required]
        [DisplayName("Content in Bulgarian")]
        [StringLength(450, ErrorMessage = ErrorMesssages.DescriptionLenght, MinimumLength = 10)]
        public string ContentInBulgarian { get; set; }
        
        [Required]
        [DisplayName("Content in English")]
        [StringLength(450, ErrorMessage = ErrorMesssages.DescriptionLenght, MinimumLength = 10)]
        public string ContentInEnglish { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [DisplayName("Description in Bulgarian")]
        [StringLength(450, ErrorMessage = ErrorMesssages.DescriptionLenght, MinimumLength = 10)]
        public string DescriptionInBulgarian { get; set; }

        [DisplayName("Description in English")]
        [StringLength(450, ErrorMessage = ErrorMesssages.DescriptionLenght, MinimumLength = 10)]
        public string DescriptionInEnglish { get; set; }
       
        public string PictureUrl { get; set; }
    }
}
