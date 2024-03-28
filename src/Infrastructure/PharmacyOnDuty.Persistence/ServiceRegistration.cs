using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyOnDuty.Aplication.Interfaces.Repository;
using PharmacyOnDuty.Application.Cache;
using PharmacyOnDuty.Application.External.Api;
using PharmacyOnDuty.Persistence.Cache;
using PharmacyOnDuty.Persistence.Context;
using PharmacyOnDuty.Persistence.External.Api;
using PharmacyOnDuty.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("PharmacyOnDutyContext")));

            services.AddScoped<IPharmacyRepository, PharmacyRepository>();

            /// Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            services.AddScoped<ICacheService, RedisCacheService>();

            /// External Api Configuration
            services.AddHttpClient();
            services.AddScoped<IExternalApi, ExternalHtppClientApi>();
        }
    }
}
