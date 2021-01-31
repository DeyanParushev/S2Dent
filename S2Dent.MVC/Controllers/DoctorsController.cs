namespace S2Dent.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using S2Dent.MVC.Areas.Identity;
    using S2Dent.Services.Interfaces;
    using S2Dent.ViewModels.InputModels;
    using S2Dent.ViewModels.ViewModels;

    public class DoctorsController : Controller
    {
        private readonly IDoctorsService doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            this.doctorsService = doctorsService;
        }

        [Route("/Team")]
        public async Task<IActionResult> Team()
        {
            var doctors = await this.doctorsService.GetAllDoctors<DoctorViewModel>();
            return this.View(doctors);
        }

        //[Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Doctors()
        {
            var doctors = await this.doctorsService.GetAllDoctors<DoctorViewModel>();
            return this.View(doctors);
        }

        //[Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> GetDoctor(string id)
        {
            try
            {
                var doctor = this.doctorsService.GetDoctorById<DoctorViewModel>(id);
                return this.View(doctor);
            }
            catch(Exception ex)
            {
                this.ModelState.AddModelError(null, ex.Message);
                return this.Redirect(this.HttpContext.Request.Path);
            }
        }

        //[Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Create()
        {
            var doctor = new DoctorInputModel();
            return this.View(doctor);
        }

        [HttpPost]
        //[Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> CreateDoctor()
        {
            var doctor = new DoctorInputModel();
            return this.View(doctor);
        }
    }
}

