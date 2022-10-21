using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository
{
    public interface IRepository
    {
        /// <summary>
        /// Gets all stored entities of type <typeparamref name="TEntity"/>
        /// </summary>
        public List<TEntity> GetAll<TEntity>() where TEntity : IPersistable;

        /// <summary>
        /// Gets the entity of type <typeparamref name="TEntity"/> with the given <paramref name="id"/>
        /// </summary>
        public TEntity? Get<TEntity>(string id) where TEntity : IPersistable;

        /// <summary>
        /// Saves (creates or updates) the given <paramref name="entity"/>
        /// </summary>
        public void Save<TEntity>(TEntity entity) where TEntity : IPersistable;

        /// <summary>
        /// Deletes the given <paramref name="entity"/>. If it existed before True is returned, otherwise False.
        /// </summary>
        public bool Delete<TEntity>(TEntity entity) where TEntity : IPersistable;

        /// <summary>
        /// Deletes the given <paramref name="id"/>. If it existed before True is returned, otherwise False.
        /// </summary>
        public bool Delete<TEntity>(string id) where TEntity : IPersistable;
    }
}
