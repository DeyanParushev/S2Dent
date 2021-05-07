namespace S2Dent.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.CookiePolicy;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    using S2Dent.DTOs;
    using S2Dent.Models;
    using S2Dent.MVC.Extensions;
    using S2Dent.Services;
    using S2Dent.Services.Automapper;
    using S2Dent.Services.Interfaces;
    using S2Dent.ViewModels.ViewModels;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.SetupDbContext(Configuration);
            
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

            services.SetupLocalization();

            services.AddTransient<IDoctorsService, DoctorsService>();
            services.AddTransient<ISpecialitiesService, SpecialitiesService>();

            services.AddRazorPages();
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(DoctorViewModel).GetTypeInfo().Assembly,
                typeof(DoctorDTO).GetTypeInfo().Assembly,
                typeof(Doctor).GetTypeInfo().Assembly);
                

            app.SeedRoles();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
