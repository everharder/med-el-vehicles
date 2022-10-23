using MedEl.Vehicles.Common.Identification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Repository
{
    public interface IRepository
    {
        /// <summary>
        /// Gets all stored entities of type <typeparamref name="TEntity"/>
        /// </summary>
        public List<TEntity> GetAll<TEntity>() where TEntity : IIdentification;

        /// <summary>
        /// Gets the entity of type <typeparamref name="TEntity"/> with the given <paramref name="id"/>
        /// </summary>
        public TEntity? Get<TEntity>(string id) where TEntity : IIdentification;

        /// <summary>
        /// Saves (creates or updates) the given <paramref name="entity"/>
        /// </summary>
        public void Save<TEntity>(TEntity entity) where TEntity : IIdentification;

        /// <summary>
        /// Deletes the given <paramref name="entity"/>. If it existed before True is returned, otherwise False.
        /// </summary>
        public bool Delete<TEntity>(TEntity entity) where TEntity : IIdentification;

        /// <summary>
        /// Deletes the given <paramref name="id"/>. If it existed before True is returned, otherwise False.
        /// </summary>
        public bool Delete<TEntity>(string id) where TEntity : IIdentification;

        /// <summary>
        /// Deletes the whole repository
        /// </summary>
        public void Truncate();

        /// <summary>
        /// Returns the highest persisted id in the repository for <typeparamref name="TEntity"/>
        /// </summary>
        public string GetHighestId<TEntity>() where TEntity : IIdentification;
    }
}
