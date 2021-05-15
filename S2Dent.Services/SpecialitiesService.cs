namespace S2Dent.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using S2Dent.Data;
    using S2Dent.Models;
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
                throw new ArgumentException("Speciality does not exist.");
            }

            return speciality;
        }

        public async Task Create(Speciality speciality)
        {
            if(dbContext.Specialities.Any(x => x.Name.ToLower() == speciality.Name.ToLower() || x.Id == speciality.Id))
            {
                throw new ArgumentException("Speciality already exists.");
            }

            await dbContext.Specialities.AddAsync(speciality);
            await dbContext.SaveChangesAsync();
        }

        public async Task Edit(Speciality speciality)
        {
            var specialityModel = await dbContext.Specialities.SingleOrDefaultAsync(x => x.Id == speciality.Id);

            if(specialityModel == null)
            {
                throw new ArgumentException("Speciality does not exist.");
            }

            specialityModel.Name = speciality.Name;
            specialityModel.IsDeleted = speciality.IsDeleted;
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int specialityId)
        {
            var speciality = await dbContext.Specialities.SingleOrDefaultAsync(x => x.Id == specialityId && x.IsDeleted == false);

            if(speciality == null)
            {
                throw new ArgumentException("Speciality does not exist.");
            }

            speciality.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }
    }
}
