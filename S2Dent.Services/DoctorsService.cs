namespace S2Dent.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    
    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services.Automapper;
    using S2Dent.Services.Interfaces;

    public class DoctorsService : IDoctorsService
    {
        private readonly S2DentDbContext dbContext;

        public DoctorsService(S2DentDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<ICollection<T>> GetAllDoctors<T>()
        {
            var doctors = this.dbContext.Doctors
                .Where(x => x.IsDeleted == false)
                .To<T>()
                .ToHashSet();

            return doctors;
        }

        public async Task<T> GetDoctorById<T>(string id)
        {
            this.CheckDoctorExists(id);

            var doctor = this.dbContext.Doctors
                        .Where(x => x.Id == id && x.IsDeleted == false)
                        .To<T>()
                        .SingleOrDefault();

            return doctor;
        }

        public async Task CreateDoctor(Doctor doctor, string password)
        {
            doctor.PasswordHash = this.GetHashedPassword(password);
            this.dbContext.Doctors.Add(doctor);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdateDoctor(Doctor inputDoctor)
        {
            this.CheckDoctorExists(inputDoctor.Id);

            this.dbContext.Doctors.Update(inputDoctor);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteDoctor(string id)
        {
            this.CheckDoctorExists(id);

            var doctor = this.dbContext.Doctors.SingleOrDefault(x => x.Id == id);
            doctor.IsDeleted = true;
            await this.dbContext.SaveChangesAsync();
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
            if (this.dbContext.Doctors.Any(x => x.Id == id && x.IsDeleted == false))
            {
                throw new ArgumentException("Doctor does not exist.");
            }
        }
    }
}
