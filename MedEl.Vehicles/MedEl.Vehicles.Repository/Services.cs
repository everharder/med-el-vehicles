using MedEl.Vehicles.Common;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Repository.Configuration;
using MedEl.Vehicles.Repository.DAC;
using MedEl.Vehicles.Repository.FileSystem;
using MedEl.Vehicles.Repository.InMemory;
using MedEl.Vehicles.Repository.PseudoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository
{
    public static class Services
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services.AddCommon()
                .AddSingleton<ISerializer, JsonSerializer>()
                .AddTransient<FileSystemRepository>()
                .AddTransient<InMemoryRepository>()
                .AddSingleton<CachedRepositoryFactory>()
                .AddSingleton<RepositoryConfiguration>()
                .AddSingleton<RepositoryFactory>()
                .AddTransient(typeof(TypeSpecificCache<>))
                .AddSingleton((provider) => provider.GetRequiredService<RepositoryFactory>().CreateInstance())
                .AddSingleton<ITypedDAC<ICar>, DACBase<ICar>>()
                .AddSingleton<ITypedDAC<IMotorcycle>, DACBase<IMotorcycle>>()
                .AddSingleton<ITypedDAC<IManufacturer>, DACBase<IManufacturer>>()
                .AddSingleton<IDACFactory, DACFactory>();
        }
    }
}
