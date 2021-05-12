namespace S2Dent.Tests.ServicesTests.SpecialitiesService
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services;
    using S2Dent.Services.Automapper;
    using S2Dent.ViewModels.InputModels;
    using S2Dent.ViewModels.ViewModels;

    public class GetOneSpeciality
    {
        [Test]
        public void GetOneShouldThrowException(
            [Range(-1, 1)] int intId)
        {
            //// Arrange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
               .UseInMemoryDatabase(nameof(GetOneShouldThrowException))
               .Options;

            using (var context = new S2DentDbContext(options))
            {
                var speciality = new Speciality
                {
                    Id = 2,
                    Name = "Dentist",
                };

                if (!context.Specialities.Any(x => x.Name == speciality.Name))
                {
                    context.Specialities.Add(speciality);
                    context.SaveChanges();
                }
            }

            using (var context = new S2DentDbContext(options))
            {
                var service = new SpecialitiesService(context);

                //// Assert
                Assert.That(() => service.GetOne<SpecialityViewModel>(intId).GetAwaiter().GetResult(), Throws.Exception);
            }
        }

        [Test]
        public void GetOneShouldReturnTheRightType()
        {
            //// Arrange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
               .UseInMemoryDatabase(nameof(GetOneShouldReturnTheRightType))
               .Options;

            using (var context = new S2DentDbContext(options))
            {
                var speciality = new Speciality
                {
                    Id = 2,
                    Name = "Dentist",
                };

                if (!context.Specialities.Any(x => x.Name == speciality.Name))
                {
                    context.Specialities.Add(speciality);
                    context.SaveChanges();
                }
            }

            using (var context = new S2DentDbContext(options))
            {
                //// Act
                var service = new SpecialitiesService(context);
                var viewModel = service.GetOne<SpecialityViewModel>(2).GetAwaiter().GetResult();
                var inputModel = service.GetOne<SpecialityInputModel>(2).GetAwaiter().GetResult();

                //// Assert
                Assert.AreEqual(true, viewModel is SpecialityViewModel);
                Assert.AreEqual(true, inputModel is SpecialityInputModel);
            }
        }

        [TestCase(1, "Anestesiologist")]
        [TestCase(2, "Orthodont")]
        [TestCase(3, "Dentist")]
        [TestCase(4, "DrunkHead")]
        public void GetOneShouldReturnTheRightData(int id, string name)
        {
            //// Arrange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
               .UseInMemoryDatabase(nameof(GetOneShouldReturnTheRightData))
               .Options;

            var speciality = new Speciality
            {
                Id = id,
                Name = name,
            };

            using (var context = new S2DentDbContext(options))
            {
                context.Specialities.Add(speciality);
                context.SaveChanges();
            }

            using (var context = new S2DentDbContext(options))
            {
                //// Act
                var service = new SpecialitiesService(context);
                var resultModel = service.GetOne<SpecialityViewModel>(id).GetAwaiter().GetResult();

                //// Assert
                Assert.AreEqual(speciality.Id, resultModel.Id);
                Assert.AreEqual(speciality.Name, resultModel.Name);
                Assert.AreEqual(true, resultModel is SpecialityViewModel);
            }
        }
    }
}
