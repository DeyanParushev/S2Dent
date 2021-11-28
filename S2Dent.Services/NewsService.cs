using S2Dent.Data;
using S2Dent.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using S2Dent.Services.Automapper;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using S2Dent.Models;
using System;

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
            var news = await this.dbContext.News
                .Where(x => x.IsDeleted == false)
                .Select(x => x)
                .OrderByDescending(x => x.Date)
                .To<T>()
                .ToListAsync();

            return news;
        }

        public async Task Create(News news)
        {
            await this.dbContext.News.AddAsync(news);
            this.dbContext.SaveChanges();
        }

        public async Task EditNews(News newsInputModel)
        {
            this.ThrowIfNull(newsInputModel.Id);

            var oldNews = await this.dbContext.News.FirstOrDefaultAsync(x => x.Id == newsInputModel.Id && x.IsDeleted == false);

            oldNews.Date = newsInputModel.Date;
            oldNews.DescriptionInEnglish = newsInputModel.DescriptionInEnglish;
            oldNews.DescriptionInBulgarian= newsInputModel.DescriptionInBulgarian;
            oldNews.ContentInBulgarian = newsInputModel.ContentInBulgarian; 
            oldNews.ContentInEnglish = newsInputModel.ContentInEnglish;
            oldNews.TitleInEnglish = newsInputModel.TitleInEnglish;
            oldNews.TitleInBulgarian = newsInputModel.TitleInBulgarian;
            oldNews.PictureUrl = newsInputModel.PictureUrl;

            this.dbContext.SaveChanges();
        }

        public async Task DeleteNews(int id)
        {
            this.ThrowIfNull(id);

            var news = await this.dbContext.News.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            news.IsDeleted = true;

            this.dbContext.SaveChanges();
        }

        private void ThrowIfNull(int id)
        {
            var newsObject = this.dbContext.News.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            if (newsObject == null)
            {
                throw new ArgumentNullException("News item does not exist.");
            }
        }
    }
}
