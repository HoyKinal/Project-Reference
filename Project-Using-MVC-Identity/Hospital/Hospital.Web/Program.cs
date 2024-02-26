using Hospital.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared;
using Hospitals.Utilities;
using Hospital.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital.Models;
using Hospital.Services;

namespace Hospital.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(option => 
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Add custom Identity here 
            builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IDbInitializer,DbInitializer>(); 
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddTransient<IHospitalInfo, HospitalInfoService>();
            builder.Services.AddRazorPages();


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

            //Call function here
            app.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() =>
            {
                DataSedding(app);
            });


            app.UseRouting();

            app.UseAuthorization();

            //Add here app.MapRazorPages();
            app.MapRazorPages();

            //Add here as well
            app.MapControllerRoute(
               name: "default",
               pattern: "{Area=Patient}/{controller=Home}/{action=Index}/{id?}");

            
            app.Run();

            void DataSedding(WebApplication app)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    dbInitializer.InitializeAsync().Wait(); // Blocking wait in Main method
                }
            }
        }
    }
}
