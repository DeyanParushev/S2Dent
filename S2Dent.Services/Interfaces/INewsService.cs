using System.Collections.Generic;
using System.Threading.Tasks;

namespace S2Dent.Services.Interfaces
{
    public interface INewsService
    {
        Task<ICollection<T>> GetAllNews<T>();
    }
}
