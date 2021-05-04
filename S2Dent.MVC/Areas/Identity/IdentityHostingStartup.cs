using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(S2Dent.MVC.Areas.Identity.IdentityHostingStartup))]
namespace S2Dent.MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}