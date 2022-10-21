using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.CachedRepository
{
    /// <summary>
    /// A collection of one or more mirrored repositories
    /// </summary>
    internal class AggregateRepository : IRepository
    {
        protected readonly List<IRepository> _repositories;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public AggregateRepository(List<IRepository> repositories)
        {
            if (repositories is null || repositories.Any(x => x is null))
            {
                throw new ArgumentNullException(nameof(repositories));
            }

            if (repositories.Count == 0)
            {
                throw new ArgumentException("At least one repository must be provided.");
            }

            // new reference copy (protect from outside world)
            _repositories = repositories.ToList();
        }

        /// <inheritdoc/>
        public bool Delete<TEntity>(TEntity entity) where TEntity : IPersistable
            => executeOnRepositories(r => r.Delete(entity));

        /// <inheritdoc/>
        public bool Delete<TEntity>(string id) where TEntity : IPersistable
            => executeOnRepositories(r => r.Delete<TEntity>(id));

        /// <inheritdoc/>
        public TEntity? Get<TEntity>(string id) where TEntity : IPersistable
            => _repositories.First().Get<TEntity>(id);

        /// <inheritdoc/>
        public List<TEntity> GetAll<TEntity>() where TEntity : IPersistable
            => _repositories.First().GetAll<TEntity>();

        /// <inheritdoc/>
        public void Save<TEntity>(TEntity entity) where TEntity : IPersistable
            => executeOnRepositories(r => r.Save(entity));

        private void executeOnRepositories(Action<IRepository> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            executeOnRepositories<object>((repo) =>
            {
                action(repo);
                return null;
            });
        }

        private TResult? executeOnRepositories<TResult>(Func<IRepository, TResult?> func)
        {
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            TResult? result = default(TResult?);
            List <Exception> exceptions = new List<Exception>();
            foreach(IRepository repo in _repositories)
            {
                try
                {
                    // execute function and gather result
                    result = func(repo);
                }
                catch(Exception ex)
                {
                    // aggregate errors so that every repository has a chance to be executed
                    exceptions.Add(ex);
                }
            }

            // in case of any error -> throw
            if(exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
            
            return result;
        }
    }
}
