using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Trips.Data;
using Trips.Repositories.RepositoryTrip;
using Trips.Services.TripService;
using Trips.ViewModel;
using AutoMapper;
using Trips.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Trips.Areas.Identity.Data;
using Trips.Migrations;

namespace Trips
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(); // Add Razor Pages services

            builder.Services.AddDbContext<TripReservationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

/*
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TripReservationDbContext>()
                .AddDefaultTokenProviders();*/




            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).
                AddRoles<IdentityRole>().AddEntityFrameworkStores<TripReservationDbContext>();

            // Repository
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<ITripService, TripService>();

            // Validator
            builder.Services.AddScoped<IValidator<CreateTripViewModel>, TripsValidator>();
            builder.Services.AddControllersWithViews().AddFluentValidation();

            // Mapper
            builder.Services.AddAutoMapper(typeof(TripMapping));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map Razor Pages
            app.MapRazorPages();

            CreateDbIfNotExists(app);
            await CreateRoles(app.Services); //awwait czeka az role siê zrobi¹ a potem idzie admin
            await AddAdmin(app.Services); 
            app.Run();
        }

        private static void CreateDbIfNotExists(IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TripReservationDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "admin", "user" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
        }


        private static async Task AddAdmin(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var email = "admin@admin.com";
                var password = "Admin@123";

                var adminUser = await userManager.FindByEmailAsync(email);
                if (adminUser == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "admin");
                    }
                }
                else
                {
                    if (!await userManager.IsInRoleAsync(adminUser, "admin"))
                    {
                        await userManager.AddToRoleAsync(adminUser, "admin");
                    }
                }
            }
        }


    }
}
