namespace S2Dent.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
   
    using S2Dent.Models;

    public class S2DentDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public S2DentDbContext()
        {
        }

        public S2DentDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Assisstant> Assisstants { get; set; }

        public DbSet<DoctorAssisstant> DoctorsAssisstants { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Tooth> Teeth { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DoctorAssisstant>()
                .HasOne(x => x.Assisstant)
                .WithMany(x => x.DoctorsAssistants)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
