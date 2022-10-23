using MedEl.Vehicles.Common;
using MedEl.Vehicles.Common.Configuration;
using MedEl.Vehicles.Repository.PseudoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Tests
{
    public static class Services
    {
        public static IServiceCollection AddRepositoryTests(this IServiceCollection services)
        {
            return services
                .AddCommon()
                .AddRepository()
                .AddSingleton<IConfigurationDictionary, SimpleConfiguration>();
        }
    }
}
