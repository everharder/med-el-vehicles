using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Model.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal abstract class Tire : DTOBase, ITire
    {
        protected Tire(string id, float pressure) : base(id)
        {
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
        public override string ToPrettyString()
        {
            return $"Pressure: {Pressure}Pa";
        }
    }
}
