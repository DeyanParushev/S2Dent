namespace S2Dent.MVC.Controllers
{
    using System;
    
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;

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
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            var cookieOptions = new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30), IsEssential = true, Path = "/" };

            this.Response.Cookies.Append(cookieName, cookieValue, cookieOptions);

            return this.Redirect(returnUrl);
        }
    }
}