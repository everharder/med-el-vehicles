using MedEl.Vehicles.Repository.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.PseudoRepositories
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
        public CachedRepository(List<IRepository> repositories) : base(repositories)
        {
            // TODO: init cache?
        }
    }
}
