namespace S2Dent.MVC.Controllers
{
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;

    public class PrivacyController : Controller
    {
        public IActionResult OnPostGive(string returnUrl)
        {
            this.HttpContext.Features.Get<ITrackingConsentFeature>().GrantConsent();
            return this.Redirect(returnUrl);
        }

        public IActionResult OnPostWithdraw(string returnUrl)
        {
            this.HttpContext.Features.Get<ITrackingConsentFeature>().WithdrawConsent();
            return this.Redirect(returnUrl);
        }
    }
}
