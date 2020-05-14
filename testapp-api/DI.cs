using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapp_api.Models;
using testapp_api.Services;

namespace testapp_api
{
    public static class DI
    {
        public static void AddDepencyInjection(this IServiceCollection services)
        {
            services.AddScoped<StoreDbContext>();
            services.AddScoped<UserService>();
            services.AddScoped<ProductService>();
            //services.AddScoped<UserManager<StoreOptions>>();
            //services.AddScoped<IProfileRepository, ProfileRepository>();
            //services.AddScoped<ICityRepository, CityRepository>();
        }
    }
}
