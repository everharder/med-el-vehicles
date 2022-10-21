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
            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddTransient<FileSystemRepository>();
            services.AddTransient<InMemoryRepository>();
            services.AddSingleton<CachedRepositoryFactory>();
            services.AddSingleton<RepositoryConfiguration>();
            services.AddTransient(typeof(TypeSpecificCache<>));
            services.AddSingleton(createRepository);

            return services;
        }

        private static IRepository createRepository(IServiceProvider services)
        {
            RepositoryConfiguration configuration = services.GetRequiredService<RepositoryConfiguration>();

            IRepository repository;
            if (!string.IsNullOrWhiteSpace(configuration.FileSystemRepositoryPath))
            {
                repository = services.GetRequiredService<FileSystemRepository>();
            }
            else
            {
                repository = services.GetRequiredService<InMemoryRepository>();
            }
            
            if(configuration.UseCaching)
            {
                // wrap repo in a cached repository
                repository = services.GetRequiredService<CachedRepositoryFactory>()
                    .CreateInstance(repository);
            }
            return repository;
        }
    }
}
