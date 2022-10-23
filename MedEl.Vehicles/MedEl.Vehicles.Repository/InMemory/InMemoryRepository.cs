using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.InMemory
{
    /// <summary>
    /// Implementation of a in memory data store. All data will be lost on application reset
    /// Can be used for caching applications
    /// </summary>
    internal class InMemoryRepository : IRepository
    {
        private readonly ConcurrentDictionary<Type, ITypeSpecificCache> _caches = new ConcurrentDictionary<Type, ITypeSpecificCache>();

        /// <inheritdoc/>
        public bool Delete<TEntity>(TEntity entity) where TEntity : IIdentification
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Delete<TEntity>(entity.Id);
        }

        /// <inheritdoc/>
        public bool Delete<TEntity>(string id) where TEntity : IIdentification
            => getOrCreateCache<TEntity>().Delete(id);

        /// <inheritdoc/>
        public TEntity? Get<TEntity>(string id) where TEntity : IIdentification
            => getOrCreateCache<TEntity>().Get(id);

        /// <inheritdoc/>
        public List<TEntity> GetAll<TEntity>() where TEntity : IIdentification
            => getOrCreateCache<TEntity>().GetAll();

        /// <inheritdoc/>
        public void Save<TEntity>(TEntity entity) where TEntity : IIdentification
            => getOrCreateCache<TEntity>().Save(entity);

        /// <inheritdoc/>
        public void Truncate()
        {
            _caches.Clear();
        }

        public string GetHighestId<TEntity>() where TEntity : IIdentification
            => getOrCreateCache<TEntity>().GetHighestId<TEntity>();

        private TypeSpecificCache<TEntity> getOrCreateCache<TEntity>() where TEntity : IIdentification
        {
            ITypeSpecificCache cache = _caches.GetOrAdd(typeof(TEntity), new TypeSpecificCache<TEntity>());

            if(cache is not TypeSpecificCache<TEntity> entityCache)
            {
                throw new Exception($"registered {nameof(entityCache)} was expected to be of type {typeof(TypeSpecificCache<TEntity>)}, but was {cache?.GetType()}");
            }
            return entityCache;
        }
    }
}
