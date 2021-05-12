namespace S2Dent.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

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
            var specialities = await dbContext.Specialities
                .Where(x => x.IsDeleted == false)
                .To<T>()
                .ToListAsync();

            return specialities;
        }

        public async Task<T> GetOne<T>(int id)
        {
            var speciality = await dbContext.Specialities
                .Where(x => x.Id == id)
                .To<T>()
                .SingleOrDefaultAsync();

            if(speciality == null)
            {
                throw new ArgumentNullException("Speciality does not exist.");
            }

            return speciality;
        }
    }
}
