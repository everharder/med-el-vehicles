using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    public class Manufacturer : IManufacturer
    {
        public Manufacturer(string name, List<EVehicleType> vehicleTypes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }
            Name = name;

            VehicleTypes = vehicleTypes ?? throw new ArgumentNullException(nameof(vehicleTypes));
        }

        public string Name { get; }

        public List<EVehicleType> VehicleTypes { get; }
    }
}
