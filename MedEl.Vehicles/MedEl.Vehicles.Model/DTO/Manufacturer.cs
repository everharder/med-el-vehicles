using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class Manufacturer : DTOBase, IManufacturer
    {
        public Manufacturer(string name, EVehicleType supportedVehicleTypes) : base(name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }
            Name = name;

            SupportedVehicleTypes = supportedVehicleTypes;
        }

        public string Name { get; }

        public EVehicleType SupportedVehicleTypes { get; }

        public string ToTableHeader()
        {
            return $"NAME\tVEHICLE TYPES";
        }

        public string ToTableRow()
        {
            return $"{Name}\t{SupportedVehicleTypes}";
        }

        public override string ToString()
        {
            return ToTableRow();
        }

        public override string ToPrettyString()
        {
            return ToTableRow();
        }
    }
}
