namespace S2Dent.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using S2Dent.DTOs;
    using S2Dent.MVC.Areas.Identity;
    using S2Dent.Services.Interfaces;
    using S2Dent.ViewModels.InputModels;
    using S2Dent.ViewModels.ViewModels;

    public class DoctorsController : Controller
    {
        private readonly IDoctorsService doctorsService;
        private readonly IMapper mapper;

        public DoctorsController(IDoctorsService doctorsService, IMapper mapper)
        {
            this.doctorsService = doctorsService;
            this.mapper = mapper;
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
        public async Task<IActionResult> CreateDoctor(DoctorInputModel inputDoctor)
        {
            if(!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            var doctor = this.mapper.Map<DoctorDTO>(inputDoctor);
            await this.doctorsService.CreateDoctor(doctor, inputDoctor.Password);
            return this.Redirect("/Home");
        }
    }
}

