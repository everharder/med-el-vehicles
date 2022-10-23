using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class SummerTire : Tire, ISummerTire
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SummerTire(string id, float pressure, float maximumTemperature) : base(id, pressure)
        {
            MaximumTemperature = maximumTemperature;
        }

        /// <summary>
        /// The maximum temperature where this tire is operable
        /// </summary>
        public float MaximumTemperature { get; }

        public override ETireType Type => ETireType.SummerTire;

        /// <inheritdoc/>
        public override string ToPrettyString()
        {
            return new StringBuilder()
                .Append(base.ToPrettyString())
                .Append($", Max. Temp.: {MaximumTemperature}°C")
                .ToString();
        }
    }
}
