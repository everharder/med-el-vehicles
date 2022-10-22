using MedEl.Vehicles.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common
{
    public static class Services
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationDictionary, AppSettingsConfiguration>();

            return services;
        }
    }
}
