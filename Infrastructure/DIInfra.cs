using Domain.Repository;
using Infrastructure.Repo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DIInfra
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProduct, ProductRepo>();

        }
    }
}
