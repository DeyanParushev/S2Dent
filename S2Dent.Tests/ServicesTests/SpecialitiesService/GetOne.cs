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

    public class GetOne
    {
        [Test]
        public void GetOntShouldThrowException(
            [Range(-1, 1)] int intId)
        {
            //// Arrange
            AutoMapperConfig.RegisterMappings(
                typeof(SpecialityViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
               .UseInMemoryDatabase(nameof(GetOntShouldThrowException))
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
                //// Assert
                var service = new SpecialitiesService(context);

                Assert.That(() => service.GetOne<SpecialityViewModel>(intId).GetAwaiter().GetResult(), Throws.Exception);
            }
        }
    }
}
