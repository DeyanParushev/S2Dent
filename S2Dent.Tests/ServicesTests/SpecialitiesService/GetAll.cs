namespace S2Dent.Tests.ServicesTests.SpecialitiesService
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using S2Dent.Data;
    using S2Dent.Services.Automapper;
    using S2Dent.Services;
    using S2Dent.ViewModels.ViewModels;
    using S2Dent.ViewModels.InputModels;
    using S2Dent.Models;

    public class GetAll
    {
        [Test]
        public void GetAllShouldReturnIcollectionViewModel()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(GetAllShouldReturnIcollectionViewModel))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);
            var specialityModels = new List<Speciality>()
            {
                new Speciality {Id = 1, Name = "Dentist"},
                new Speciality{Id = 2, Name = "Anesthesiologist"},
            };

            context.Specialities.AddRange(specialityModels);
            context.SaveChanges();

            //// Act
            var resultSpecialities = service.GetAll<SpecialityViewModel>().GetAwaiter().GetResult();

            //// Assert
            Assert.AreEqual(true, resultSpecialities is ICollection<SpecialityViewModel>);
        }

        [Test]
        public void GetAllShouldReturnIcollectionInputModel()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(GetAllShouldReturnIcollectionInputModel))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);

            var specialityModels = new List<Speciality>
            {
                new Speciality {Id = 1, Name = "Dentist"},
                new Speciality{Id = 2, Name = "Anesthesiologist"},
            };

            context.Specialities.AddRange(specialityModels);
            context.SaveChanges();

            //// Act
            var resultSpecialities = service.GetAll<SpecialityInputModel>().GetAwaiter().GetResult();

            //// Assert
            Assert.AreEqual(true, resultSpecialities is ICollection<SpecialityInputModel>);
        }

        [Test]
        public void GetAllShouldWorkProperly()
        {
            //// Arrange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
               .UseInMemoryDatabase(nameof(GetAllShouldWorkProperly))
               .Options;
            var specialityModels = new List<Speciality>()
            {
                new Speciality { Id = 1, Name = "Anesthesiologist", IsDeleted = false },
                new Speciality { Id = 2, Name = "Orthodontologist", IsDeleted = false },
                new Speciality { Id = 3, Name = "Dentists", IsDeleted = true },
            };

            using (var context = new S2DentDbContext(options))
            {
                context.Specialities.AddRange(specialityModels);
                context.SaveChanges();
            }

            using (var context = new S2DentDbContext(options))
            {
                //// Assert
                var service = new SpecialitiesService(context);
                var specialities = service.GetAll<SpecialityViewModel>().GetAwaiter().GetResult();

                Assert.AreEqual(specialityModels[0].Name, specialities.FirstOrDefault(x => x.Id == 1).Name);
                Assert.AreEqual(specialityModels[1].Name, specialities.FirstOrDefault(x => x.Id == 2).Name);
                Assert.AreEqual(2, specialities.Count());
            }
        }
    }
}
