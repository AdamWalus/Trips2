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



namespace Trips
{
	public class Program
	{
		public static void Main(string[] args)
		{

            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<TripReservationDbContext>(options =>{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

   builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TripsIdentityDbContext>();

			//repository
			builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<ITripService, TripService>();


			//validator
            builder.Services.AddScoped<IValidator<CreateTripViewModel>, TripsValidator>();
            builder.Services.AddControllersWithViews().AddFluentValidation();

			//mapper
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



            CreateDbIfNotExists(app);

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
    }
}