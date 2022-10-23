using MedEl.Vehicles.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Logic.Tests
{
    public static class Services
    {
        public static IServiceCollection AddLogicTests(this IServiceCollection services)
        {
            services.AddLogic();
            services.AddSingleton<IConfigurationDictionary, SimpleConfiguration>();

            return services;
        }
    }
}
