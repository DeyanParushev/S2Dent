namespace S2Dent.MVC.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using S2Dent.Data;
    using S2Dent.MVC.Areas.Identity;
    using S2Dent.Services.Interfaces;
    using S2Dent.ViewModels.ViewModels;

    public class DoctorsController : Controller
    {
        private readonly S2DentDbContext dbContext;
        private readonly IDoctorsService doctorsService;

        public DoctorsController(S2DentDbContext dbContext, IDoctorsService doctorsService)
        {
            this.dbContext = dbContext;
            this.doctorsService = doctorsService;
        }

        public async Task<IActionResult> Doctors()
        {
            var doctors = await this.doctorsService.GetAllDoctors<DoctorsViewModel>();
            return this.View(doctors);
        }

        [Authorize(Roles = IdentityRoles.SiteAdmin)]
        public async Task<IActionResult> GetDoctor(string id)
        {
            try
            {
                var doctor = this.doctorsService.GetDoctorById<DoctorsViewModel>(id);
                return this.View(doctor);
            }
            catch(Exception ex)
            {
                this.ModelState.AddModelError(null, ex.Message);
                return this.Redirect(this.HttpContext.Request.Path);
            }
        }
    }
}
