using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PharmacyOnDuty.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Application
{
    public static class ServiceRegistration
    {

        public static void AddApplicationRegistration(this IServiceCollection services)
        {
            var assm=Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assm);
            services.AddMediatR(assm);

            services.AddScoped<PharmacyService>();
        }
    }
}
