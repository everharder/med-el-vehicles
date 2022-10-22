using MedEl.Vehicles.Common.Configuration;
using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            return services.AddSingleton<IConfigurationDictionary, AppSettingsConfiguration>()
                .AddSingleton<IConfiguration, BaseConfiguration>()
                .AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<object>>())
                .AddSingleton<IIdentificationProvider, IncrementalIdentifierProvider>();
        }
    }
}
