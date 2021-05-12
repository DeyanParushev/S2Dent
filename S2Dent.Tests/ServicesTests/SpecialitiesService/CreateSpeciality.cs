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

    public class CreateSpeciality
    {
        [TestCase("TestSpeciality")]
        [TestCase("TestSpeciality2")]
        [TestCase("TestSpeciality3")]
        [TestCase("TestSpeciality4")]
        public void CreateShouldCreateOject(string name)
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(CreateShouldCreateOject))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);
            var speciality = new Speciality { Name = name };

            //// Act
            service.Create(speciality).GetAwaiter().GetResult();

            //// Assert
            Assert.AreEqual(true, context.Specialities.Any(x => x.Name == speciality.Name));
        }

        [Test]
        public void CreateShouldThrowErrorWithSameName()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(CreateShouldThrowErrorWithSameName))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);
            var speciality = new Speciality { Name = "TestName" };

            context.Specialities.Add(speciality);
            context.SaveChanges();

            //// Act
            ;

            //// Assert
            Assert.That(() => 
            service.Create(speciality).GetAwaiter().GetResult(),
            Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Speciality already exists."));
        }

        [Test]
        public void CreateShouldThrowErrorWithSameId()
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(nameof(CreateShouldThrowErrorWithSameName))
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new SpecialitiesService(context);
            var speciality = new Speciality { Id = 1, Name = "TestName" };

            context.Specialities.Add(speciality);
            context.SaveChanges();

            //// Act
            var testSpeciality = new Speciality { Id = 1, Name = "TestSpeciality" };


            //// Assert
            Assert.That(() => 
            service.Create(testSpeciality).GetAwaiter().GetResult(), 
            Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Speciality already exists."));
        }
    }
}
