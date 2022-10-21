using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    /// <summary>
    /// Concrete implementation of a car
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The unique id of the Car</param>
        /// <param name="manufacturer">The manufacturer</param>
        public Car(string id, IManufacturer manufacturer) : base(id, manufacturer)
        {
        }

        /// <inheritdoc/>
        public override EVehicleType VehicleType => EVehicleType.Car;
    }
}
