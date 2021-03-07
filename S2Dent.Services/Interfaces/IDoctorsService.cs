namespace S2Dent.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;


    public interface IDoctorsService
    {
        public Task CreateDoctor(DoctorDTO doctor, string password);
        
        public Task DeleteDoctor(string id);
        
        public Task<ICollection<T>> GetAllDoctors<T>();
        
        public Task<T> GetDoctorById<T>(string id);
        
        public Task UpdateDoctor(DoctorDTO inputDoctor);
    }
}
