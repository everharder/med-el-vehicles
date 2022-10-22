using MedEl.Vehicles.Common;
using MedEl.Vehicles.Repository.Configuration;
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
                .AddSingleton((provider) => provider.GetRequiredService<RepositoryFactory>().CreateInstance());
        }
    }
}
