using MedEl.Vehicles.Common.Identification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.DAC
{
    public interface ITypedDAC<TEntity> : IDAC where TEntity : IIdentification
    {
        /// <summary>
        /// Gets all stored entities of type <typeparamref name="TEntity"/>
        /// </summary>
        public new List<TEntity> FindAll();

        /// <summary>
        /// Gets the entity of type <typeparamref name="TEntity"/> with the given <paramref name="id"/>
        /// </summary>
        public new TEntity? Find(string id);

        /// <summary>
        /// Saves (creates or updates) the given <paramref name="entity"/>
        /// </summary>
        public void Save(TEntity entity);

        /// <summary>
        /// Deletes the given <paramref name="entity"/>. If it existed before True is returned, otherwise False.
        /// </summary>
        public bool Delete(TEntity entity);
    }
}
