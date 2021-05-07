namespace S2Dent.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using S2Dent.Data;
    using S2Dent.Services.Automapper;
    using S2Dent.Services.Interfaces;
    
    public class SpecialitiesService : ISpecialitiesService
    {
        private readonly S2DentDbContext dbContext;

        public SpecialitiesService(S2DentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<T>> GetAll<T>()
        {
            var specialities = dbContext.Specialities.To<T>().ToList();

            return specialities;
        }
    }
}
