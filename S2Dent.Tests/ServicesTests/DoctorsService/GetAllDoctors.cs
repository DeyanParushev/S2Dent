namespace S2Dent.Tests.ServicesTests.DoctorsService
{
    using System.Collections.Generic;
    using System.Reflection;

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
        public void GetAllShouldReturnICollection()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                typeof(DoctorViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(GetAllShouldReturnICollection))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);

            var inputDoctors = new List<Doctor>()
            {
                new Doctor{FirstName = "Ivan", ThirdName = "Zlatev"},
                new Doctor{FirstName = "Peshi", ThirdName = "Georgiev"},
            };

            context.Doctors.AddRange(inputDoctors);
            context.SaveChanges();

            //// Act
            var resultDoctors = service.GetAll<DoctorViewModel>().GetAwaiter().GetResult();

            //// Assert
            Assert.AreEqual(true, resultDoctors is ICollection<DoctorViewModel>);
        }

        [Test]
        public void GetAllShouldReturnCorrectData()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                 typeof(DoctorViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(GetAllShouldReturnCorrectData))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);

            var inputDoctors = new List<Doctor>()
            {
                new Doctor{FirstName = "Ivan", ThirdName = "Zlatev"},
                new Doctor{FirstName = "Peshi", ThirdName = "Georgiev"},
            };

            context.Doctors.AddRange(inputDoctors);
            context.SaveChanges();

            //// Act
            var resultDoctors = service.GetAll<DoctorViewModel>().GetAwaiter().GetResult();
            var counter = 0;

            //// Assert
            foreach (var resultDoctor in resultDoctors)
            {
                Assert.AreEqual(inputDoctors[counter].FirstName, resultDoctor.FirstName);
                Assert.AreEqual(inputDoctors[counter].ThirdName, resultDoctor.ThirdName);
                counter++;
            }
        }
    }
}
