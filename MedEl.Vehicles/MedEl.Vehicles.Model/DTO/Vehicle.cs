using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    /// <summary>
    /// An abstract representation of a vehicle
    /// </summary>
    public abstract class Vehicle : IPrettyPrintable, IVehicle
    {
        protected Vehicle(string id, IManufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }
            Id = id;

            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
        }

        /// <summary>
        /// The type of vehicle (e.g. Car, Motorcycle)
        /// </summary>
        public abstract EVehicleType VehicleType { get; }

        /// <summary>
        /// The manufacturer of the vehicle (e.g. Honda, Toyota, KTM)
        /// </summary>
        public IManufacturer Manufacturer { get; }

        /// <inheritdoc/>
        public string Id { get; }

        /// <summary>
        /// As required by the assignment this method prints the current state of the instance
        /// TODO: Not sure why this method should be titled move...
        /// GetStatus(), GetInformation(), etc. would be better
        /// </summary>
        public void Move()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string ToPrettyString()
        {
            return $"You are driving a {this.VehicleType.ToString().ToLower()} from {Manufacturer.Name}";
        }

        /// <inheritdoc/>
        public bool Validate(out string issues)
        {
            throw new NotImplementedException();
        }
    }
}
