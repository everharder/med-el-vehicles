using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MedEl.Vehicles.Model.DTO
{
    /// <summary>
    /// An abstract representation of a vehicle
    /// </summary>
    internal abstract class Vehicle : DTOBase, IVehicle
    {
        protected Vehicle(string id, IManufacturer manufacturer, IChassis chassis) : base(id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            Chassis = chassis ?? throw new ArgumentNullException(nameof(chassis));
        }

        /// <summary>
        /// The type of vehicle (e.g. Car, Motorcycle)
        /// </summary>
        public abstract EVehicleType VehicleType { get; }

        /// <summary>
        /// The manufacturer of the vehicle (e.g. Honda, Toyota, KTM)
        /// </summary>
        public IManufacturer Manufacturer { get; }

        /// <summary>
        /// The chassis of the vehicle, containing the tires
        /// </summary>
        public IChassis Chassis { get; }

        /// <summary>
        /// As required by the assignment this method prints the current state of the instance
        /// TODO: Not sure why this method should be titled move...
        /// GetStatus(), GetInformation(), etc. would be better
        /// </summary>
        public void Move()
        {
            Console.WriteLine(ToPrettyString());
        }

        /// <inheritdoc/>
        public override string ToPrettyString()
        {
            return new StringBuilder()
                .AppendLine($"You are driving a {VehicleType.ToString().ToLower()} from {Manufacturer.Name}")
                .AppendLine(Chassis.ToPrettyString())
                .ToString();
        }

        public string ToTableHeader()
        {
            return $"ID\tVEHICLETYPE\tMANUFACTURER";
        }

        public string ToTableRow()
        {
            return $"{Id}\t{VehicleType}\t{Manufacturer.Name}";
        }

        public override string ToString()
        {
            return ToTableRow();
        }
    }
}
