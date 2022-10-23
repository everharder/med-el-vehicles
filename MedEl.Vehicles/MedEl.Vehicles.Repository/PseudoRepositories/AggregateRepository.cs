using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.PseudoRepositories
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
        public bool Delete<TEntity>(TEntity entity) where TEntity : IIdentification
            => executeOnRepositories(r => r.Delete(entity));

        /// <inheritdoc/>
        public bool Delete<TEntity>(string id) where TEntity : IIdentification
            => executeOnRepositories(r => r.Delete<TEntity>(id));

        /// <inheritdoc/>
        public virtual TEntity? Get<TEntity>(string id) where TEntity : IIdentification
            => _repositories.Select(x => x.Get<TEntity>(id)).FirstOrDefault(x => x != null);

        /// <inheritdoc/>
        public virtual List<TEntity> GetAll<TEntity>() where TEntity : IIdentification
            => _repositories.Select(x => x.GetAll<TEntity>()).FirstOrDefault(x => x.Count > 0)
                ?? new List<TEntity>();

        /// <inheritdoc/>
        public void Save<TEntity>(TEntity entity) where TEntity : IIdentification
            => executeOnRepositories(r => r.Save(entity));

        /// <inheritdoc/>
        public void Truncate()
            => executeOnRepositories(r => r.Truncate());

        /// <inheritdoc/>
        public string GetHighestId<TEntity>() where TEntity : IIdentification
            => _repositories.First().GetHighestId<TEntity>();

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
