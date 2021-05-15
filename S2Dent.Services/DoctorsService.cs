namespace S2Dent.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services.Automapper;
    using S2Dent.Services.Interfaces;

    public class DoctorsService : IDoctorsService
    {
        private readonly S2DentDbContext dbContext;

        public DoctorsService(S2DentDbContext context)
        {
            dbContext = context;
        }

        public async Task<ICollection<T>> GetAll<T>()
        {
            var doctors = await dbContext.Doctors
                .Where(x => x.IsDeleted == false)
                .To<T>()
                .ToListAsync();

            return doctors;
        }

        public async Task<T> GetById<T>(string id)
        {
            CheckDoctorExists(id);

            var doctor = await dbContext.Doctors
                        .Where(x => x.Id == id && x.IsDeleted == false)
                        .To<T>()
                        .SingleOrDefaultAsync();

            return doctor;
        }

        public async Task Create(Doctor doctor, string password)
        {
            doctor.PasswordHash = GetHashedPassword(password);
            dbContext.Doctors.Add(doctor);
            await dbContext.SaveChangesAsync();
        }

        public async Task Edit(Doctor inputDoctor)
        {
            CheckDoctorExists(inputDoctor.Id);

            dbContext.Doctors.Update(inputDoctor);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            CheckDoctorExists(id);

            var doctor = dbContext.Doctors.SingleOrDefault(x => x.Id == id);
            doctor.IsDeleted = true;
            await dbContext.SaveChangesAsync();
        }

        private string GetHashedPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 / 8));

            return hashed;
        }

        private void CheckDoctorExists(string id)
        {
            if (!dbContext.Doctors.Any(x => x.Id == id && x.IsDeleted == false))
            {
                throw new ArgumentException("Doctor does not exist.");
            }
        }
    }
}
