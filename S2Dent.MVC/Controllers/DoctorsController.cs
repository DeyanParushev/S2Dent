namespace S2Dent.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using S2Dent.DTOs;
    using S2Dent.Models;
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
            catch(Exception ex)
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
            return View(doctor);
        }

        [HttpPost]
        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> Create(DoctorInputModel inputDoctor)
        {
            if(!ModelState.IsValid)
            {
                return this.View(inputDoctor);
            }

            var doctor = mapper.Map<Doctor>(inputDoctor);
            await doctorsService.CreateDoctor(doctor, inputDoctor.Password);
            return Redirect("/Home");
        }
    }
}

