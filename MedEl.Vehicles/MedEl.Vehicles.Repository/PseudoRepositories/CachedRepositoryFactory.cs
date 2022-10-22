using MedEl.Vehicles.Repository.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.PseudoRepositories
{
    internal class CachedRepositoryFactory
    {
        private readonly IServiceProvider services;

        public CachedRepositoryFactory(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IRepository CreateInstance(IRepository repository)
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            List<IRepository> repositories;
            if (repository is InMemoryRepository)
            {
                // ne need for cache if the main repository is already in memory 
                return repository;
            }

            InMemoryRepository cache = services.GetRequiredService<InMemoryRepository>();
            repositories = new List<IRepository>() { cache, repository };
            return new CachedRepository(repositories);
        }
    }
}
