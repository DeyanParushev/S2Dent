namespace S2Dent.MVC.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using S2Dent.Data;
    using S2Dent.Models;
    using S2Dent.MVC.Areas.Identity;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class DbContextSetup
    {
        public static IServiceCollection SetupDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<S2DentDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<S2DentDbContext>();

            return services;
        }

        public static async Task SeedRoles(this IApplicationBuilder app)
        {
            var roles = typeof(IdentityRoles).GetFields(BindingFlags.Public | BindingFlags.Static);

            var serviceProvider = app.ApplicationServices;

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<S2DentDbContext>();

                if (!dbContext.Database.GetPendingMigrations().Any())
                {
                    await dbContext.Database.MigrateAsync();

                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                    
                    foreach (var role in roles)
                    {
                        var rolesExists = await roleManager.RoleExistsAsync(role.Name);

                        if (!rolesExists)
                        {
                            await roleManager.CreateAsync(new ApplicationRole(role.Name));
                        }
                    }
                }
            }
        }
    }
}
