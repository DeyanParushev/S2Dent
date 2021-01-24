namespace S2Dent.Services
{
    using System.Threading.Tasks;
    
    using S2Dent.Data;

    public class DoctorsService
    {
        private readonly S2DentDbContext context;

        public DoctorsService(S2DentDbContext context)
        {
            this.context = context;
        }

        public async Task GetAllDoctors<T>()
        {

        }
    }
}
