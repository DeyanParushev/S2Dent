using S2Dent.Data;
using S2Dent.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using S2Dent.Services.Automapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace S2Dent.Services
{
    public class NewsService : INewsService
    {
        private S2DentDbContext dbContext;

        public NewsService(S2DentDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<ICollection<T>> GetAllNews<T>()
        {
            var news = await this.dbContext.News.Select(x => x).OrderByDescending(x => x.Date).To<T>().ToListAsync();

            return news;
        }
    }
}
