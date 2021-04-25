using coshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coshop.Data
{
    public class DbInitializer
    {
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // check if roles exist
            if (roleManager.Roles.Count() > 0)
                return 1;

            // seed the roles
            int result = await SeedRole(roleManager, "Admin");
            if (result != 0)
                return 2;

            result = await SeedRole(roleManager, "User");
            if (result != 0)
                return 3;

            // check if users exist
            if (userManager.Users.Count() > 0)
                return 4;
            
            // seed the users
            result = await SeedUser(userManager, "admin@coshop.ca", "Admin", "Admin", "Password!1");
            if (result != 0)
                return 5;

            result = await SeedUser(userManager, "leo@coshop.ca", "Leo", "User", "Password!1");
            if (result != 0)
                return 6;

            result = await SeedUser(userManager, "raph@coshop.ca", "Raph", "User", "Password!1");
            if (result != 0)
                return 7;

            result = await SeedUser(userManager, "donnie@coshop.ca", "Donnie", "User", "Password!1");
            if (result != 0)
                return 8;

            result = await SeedUser(userManager, "mikey@coshop.ca", "Mikey", "User", "Password!1");
            if (result != 0)
                return 9;

            return 0;
        }

        private static async Task<int> SeedRole(RoleManager<IdentityRole> roleManager, string role)
        {
            // create role
            var result = await roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
                return 1;

            return 0;
        }

        private static async Task<int> SeedUser(UserManager<ApplicationUser> userManager, string email, string displayName, string role, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                DisplayName = displayName,
                EmailConfirmed = true
            };

            // create user
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return 1;

            // assign role to user
            result = await userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                return 2;

            return 0;
        }

    }
}
