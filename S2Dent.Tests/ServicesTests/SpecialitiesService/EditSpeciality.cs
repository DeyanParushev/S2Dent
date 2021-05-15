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

    public class EditSpeciality
    {
        [Test]
        public void EditShouldThrowError()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>().UseInMemoryDatabase(nameof(EditShouldThrowError)).Options;
            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);

            var speciality = new Speciality { Id = 1, Name = "Dentist", IsDeleted = false };
            context.Specialities.Add(speciality);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            var editSpeciality = new Speciality { Id = 2, Name = "Anesthesiologis", IsDeleted = false };

            //// Assert
            Assert.That(() =>
            service.Edit(editSpeciality).GetAwaiter().GetResult(),
            Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Speciality does not exist."));
        }

        [TestCase("Anesthesiologist", true)]
        public void EditShouldWorkProperly(string specialityName, bool isDeleted)
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>().UseInMemoryDatabase(nameof(EditShouldWorkProperly)).Options;
            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);

            var speciality = new Speciality { Id = 1, Name = "Dentist", IsDeleted = false };

            context.Specialities.Add(speciality);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            var editSpeciality = new Speciality { Id = 1, Name = specialityName, IsDeleted = isDeleted };

            //// Act
            service.Edit(editSpeciality).GetAwaiter().GetResult();

            //// Assert 
            var specialityModel = context.Specialities.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual(specialityName, specialityModel.Name);
            Assert.AreEqual(isDeleted, specialityModel.IsDeleted);
        }
    }
}
