using MedEl.Vehicles.Common.Repository;
using MedEl.Vehicles.Repository.InMemory;
using System;
using System.Collections.Concurrent;
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
        private readonly IRepository realRepository;
        private readonly ConcurrentBag<Type> initializedTypes = new ConcurrentBag<Type>();

        /// <summary>
        ///  Extends the given <paramref name="repository"/> with caching
        /// </summary>
        public CachedRepository(List<IRepository> repositories) : base(repositories)
        {
            if(repositories.Count < 2)
            {
                throw new ArgumentException($"{nameof(CachedRepository)} must constist of exactly at least two repositories");
            }

            if(repositories.Count > 2)
            {
                throw new NotImplementedException($"{nameof(CachedRepository)} not implemented for more than two repositories");
            }

            IRepository firstRepository = repositories.First();
            if(firstRepository is not InMemoryRepository inMemoryRepository)
            {
                throw new ArgumentException($"First repository must be of type {nameof(inMemoryRepository)}");
            }
            this.cacheRepository = inMemoryRepository;
            this.realRepository = repositories.Last();
        }

        protected override AggregateRepository ensureInitialized<TEntity>()
        {
            AggregateRepository repository = base.ensureInitialized<TEntity>();
            if(initializedTypes.Contains(typeof(TEntity)))
            {
                return repository;
            }

            // load all entities from real repository into cache
            List<TEntity> entities = realRepository.GetAll<TEntity>();
            entities.ForEach(x => cacheRepository.Save(x));
            initializedTypes.Add(typeof(TEntity));
            return repository;
        }
    }
}
