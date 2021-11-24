using S2Dent.Models;
using S2Dent.Services.Automapper;
using System;

namespace S2Dent.DTOs
{
    public class NewsDTO : IMapFrom<News>, IMapTo<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ContentInEnglish { get; set; }

        public string ContentInBulgarian { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

    }
}
