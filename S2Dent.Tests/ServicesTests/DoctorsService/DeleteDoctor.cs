namespace S2Dent.Tests.ServicesTests.DoctorsService
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    
    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services;

    public class DeleteDoctor
    {
        [TestCase("SomeSampleId", true)]
        public void DeleteShouldTrhowException(string id, bool isDeleted )
        {
            //// Arrange
            var options = new DbContextOptionsBuilder<S2DentDbContext>()
                .UseInMemoryDatabase(nameof(DeleteShouldTrhowException))
                .Options;
            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);
            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "TestDoctor",
                MiddleName = "TestDoctors",
                ThirdName = "FamilyName",
                IsDeleted = isDeleted,
            };
            context.Doctors.Add(doctor);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            //// Assert
            Assert.That(
                () => service.Delete(id).GetAwaiter().GetResult(),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Doctor does not exist."));
            Assert.That(
                () => service.Delete(doctor.Id).GetAwaiter().GetResult(),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Doctor does not exist."));
        }

        [Test]
        public void DeleteDoctorShouldWorkProperly()
        {
            //// Arrange
            var options = new DbContextOptionsBuilder<S2DentDbContext>()
                .UseInMemoryDatabase(nameof(DeleteShouldTrhowException))
                .Options;
            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);
            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "TestDoctor",
                MiddleName = "TestDoctors",
                ThirdName = "FamilyName",
                IsDeleted = false,
            };
            context.Doctors.Add(doctor);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            //// Act
            service.Delete(doctor.Id).GetAwaiter().GetResult();

            //// Assert
            var doctorModel = context.Doctors.FirstOrDefault(x => x.Id == doctor.Id);
            Assert.AreEqual(true, doctorModel.IsDeleted);
        }
    }
}
