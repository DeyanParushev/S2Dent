namespace S2Dent.Services.Interfaces
{
    using S2Dent.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IDoctorsService
    {
        public Task Create(Doctor doctor, string password);
        
        public Task Delete(string id);
        
        public Task<ICollection<T>> GetAll<T>();
        
        public Task<T> GetById<T>(string id);
        
        public Task EditDoctorInfo(Doctor inputDoctor);
    }
}
