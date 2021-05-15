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
    using S2Dent.ViewModels.ViewModels;

    public class DeleteSpeciality
    {
        [Test]
        public void DeleteShouldWorkProperly()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(typeof(SpecialityViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<S2DentDbContext>()
                .UseInMemoryDatabase(nameof(DeleteShouldWorkProperly))
                .Options;
            var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);
            var speciality = new Speciality { Id = 1, Name = "Doctor", IsDeleted = false };
            context.Specialities.Add(speciality);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            //// Act
            service.Delete(1).GetAwaiter().GetResult();

            //// Assert
            var specialityModel = context.Specialities.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual(true, specialityModel.IsDeleted);
        }

        [Test]
        public void DeleteShouldThrowException()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(typeof(SpecialityViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<S2DentDbContext>()
                .UseInMemoryDatabase(nameof(DeleteShouldThrowException))
                .Options;
            var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);
            var speciality = new Speciality { Id = 1, Name = "Doctor", IsDeleted = false };
            context.Specialities.Add(speciality);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            //// Assert
            Assert.That(
                () => service.Delete(2).GetAwaiter().GetResult(),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Speciality does not exist."));
        }
    }
}
