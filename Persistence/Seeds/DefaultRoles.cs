using Application.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var superAdmin = new ApplicationRole();
            superAdmin.Name = Convert.ToString(Roles.SuperAdmin);
            superAdmin.NormalizedName = Convert.ToString(Roles.SuperAdmin)?.ToUpper();
            await roleManager.CreateAsync(superAdmin);

            var roleAdmin = new ApplicationRole();
            roleAdmin.Name = Convert.ToString(Roles.Admin);
            roleAdmin.NormalizedName = Convert.ToString(Roles.Admin)?.ToUpper();
            await roleManager.CreateAsync(roleAdmin);

            var roleBasic = new ApplicationRole();
            roleBasic.Name = Convert.ToString(Roles.Basic);
            roleBasic.NormalizedName = Convert.ToString(Roles.Basic)?.ToUpper();
            await roleManager.CreateAsync(roleBasic);

            var roleUser = new ApplicationRole();
            roleUser.Name = Convert.ToString(Roles.User);
            roleUser.NormalizedName = Convert.ToString(Roles.User)?.ToUpper();
            await roleManager.CreateAsync(roleUser);
        }

    }
}
