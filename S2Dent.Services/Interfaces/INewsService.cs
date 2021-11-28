using S2Dent.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S2Dent.Services.Interfaces
{
    public interface INewsService
    {
        Task Create(News news);
        public Task EditNews(News newsInputModel);
        
        public Task<ICollection<T>> GetAllNews<T>();
    }
}
