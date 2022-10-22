using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Common.Service;
using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.DTO;
using MedEl.Vehicles.Model.DTO.Interfaces;
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

        public ITire CreateSummerTire()
            => CreateSummerTire(summerTireConfiguration);

        public ITire CreateSummerTire(SummerTireConfiguration configuration)
        {
            string id = getId<SummerTire>();
            return new SummerTire(id, configuration.Pressure, configuration.MaximumTemperature);
        }

        public ITire CreateWinterTire()
            => CreateWinterTire(winterTireConfiguration);

        public ITire CreateWinterTire(WinterTireConfiguration configuration)
        {
            string id = getId<WinterTire>();
            return new WinterTire(id, configuration.Pressure, configuration.MinimumTemperature, configuration.Thickness);
        }
    }
}
