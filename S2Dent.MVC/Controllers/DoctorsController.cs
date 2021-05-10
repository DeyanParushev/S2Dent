namespace S2Dent.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using S2Dent.Models;
    using S2Dent.MVC.Areas.Identity;
    using S2Dent.Services.Interfaces;
    using S2Dent.ViewModels;
    using S2Dent.ViewModels.InputModels;
    using S2Dent.ViewModels.ViewModels;

    public class DoctorsController : Controller
    {
        private readonly IDoctorsService doctorsService;
        private readonly ISpecialitiesService specialitiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public DoctorsController(
            IDoctorsService doctorsService,
            ISpecialitiesService specialitiesService,
            UserManager<ApplicationUser> userManager)
        {
            this.doctorsService = doctorsService;
            this.specialitiesService = specialitiesService;
            this.userManager = userManager;
        }

        [Route("/Team")]
        public async Task<IActionResult> Team()
        {
            var doctors = await doctorsService.GetAllDoctors<DoctorViewModel>();
            return View(doctors);
        }

        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Doctors()
        {
            var doctors = await doctorsService.GetAllDoctors<DoctorViewModel>();
            return View(doctors);
        }

        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> GetDoctor(string id)
        {
            try
            {
                var doctor = doctorsService.GetDoctorById<DoctorViewModel>(id);
                return View(doctor);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(null, ex.Message);
                return Redirect(HttpContext.Request.Path);
            }
        }

        [HttpGet]
        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Create()
        {
            var doctor = new DoctorInputModel();
            var specialities = await specialitiesService.GetAll<SpecialityViewModel>();

            var formModel = new CreateDoctorFormModel
            {
                Doctor = doctor,
                Specialities = specialities,
            };

            return View(formModel);
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Create(CreateDoctorFormModel inputDoctor)
        {
            if (!ModelState.IsValid)
            {
                inputDoctor.Specialities = await specialitiesService.GetAll<SpecialityViewModel>();
                return this.View(inputDoctor);
            }

            var doctor = new Doctor
            {
                FirstName = inputDoctor.Doctor.FirstName,
                MiddleName = inputDoctor.Doctor.MiddleName,
                ThirdName = inputDoctor.Doctor.ThirdName,
                Description = inputDoctor.Doctor.Description,
                SpecialityId = inputDoctor.Doctor.SpecialityId,
                PhoneNumber = inputDoctor.Doctor.PhoneNumber,
                Email = inputDoctor.Doctor.Email,
                UserName = inputDoctor.Doctor.FirstName + '_' + inputDoctor.Doctor.ThirdName,
            };

            var password = doctor.ThirdName + DateTime.UtcNow.DayOfYear.ToString();

            var result = await userManager.CreateAsync(doctor, password);

            if (result.Succeeded)
            {
                return Redirect(nameof(this.Doctors));
            }
            else
            {
                return this.View(result);
            }
        }

        [HttpGet("/{doctorId}")]
        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Edit(string doctorId)
        {
            var doctor = await doctorsService.GetDoctorById<DoctorInputModel>(doctorId);
            var specialities = await specialitiesService.GetAll<SpecialityViewModel>();

            var formModel = new CreateDoctorFormModel
            {
                Doctor = doctor,
                Specialities = specialities,
            };

            return this.View(formModel);
        }
    }
}
