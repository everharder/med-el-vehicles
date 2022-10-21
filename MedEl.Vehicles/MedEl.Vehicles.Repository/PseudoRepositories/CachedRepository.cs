using MedEl.Vehicles.Repository.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.CachedRepository
{
    /// <summary>
    /// A special aggregate repository that preceeds an InMemoryRepository before the actual repo for caching
    /// If the real repository is already an InMemoryRepository only one Repository is used
    /// </summary>
    internal class CachedRepository : AggregateRepository
    {
        /// <summary>
        ///  Extends the given <paramref name="repository"/> with caching
        /// </summary>
        public CachedRepository(IRepository repository) : base(createRepositories(repository))
        {
            // TODO: init cache?
        }

        private static List<IRepository> createRepositories(IRepository repository)
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (repository is InMemoryRepository)
            {
                return new List<IRepository>() { repository };
            }

            InMemoryRepository cache = new InMemoryRepository();
            return new List<IRepository>() { cache, repository };
        }
    }
}
