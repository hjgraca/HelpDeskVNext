using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace HelpDeskVNext.Data.Models
{
    public static class SampleData
    {
        public static async Task InitializeIdentityDatabaseAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            await CreateAdminUser(serviceProvider);
        }

        /// <summary>
        /// Creates a store manager user who can manage tickets.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<IOptions<IdentityDbContextOptions>>().Value;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string adminRole = options.AdministratorRole.Trim();

            if (!string.IsNullOrEmpty(options.Roles))
            {
                foreach (var role in options.Roles.Split(','))
                {
                    if (!await roleManager.RoleExistsAsync(role.Trim()))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role.Trim()));
                    }
                }
            }

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            var user = await userManager.FindByNameAsync(options.DefaultAdminUserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = options.DefaultAdminUserName };
                await userManager.CreateAsync(user, options.DefaultAdminPassword);
                await userManager.AddToRoleAsync(user, adminRole);
                await userManager.AddClaimAsync(user, new Claim("ManageTickets", "Allowed"));
            }
        }
    }
}
