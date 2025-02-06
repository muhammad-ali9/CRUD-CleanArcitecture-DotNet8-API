using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.IdentityModels;
using Persistence.Seeds;
using Persistence.ShareService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceConfiguration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(
                configuration.GetConnectionString("DefaultConn")
                ));

            services.AddScoped<IApplicationDbContext,  ApplicationDbContext>();

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAccountService, AccountService>();

            //Seed Roles and user , Default

            DefaultRoles.SeedRolesAsync(services.BuildServiceProvider()).Wait();
            DefaultUser.SeedUserAsync(services.BuildServiceProvider()).Wait();
        }
    }
}
