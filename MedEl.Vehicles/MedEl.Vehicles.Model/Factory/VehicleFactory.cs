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
    internal class VehicleFactory : FactoryBase, IVehicleFactory
    {
        private readonly IChassisFactory chassisFactory;

        public VehicleFactory(ILogger logger, IIdentificationProvider identificationProvider, IChassisFactory chassisFactory) : base(logger, identificationProvider)
        {
            this.chassisFactory = chassisFactory ?? throw new ArgumentNullException(nameof(chassisFactory));
        }

        public ICar CreateCar(IManufacturer manufacturer)
        {
            if (!manufacturer.SupportedVehicleTypes.HasFlag(EVehicleType.Car))
            {
                throw new ArgumentException($"Cannot create car for manufacturer {manufacturer.Name}. Not allowed!");
            }

            string id = getId<Car>();
            IChassis chassis = chassisFactory.CreateCarChassis();
            return new Car(id, manufacturer, chassis);
        }

        public IMotorcycle CreateMotorcycle(IManufacturer manufacturer)
        {
            if (!manufacturer.SupportedVehicleTypes.HasFlag(EVehicleType.Motorcycle))
            {
                throw new ArgumentException($"Cannot create motorcycle for manufacturer {manufacturer.Name}. Not allowed!");
            }

            string id = getId<Motorcycle>();
            IChassis chassis = chassisFactory.CreateMotorcycleChassis();
            return new Motorcycle(id, manufacturer, chassis);
        }
    }
}
