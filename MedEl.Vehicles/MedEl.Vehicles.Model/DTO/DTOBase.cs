using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal abstract class DTOBase : IPrettyPrintable, IIdentification
    {
        protected DTOBase(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }
            Id = id;
        }

        /// <inheritdoc/>
        public string Id { get; }

        /// <inheritdoc/>
        public virtual string ToPrettyString()
        {
            return Id;
        }
    }
}
