namespace S2Dent.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.CookiePolicy;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using S2Dent.Data;
    using S2Dent.DTOs;
    using S2Dent.Models;
    using S2Dent.MVC.Pages;
    using S2Dent.Services.Automapper;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<S2DentDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<S2DentDbContext>();

            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
                .AddRazorRuntimeCompilation();

            services.AddAutoMapper(typeof(Startup));
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.ConsentCookie.Expiration = DateTime.UtcNow.AddDays(3) - DateTime.UtcNow;
            });
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddRazorPages();
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorModel).GetTypeInfo().Assembly,
                typeof(DoctorDTO).GetTypeInfo().Assembly);

            var supportedCultures = new[] { "bg-Bg", "en-Us" };
            var localizationOptions =
                new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            localizationOptions.RequestCultureProviders =
                new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider(),
                };

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
