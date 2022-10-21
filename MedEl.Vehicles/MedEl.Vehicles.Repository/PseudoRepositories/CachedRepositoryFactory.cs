using MedEl.Vehicles.Repository.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.PseudoRepositories
{
    internal class CachedRepositoryFactory
    {
        public CachedRepository CreateInstance(IRepository repository)
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            List<IRepository> repositories;
            if (repository is InMemoryRepository)
            {
                repositories = new List<IRepository>() { repository };
            }
            else
            {
                InMemoryRepository cache = new InMemoryRepository();
                repositories = new List<IRepository>() { cache, repository };
            }

            return new CachedRepository(repositories);
        }
    }
}
