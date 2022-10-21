using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    public abstract class Tire : IPersistable, ITire
    {
        protected Tire(string id, float pressure)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }
            Id = id;

            if (pressure < 0)
            {
                throw new ArgumentException($"'{nameof(pressure)}' cannot be less than zero.", nameof(pressure));
            }
            Pressure = pressure;
        }

        /// <summary>
        /// The current pressure of the tire
        /// </summary>
        public float Pressure { get; }

        /// <inheritdoc/>
        public string Id { get; }
    }
}
