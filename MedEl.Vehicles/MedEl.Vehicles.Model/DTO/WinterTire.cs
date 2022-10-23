using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class WinterTire : Tire, IWinterTire
    {
        public WinterTire(string id, float pressure, float minimumTemperature, float thickness) : base(id, pressure)
        {
            MinimumTemperature = minimumTemperature;

            if (thickness <= 0)
            {
                throw new ArgumentException($"'{nameof(thickness)}' cannot be less than or equal to zero.", nameof(thickness));
            }
            Thickness = thickness;
        }

        public float MinimumTemperature { get; }

        public float Thickness { get; }

        public override ETireType Type => ETireType.WinterTire;

        /// <inheritdoc/>
        public override string ToPrettyString()
        {
            return new StringBuilder()
                .Append(base.ToPrettyString())
                .Append($", Min. Temp.: {MinimumTemperature}°C")
                .Append($", Thickness: {Thickness * 1000}mm")
                .ToString();
        }
    }
}
