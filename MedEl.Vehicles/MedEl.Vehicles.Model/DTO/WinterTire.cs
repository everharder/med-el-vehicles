using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class WinterTire : Tire
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
    }
}
