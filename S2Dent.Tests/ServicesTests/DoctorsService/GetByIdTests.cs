namespace S2Dent.Tests
{
    using System;
    using System.Reflection;

    using System.Threading.Tasks;

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
        public async Task GetByIdShouldReturnInputModelProperly(
            string firstName, string middleName,
            string lastName, string email,
            string description, int speciality,
            string specailityName, string phone)
        {
            //// Arange
            var context = SetupInMemoryContext<DoctorInputModel>();

            var doctorsService = new DoctorsService(context);
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
            await context.SaveChangesAsync();

            //// Act
            var resultDoctor = await doctorsService.GetDoctorById<DoctorInputModel>(doctor.Id);

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
        public async Task GetByIdShouldReturnViewModelProperly(
        string firstName, string middleName,
        string lastName, string email,
        string description, string speciality,
        string pictureUrl)
        {
            //// Arange
            var context = SetupInMemoryContext<DoctorViewModel>();

            var doctorsService = new DoctorsService(context);

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
            await context.SaveChangesAsync();

            //// Act
            var resultDoctor = await doctorsService.GetDoctorById<DoctorViewModel>(doctor.Id);

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
        public async Task GetByIdShouldThrowErrorWithInvalidId(string id)
        {
            var context = SetupInMemoryContext<DoctorViewModel>();
            var service = new DoctorsService(context);

            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = false,
            };

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();

            Assert.That(async () => await service.GetDoctorById<DoctorViewModel>(id), Throws.Exception);
        }

        [Test]
        public async Task GetByIdShouldThrowErrorWhenEntityIsDeleted()
        {
            var context = SetupInMemoryContext<DoctorViewModel>();
            var service = new DoctorsService(context);

            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = true,
            };

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();

            Assert.That(
                async () => await service.GetDoctorById<DoctorViewModel>(doctor.Id), Throws.Exception);
        }

        private S2DentDbContext SetupInMemoryContext<T>()
        {
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