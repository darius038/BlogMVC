using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogMVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogMVC.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            // Creating App host
            var appHost = CreateHostBuilder(args).Build();

            // Seeding database with Application admin user data
            try
            {
                var scope = appHost.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!context.Roles.Any())
                {
                    //Creating an admin role
                    roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!context.Users.Any(u => u.UserName == "admin"))
                {
                    //Creating an admin user
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@mail.com"
                    };
                    var result = userManager.CreateAsync(adminUser, "admin").GetAwaiter().GetResult();
                    //Adding role to user
                    userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            appHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
