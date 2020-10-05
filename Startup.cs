using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using biljar.DbModels;
using biljar.Repositories;
using biljar.Services;
using biljar.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Data.Entity.Core.Metadata.Edm;

namespace biljar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private void SetupDbContext(IServiceCollection service)
        {
            var connString = Configuration.GetConnectionString("MyConnection");
            service.AddDbContext<pma_biljarContext>(options => options.UseSqlServer(connString));

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllersWithViews();
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AdminRepository>();
            services.AddScoped<AdminService>();
            services.AddScoped<KorisnikRepository>();
            services.AddScoped<KorisnikService>();

            services.AddScoped<pma_biljarContext>();
            services.AddDbContext<pma_biljarContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")));
            SetupDbContext(services);
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
            services.AddDistributedMemoryCache();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   "SignIn",
                   "biljar/signin",
                   new { controller = "Authentication", action = "SignIn" }
                   );

                endpoints.MapControllerRoute("Odabir kluba", "register/{id}", new { controller = "Authentication", action = "KlubOdaberi" });

                endpoints.MapControllerRoute("Profila", "korisnik/profil", new { controller = "Korisnici", action = "PrikazKorisnika" });

                endpoints.MapControllerRoute("Profila", "admin/korisnici", new { controller = "Admin", action = "SviKorisnici" });

                endpoints.MapControllerRoute("Profila", "admin/klubovi", new { controller = "Admin", action = "Klubovi" });


                endpoints.MapControllerRoute(
                    "SignUp",
                    "biljar/signup",
                    new { controller = "Authentication", action = "SignUp" }
                    );
                endpoints.MapControllerRoute(
                    "AdminSignin",
                    "biljar/adminsignin",
                    new { controller = "Authentication", action = "AdminSignin" }
                    );
                endpoints.MapControllerRoute(
                    "Logout",
                    "biljar/logout",
                    new { controller = "Home", action = "Logout" }
                    );

                endpoints.MapControllerRoute(
                    "Klubovi",
                    "biljar/klubovi",
                    new { controller = "Klubovi", action = "Klubovi" }
                    );
                endpoints.MapControllerRoute(
                    "Korisnici",
                    "biljar/korisnici",
                    new { controller = "Korisnici", action = "Korisnici" }
                    );
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
