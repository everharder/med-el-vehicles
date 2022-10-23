using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Common.Service;
using MedEl.Vehicles.Model.DTO;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Factory
{
    internal class ManufacturerFactory : FactoryBase, IManufacturerFactory
    {
        public ManufacturerFactory(ILogger logger, IIdentificationProvider identificationProvider) : base(logger, identificationProvider)
        {
        }

        public IManufacturer CreateManufacturer(string name, EVehicleType supportedVehicleTypes)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if(supportedVehicleTypes == 0)
            {
                throw new ArgumentException($"At least one vehicle type must be supported.");
            }

            return new Manufacturer(name, supportedVehicleTypes);
        }
    }
}
