using MedEl.Vehicles.Common.Identification;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.InMemory
{
    internal class TypeSpecificCache<TCache> : ITypeSpecificCache where TCache : IIdentification
    {
        private readonly ConcurrentDictionary<string, TCache> _cache = new ConcurrentDictionary<string, TCache>(StringComparer.OrdinalIgnoreCase);
        
        public Type ElementType => typeof(TCache);

        /// <summary>
        /// Deletes the given <paramref name="id"/>. If it existed before True is returned, otherwise False.
        /// </summary>
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }
            return _cache.TryRemove(id, out _);
        }

        /// <summary>
        /// Gets the entity of type <typeparamref name="TEntity"/> with the given <paramref name="id"/>
        /// </summary>
        public TCache? Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            if (_cache.TryGetValue(id, out TCache? value))
            {
                return value;
            }
            return default(TCache);
        }

        /// <summary>
        /// Gets all stored entities of type <typeparamref name="TEntity"/>
        /// </summary>
        public List<TCache> GetAll()
        {
            return _cache.Values.ToList();
        }

        /// <summary>
        /// Saves (creates or updates) the given <paramref name="entity"/>
        /// </summary>
        public void Save(TCache entity)
        {
            _cache.AddOrUpdate(entity.Id, entity, (_, _) => entity);
        }

        /// <summary>
        /// Gets the highest numerical id from the cache
        /// </summary>
        internal string GetHighestId<TEntity>() where TEntity : IIdentification
        {
            return _cache.Keys
                .Select(x => long.TryParse(x, out long id) ? id : (long?)null)
                .Where(x => x.HasValue)
                .OrderByDescending(x => x)
                .FirstOrDefault()?.ToString() ?? "-1";
        }
    }
}
