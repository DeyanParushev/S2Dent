namespace S2Dent.MVC.Controllers
{
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;

    public class PrivacyController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult OnPostGive(string returnUrl)
        {
            HttpContext.Features.Get<ITrackingConsentFeature>().GrantConsent();
            return Redirect(returnUrl);
        }

        public IActionResult OnPostWithdraw(string returnUrl)
        {
            HttpContext.Features.Get<ITrackingConsentFeature>().WithdrawConsent();
            return Redirect(returnUrl);
        }
    }
}
