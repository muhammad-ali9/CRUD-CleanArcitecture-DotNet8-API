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
    public class DefaultUser
    {
        public static async Task SeedUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser();
            user.UserName = "superadmin";
            user.Email = "superadmin@gmail.com";
            user.FirstName = "Muhammad";
            user.LastName = "Ali";
            user.Gender = "Male";
            user.SecurityStamp = "1";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;

            if(userManager.Users.All(x=>x.Id != user.Id))
            {
                var result = await userManager.FindByEmailAsync(user.Email);
                if (result == null)
                {
                    await userManager.CreateAsync(user, "Test@123456");
                    await userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(user, Roles.User.ToString());
                }
            }
            //await userManager.CreateAsync(user);
        }
    }
}
