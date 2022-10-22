using MedEl.Vehicles.Model.Interfaces;
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
        public bool Delete<TEntity>(TEntity entity) where TEntity : IPersistable
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Delete<TEntity>(entity.Id);
        }

        /// <inheritdoc/>
        public bool Delete<TEntity>(string id) where TEntity : IPersistable
            => getOrCreateCache<TEntity>().Delete(id);

        /// <inheritdoc/>
        public TEntity? Get<TEntity>(string id) where TEntity : IPersistable
            => getOrCreateCache<TEntity>().Get(id);

        /// <inheritdoc/>
        public List<TEntity> GetAll<TEntity>() where TEntity : IPersistable
            => getOrCreateCache<TEntity>().GetAll();

        /// <inheritdoc/>
        public void Save<TEntity>(TEntity entity) where TEntity : IPersistable
            => getOrCreateCache<TEntity>().Save(entity);

        /// <inheritdoc/>
        public void Truncate()
        {
            _caches.Clear();
        }

        private TypeSpecificCache<TEntity> getOrCreateCache<TEntity>() where TEntity : IPersistable
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
