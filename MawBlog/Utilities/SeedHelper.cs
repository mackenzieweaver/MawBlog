using MawBlog.Enums;
using MawBlog.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MawBlog.Utilities
{
    public class SeedHelper
    {
        public static async Task SeedDataAsync(UserManager<BlogUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedAdmin(userManager);
            await SeedModerator(userManager);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
        }

        public static async Task SeedAdmin(UserManager<BlogUser> userManager)
        {
            if(await userManager.FindByEmailAsync("mackenzie@weaver.com") == null)
            {
                var admin = new BlogUser()
                {
                    Email = "mackenzie@weaver.com",
                    UserName = "mackenzie@weaver.com",
                    FirstName = "Mackenzie",
                    LastName = "Weaver",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Mweaver1!");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }

        public static async Task SeedModerator(UserManager<BlogUser> userManager)
        {
            if (await userManager.FindByEmailAsync("jason@twitchell.com") == null)
            {
                var moderator = new BlogUser()
                {
                    Email = "jason@twitchell.com",
                    UserName = "jason@twitchell.com",
                    FirstName = "Jason",
                    LastName = "Twitchell",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(moderator, "Jtwitchell1!");
                await userManager.AddToRoleAsync(moderator, Roles.Moderator.ToString());
            }
        }
    }
}
