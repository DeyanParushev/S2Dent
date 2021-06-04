namespace S2Dent.Tests.ServicesTests.DoctorsService
{
    using System;
    using System.Linq;
    
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.Services;

    public class EditDoctor
    {
        [TestCase("SampleId", true)]
        public void EditDoctorShouldThrowException(string id, bool isDeleted)
        {
            //// Arange
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(nameof(EditDoctorShouldThrowException))
                .Options;
            var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);
            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "SomeFirstName",
                MiddleName = "SomeMiddleName",
                IsDeleted = isDeleted,
            };

            context.Doctors.Add(doctor);
            context.SaveChangesAsync().GetAwaiter().GetResult();

            //// Act
            var testInputDoctor = new Doctor
            {
                Id = id,
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                IsDeleted = false
            };

            var secondTestInputDoctor = new Doctor
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                IsDeleted = doctor.IsDeleted,
            };

            //// Assert
            Assert.That(
                () => service.EditDoctorInfo(testInputDoctor).GetAwaiter().GetResult(),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Doctor does not exist."));
            Assert.That(
                () => service.EditDoctorInfo(secondTestInputDoctor).GetAwaiter().GetResult(),
                Throws.TypeOf<ArgumentException>().With.Message.EqualTo("Doctor does not exist."));
        }

        [TestCase("SampleId", "SomeFirstName", "SomeMiddleName", "SomeThirdName")]
        public void EditDoctorShowldChangeBasicProperties(
            string id, string firstName, string middleName,
            string lastName)
        {
            //// Arange
            var options = new DbContextOptionsBuilder<S2DentDbContext>()
                .UseInMemoryDatabase(nameof(EditDoctorShowldChangeBasicProperties))
                .Options;
            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);
            var doctor = new Doctor
            {
                Id = id,
                FirstName = "John",
                MiddleName = "Doe",
                ThirdName = "Johnson",
                IsDeleted = false,
            };

            context.Doctors.Add(doctor);
            context.SaveChanges();

            //// Act
            var editDoctor = new Doctor
            {
                Id = id,
                FirstName = firstName,
                MiddleName = middleName,
                ThirdName = lastName,
            };

            service.EditDoctorInfo(editDoctor).GetAwaiter().GetResult();

            //// Asert
            var doctorModel = context.Doctors.AsNoTracking().FirstOrDefault(x => x.Id == doctor.Id);

            Assert.AreEqual(editDoctor.Id, doctorModel.Id);
            Assert.AreEqual(editDoctor.FirstName, doctorModel.FirstName);
            Assert.AreEqual(editDoctor.MiddleName, doctorModel.MiddleName);
            Assert.AreEqual(editDoctor.ThirdName, doctorModel.ThirdName);
        }

        [Test]
        public void EditDoctorShouldChangeSpecility()
        {
            //// Arange
            var options = new DbContextOptionsBuilder<S2DentDbContext>()
                .UseInMemoryDatabase(nameof(EditDoctorShouldChangeSpecility))
                .Options;
            using var context = new S2DentDbContext(options);
            var service = new DoctorsService(context);
            var doctor = new Doctor
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                MiddleName = "Doe",
                ThirdName = "Johnson",
                IsDeleted = false,
                Speciality = new Speciality
                {
                    Id = 1,
                    Name = "Doctor"
                }
            };

            var editSpeciality = new Speciality
            {
                Id = 2,
                Name = "Pediatritian",
            };

            context.Specialities.Add(editSpeciality);
            context.Doctors.Add(doctor);
            context.SaveChanges();

            //// Act
            var editDoctor = new Doctor
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                ThirdName = doctor.ThirdName,
                IsDeleted = false,
                Speciality = editSpeciality,
            };

            service.EditDoctorInfo(editDoctor).GetAwaiter().GetResult();
            var resultDoctor = 
                context.Doctors.SingleOrDefault(x => x.Id == editDoctor.Id && x.IsDeleted == false);

            Assert.AreEqual(editSpeciality.Id, resultDoctor.Speciality.Id);
            Assert.AreEqual(editSpeciality.Name, resultDoctor.Speciality.Name);
        }
    }
}
