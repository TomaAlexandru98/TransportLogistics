using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using TransportLogistics.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TransportLogistics.DataAccess;

using TransportLogistics.ApplicationLogic.Services;

using TransportLogistics.DataAccess.Repositories;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.Data.Abstractions;
using SignalR;

namespace TransportLogistics
{
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
           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<TransportLogisticsDbContext>(options =>
            options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection1")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSignalR();

            services.AddScoped<ITrailerRepository, EFTrailerRepository>();
            services.AddScoped<TrailerService>();
            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
            services.AddScoped<IPersistenceContext, EFPersistenceContext>();
            services.AddScoped<CustomerService>();
            services.AddScoped<SupervisorService>();
            services.AddScoped<VehicleService>();
            services.AddScoped<RequestService>();
            services.AddScoped<EmployeeServices>();
            services.AddScoped<DriverService>();
            services.AddScoped<OrderService>();
            services.AddScoped<RouteService>();
            services.AddScoped<DispatcherService>();
            services.AddScoped<EditInfoRequestService>();
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                
                endpoints.MapHub<RequestHub>("/request");
            });
           
        }
    }
}
