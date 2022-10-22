using MedEl.Vehicles.Repository.FileSystem;
using MedEl.Vehicles.Repository.InMemory;
using MedEl.Vehicles.Repository.PseudoRepositories;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MedEl.Vehicles.Repository.Tests")]
namespace MedEl.Vehicles.Repository
{
    internal class RepositoryFactory
    {
        private readonly IServiceProvider services;
        private readonly RepositoryConfiguration configuration;
        private readonly CachedRepositoryFactory cachedRepositoryFactory;

        /// <summary>
        /// Creates a repository factory instance
        /// </summary>
        public RepositoryFactory(IServiceProvider services, RepositoryConfiguration configuration, CachedRepositoryFactory cachedRepositoryFactory)
        {
            this.services = services;
            this.configuration = configuration;
            this.cachedRepositoryFactory = cachedRepositoryFactory;
        }

        /// <summary>
        /// Creates a new repository from the injected RepositoryConfiguration
        /// </summary>
        public IRepository CreateInstance()
        {
            IRepository repository;
            if (!string.IsNullOrWhiteSpace(configuration.FileSystemRepositoryPath))
            {
                repository = services.GetRequiredService<FileSystemRepository>();
            }
            else
            {
                repository = services.GetRequiredService<InMemoryRepository>();
            }

            if (configuration.UseCaching)
            {
                // wrap repo in a cached repository
                repository = services.GetRequiredService<CachedRepositoryFactory>()
                    .CreateInstance(repository);
            }
            return repository;
        }
    }
}
