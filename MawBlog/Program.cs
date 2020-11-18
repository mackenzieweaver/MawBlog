using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MawBlog.Data;
using MawBlog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MawBlog.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

namespace MawBlog
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //host.MigrateDatabase();
            await DataHelper.ManageData(host);
            await SeedDataAsync(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true);
                    webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                    webBuilder.UseStartup<Startup>();
                });

        public async static Task SeedDataAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<BlogUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await SeedHelper.SeedDataAsync(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }        
    }
}
