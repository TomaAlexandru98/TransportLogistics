using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TransportLogistics
{
    public class Program
    {
       
       
        public static void InitiateAdmin(IConfiguration configuration,UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager)
        {
            //Configuration = configuration;
            string name = configuration.GetSection("Admin").GetSection("UserName").Value;
            string email = configuration.GetSection("Admin").GetSection("Email").Value;
            string password = configuration.GetSection("Admin").GetSection("Password").Value;
            IdentityUser admin = new IdentityUser
            {
                UserName = name,
                Email = email
            };
            var adminExists = userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if(adminExists == null)
            {
                userManager.CreateAsync(admin, password).GetAwaiter().GetResult();
                var role = new IdentityRole("Administrator");
                roleManager.CreateAsync(role).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(admin, "Administrator").GetAwaiter().GetResult();
            }
            if(userManager.IsInRoleAsync(admin , "Administrator").GetAwaiter().GetResult() == false)
            {
                userManager.AddToRoleAsync(admin, "Administrator").GetAwaiter().GetResult();
            }
        }
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .Enrich.WithThreadId()
               .Enrich.WithProcessId()
               .WriteTo.Console()
               .CreateLogger();
            IHost host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetService<RoleManager<IdentityRole>>();
                var userManager = services.GetService<UserManager<IdentityUser>>();
                var configuration = services.GetService<IConfiguration>();
                InitiateAdmin(configuration, userManager, roleManager);

            }

            host.Run();
            // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
