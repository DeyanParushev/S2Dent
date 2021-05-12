using System.Collections.Generic;
using System.Threading.Tasks;

namespace S2Dent.Services.Interfaces
{
    public interface ISpecialitiesService
    {
        public Task<ICollection<T>> GetAll<T>();
        Task<T> GetOne<T>(int id);
    }
}
