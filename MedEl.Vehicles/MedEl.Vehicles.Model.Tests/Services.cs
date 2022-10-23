using MedEl.Vehicles.Common;
using MedEl.Vehicles.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Tests
{
    public static class Services
    {
        public static IServiceCollection AddModelTests(this IServiceCollection services)
        {
            return services
                .AddCommon()
                .AddModel()
                .AddSingleton<IConfigurationDictionary, SimpleConfiguration>();
        }
    }
}
