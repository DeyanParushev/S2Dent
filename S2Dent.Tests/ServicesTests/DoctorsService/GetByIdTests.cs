namespace S2Dent.Tests
{
    using System;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services;
    using S2Dent.Services.Automapper;
    using S2Dent.ViewModels.InputModels;
    using S2Dent.ViewModels.ViewModels;

    public class GetByIdTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("John", "Doe", "Johnson", "testEmail@email.com", "testDescpription", 1, "testSpeciality", "testPhone")]
        [TestCase("Arthur", "Clark", "Spencer", "testEmail@email2.com", "testDescpription2", 2, "testSpeciality2", "testPhone2")]
        [TestCase("Aizuk", "Azimof", "Ivanov", "testEmail@email3.com", "testDescpription3", 3, "testSpeciality3", "testPhone3")]
        public void GetByIdShouldReturnInputModelProperly(
            string firstName, string middleName,
            string lastName, string email,
            string description, int speciality,
            string specailityName, string phone)
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                 typeof(DoctorViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(databaseName: "FakeConnectionString")
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);

            var specialityModel = new Speciality { Id = speciality, Name = specailityName };

            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = firstName,
                MiddleName = middleName,
                ThirdName = lastName,
                Email = email,
                Description = description,
                Speciality = specialityModel,
                PhoneNumber = phone,
            };

            context.Doctors.Add(doctor);
            context.Specialities.Add(specialityModel);
            context.SaveChanges();

            //// Act
            var resultDoctor = service.GetDoctorById<DoctorInputModel>(doctor.Id).GetAwaiter().GetResult();

            //// Assert
            Assert.AreEqual(doctor.FirstName, resultDoctor.FirstName);
            Assert.AreEqual(doctor.MiddleName, resultDoctor.MiddleName);
            Assert.AreEqual(doctor.ThirdName, resultDoctor.ThirdName);
            Assert.AreEqual(doctor.Email, resultDoctor.Email);
            Assert.AreEqual(doctor.Description, resultDoctor.Description);
            Assert.AreEqual(doctor.Speciality.Id, resultDoctor.SpecialityId);
            Assert.AreEqual(doctor.PhoneNumber, resultDoctor.PhoneNumber);
            Assert.AreEqual(doctor.Id, resultDoctor.Id);
            Assert.AreEqual(typeof(DoctorInputModel), resultDoctor.GetType());

        }

        [TestCase("John", "Doe", "Johnson", "testEmail@email.com", "testDescpription", "Anestesiologist", "testURL")]
        [TestCase("Arthur", "Clark", "Spencer", "testEmail@email2.com", "testDescpription2", "Orthodont", "testURL2")]
        [TestCase("Aizuk", "Azimof", "Ivanov", "testEmail@email3.com", "testDescpription3", "Technician", "testURL3")]
        public void GetByIdShouldReturnViewModelProperly(
        string firstName, string middleName,
        string lastName, string email,
        string description, string speciality,
        string pictureUrl)
        {
            //// Arange
            AutoMapperConfig.RegisterMappings(
                 typeof(DoctorViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(databaseName: "FakeConnectionString")
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);

            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = firstName,
                MiddleName = middleName,
                ThirdName = lastName,
                Email = email,
                Description = description,
                Speciality = new Speciality
                {
                    Name = speciality,
                },
                PictureUrl = pictureUrl,
            };

            context.Doctors.Add(doctor);
            context.SaveChanges();

            //// Act
            var resultDoctor = service.GetDoctorById<DoctorViewModel>(doctor.Id).GetAwaiter().GetResult();

            //// Assert
            Assert.AreEqual(doctor.FirstName, resultDoctor.FirstName);
            Assert.AreEqual(doctor.MiddleName, resultDoctor.MiddleName);
            Assert.AreEqual(doctor.ThirdName, resultDoctor.ThirdName);
            Assert.AreEqual(doctor.Email, resultDoctor.Email);
            Assert.AreEqual(doctor.Description, resultDoctor.Description);
            Assert.AreEqual(doctor.Speciality.Name, resultDoctor.Speciality.Name);
            Assert.AreEqual(doctor.PictureUrl, resultDoctor.PictureUrl);
            Assert.AreEqual(doctor.Id, resultDoctor.Id);
            Assert.AreEqual(typeof(DoctorViewModel), resultDoctor.GetType());

        }

        [TestCase("testId")]
        public void GetByIdShouldThrowErrorWithInvalidId(string id)
        {
            AutoMapperConfig.RegisterMappings(
                 typeof(DoctorViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(databaseName: "FakeConnectionString")
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);

            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = false,
            };

            context.Doctors.Add(doctor);
            context.SaveChanges();

            Assert.That(() => service.GetDoctorById<DoctorViewModel>(id).GetAwaiter().GetResult(), Throws.Exception);

        }

        [Test]
        public void GetByIdShouldThrowErrorWhenEntityIsDeleted()
        {
            AutoMapperConfig.RegisterMappings(
                 typeof(DoctorViewModel).GetTypeInfo().Assembly);

            var options = new DbContextOptionsBuilder<S2DentDbContext>()
              .UseInMemoryDatabase(databaseName: "FakeConnectionString")
              .Options;

            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);

            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = true,
            };

            context.Doctors.AddAsync(doctor);
            context.SaveChangesAsync();

            Assert.That(() => service.GetDoctorById<DoctorViewModel>(doctor.Id).GetAwaiter().GetResult(), Throws.Exception);
        }
    }
}