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
    internal class ChassisFactory : FactoryBase, IChassisFactory
    {
        private readonly IAxleFactory axleFactory;

        public ChassisFactory(ILogger logger, IIdentificationProvider identificationProvider, IAxleFactory axleFactory) : base(logger, identificationProvider)
        {
            this.axleFactory = axleFactory ?? throw new ArgumentNullException(nameof(axleFactory));
        }

        public IChassis CreateCarChassis()
        {
            return createChassis(EVehicleType.Car, new List<IAxle>()
            {
                axleFactory.CreateCarAxle(),
                axleFactory.CreateCarAxle(),
            });
        }

        public IChassis CreateMotorcycleChassis()
        {
            return createChassis(EVehicleType.Motorcycle, new List<IAxle>()
            {
                axleFactory.CreateMotorcycleAxle(),
                axleFactory.CreateMotorcycleAxle()
            });
        }

        public IChassis CreateChassis(EVehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case EVehicleType.Car:
                    return CreateCarChassis();
                case EVehicleType.Motorcycle:
                    return CreateMotorcycleChassis();
                default:
                    throw new NotImplementedException(vehicleType.ToString());
            }
        }

        private IChassis createChassis(EVehicleType vehicleType, IEnumerable<IAxle> axles)
        {
            string id = getId<Chassis>();
            return new Chassis(id, vehicleType, axles);
        }
    }
}
