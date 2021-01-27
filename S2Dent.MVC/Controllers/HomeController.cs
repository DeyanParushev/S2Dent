namespace S2Dent.MVC.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Contacts()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CultureManagment(string culture, string returnUrl)
        {
            var cookieName = CookieRequestCultureProvider.DefaultCookieName;
            var cookieOptions = new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) };
            Response.Cookies.Append(cookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), cookieOptions);

            return this.Redirect(returnUrl);
        }
    }
}
