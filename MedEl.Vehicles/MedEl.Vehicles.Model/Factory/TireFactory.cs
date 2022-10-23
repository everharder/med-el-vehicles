using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Common.Service;
using MedEl.Vehicles.Model.Configuration;
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
    internal class TireFactory : FactoryBase, ITireFactory
    {
        private readonly SummerTireConfiguration summerTireConfiguration;
        private readonly WinterTireConfiguration winterTireConfiguration;

        public TireFactory(ILogger logger,
            IIdentificationProvider identificationProvider,
            SummerTireConfiguration summerTireConfiguration,
            WinterTireConfiguration winterTireConfiguration) : base(logger, identificationProvider)
        {
            this.summerTireConfiguration = summerTireConfiguration;
            this.winterTireConfiguration = winterTireConfiguration;
        }

        public ITire CreateTire(ETireType tireType)
        {
            switch(tireType)
            {
                case ETireType.WinterTire:
                    return CreateWinterTire();
                case ETireType.SummerTire:
                    return CreateSummerTire();
                default:
                    throw new NotImplementedException(tireType.ToString());
            }
        }

        public ISummerTire CreateSummerTire()
            => CreateSummerTire(summerTireConfiguration);

        public ISummerTire CreateSummerTire(SummerTireConfiguration configuration)
        {
            string id = getId<SummerTire>();
            return new SummerTire(id, configuration.Pressure, configuration.MaximumTemperature);
        }

        public IWinterTire CreateWinterTire()
            => CreateWinterTire(winterTireConfiguration);

        public IWinterTire CreateWinterTire(WinterTireConfiguration configuration)
        {
            string id = getId<WinterTire>();
            return new WinterTire(id, configuration.Pressure, configuration.MinimumTemperature, configuration.Thickness);
        }

        public IEnumerable<ITire> CreateTires(IVehicle vehicle, ETireType tireType)
        {
            switch (tireType)
            {
                case ETireType.SummerTire:
                    return CreateSummerTires(vehicle);
                case ETireType.WinterTire:
                    return CreateWinterTires(vehicle);
                default:
                    throw new NotImplementedException(tireType.ToString());
            }
        }

        public IEnumerable<ISummerTire> CreateSummerTires(IVehicle vehicle)
            => CreateSummerTires(vehicle, summerTireConfiguration);

        public IEnumerable<ISummerTire> CreateSummerTires(IVehicle vehicle, SummerTireConfiguration configuration)
        {
            int tireCount = vehicle.Chassis.Axles.Sum(x => x.TireCount);
            return Enumerable.Range(0, tireCount)
                .Select(x => CreateSummerTire(configuration))
                .ToList();
        }

        public IEnumerable<IWinterTire> CreateWinterTires(IVehicle vehicle)
            => CreateWinterTires(vehicle, winterTireConfiguration);

        public IEnumerable<IWinterTire> CreateWinterTires(IVehicle vehicle, WinterTireConfiguration configuration)
        {
            int tireCount = vehicle.Chassis.Axles.Sum(x => x.TireCount);
            return Enumerable.Range(0, tireCount)
                .Select(x => CreateWinterTire(configuration))
                .ToList();
        }
    }
}
