namespace S2Dent.Services.Interfaces
{
    using S2Dent.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDoctorsService
    {
        public Task CreateDoctor(Doctor doctor, string password);
        
        public Task DeleteDoctor(string id);
        
        public Task<ICollection<T>> GetAllDoctors<T>();
        
        public Task<T> GetDoctorById<T>(string id);
        
        public Task UpdateDoctor(Doctor inputDoctor);
    }
}
