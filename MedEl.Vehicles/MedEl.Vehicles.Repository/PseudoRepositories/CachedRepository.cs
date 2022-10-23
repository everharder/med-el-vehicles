using MedEl.Vehicles.Common.Repository;
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
        private readonly InMemoryRepository cacheRepository;

        /// <summary>
        ///  Extends the given <paramref name="repository"/> with caching
        /// </summary>
        public CachedRepository(List<IRepository> repositories) : base(repositories)
        {
            IRepository firstRepository = repositories.First();
            if(firstRepository is not InMemoryRepository inMemoryRepository)
            {
                throw new ArgumentException($"First repository must be of type {nameof(inMemoryRepository)}");
            }
            this.cacheRepository = inMemoryRepository;
        }

        public override List<TEntity> GetAll<TEntity>()
        {
            List<TEntity> result = base.GetAll<TEntity>();
            if(result.Count > 0)
            {
                // cache result
                result.ForEach(x => cacheRepository.Save(x));
            }
            return result;
        }
    }
}
