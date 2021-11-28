using S2Dent.Models;
using S2Dent.Services.Automapper;
using System;

namespace S2Dent.DTOs
{
    public class NewsDTO : IMapFrom<News>, IMapTo<News>
    {
        public int Id { get; set; }

        public string TitleInBulgarian { get; set; }

        public string TitleInEnglish { get; set; }

        public string ContentInEnglish { get; set; }

        public string ContentInBulgarian { get; set; }

        public DateTime Date { get; set; }

        public string DescriptionInEnglish { get; set; }

        public string DescriptionInBulgarian { get; set; }

        public string PictureUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}
