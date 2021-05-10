namespace S2Dent.Tests.ServicesTests.DoctorsService
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services;
    using S2Dent.Services.Automapper;
    using S2Dent.ViewModels.ViewModels;

    public class GetAllDoctors
    {
        [Test]
        public async Task GetAllShouldReturnICollection()
        {
            //// Arange
            var context = SetupInMemoryContext<DoctorViewModel>();
            var service = new DoctorsService(context);

            var inputDoctors = new List<Doctor>()
            {
                new Doctor{FirstName = "Ivan", ThirdName = "Zlatev"},
                new Doctor{FirstName = "Peshi", ThirdName = "Georgiev"},
            };

            await context.Doctors.AddRangeAsync(inputDoctors);
            await context.SaveChangesAsync();

            //// Act
            var resultDoctors = await service.GetAllDoctors<DoctorViewModel>();

            //// Assert
            Assert.AreEqual(true, resultDoctors is ICollection<DoctorViewModel>);
        }

        [Test]
        public async Task GetAllShouldReturnCorrectData()
        {
            //// Arange
            var context = SetupInMemoryContext<DoctorViewModel>();
            var service = new DoctorsService(context);

            var inputDoctors = new List<Doctor>()
            {
                new Doctor{FirstName = "Ivan", ThirdName = "Zlatev"},
                new Doctor{FirstName = "Peshi", ThirdName = "Georgiev"},
            };

            await context.Doctors.AddRangeAsync(inputDoctors);
            await context.SaveChangesAsync();

            //// Act
            var resultDoctors = await service.GetAllDoctors<DoctorViewModel>();
            var counter = 0;

            //// Assert
            foreach (var resultDoctor in resultDoctors)
            {
                Assert.AreEqual(inputDoctors[counter].FirstName, resultDoctor.FirstName);
                Assert.AreEqual(inputDoctors[counter].ThirdName, resultDoctor.ThirdName );
                counter++;
            }
        }

        private S2DentDbContext SetupInMemoryContext<T>()
        {
            //// Registers the reflection automapping configuration
            AutoMapperConfig.RegisterMappings(
                typeof(T).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(databaseName: "FakeConnectionString")
              .Options;

            var context = new S2DentDbContext(options);

            return context;
        }
    }
}
