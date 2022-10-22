using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Common.Service;
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
    internal class ChassisFactory : FactoryBase, IChassisFactory
    {
        private readonly ITireFactory tireFactory;

        public ChassisFactory(ILogger logger, IIdentificationProvider identificationProvider, ITireFactory tireFactory) : base(logger, identificationProvider)
        {
            this.tireFactory = tireFactory ?? throw new ArgumentNullException(nameof(tireFactory));
        }

        public IChassis CreateCarChassis()
        {
            string id = getId<Chassis>();

            // create four summer tires (default)
            List<ITire> tires = Enumerable.Range(0, 4)
                .Select(x => tireFactory.CreateSummerTire())
                .ToList();

            return new Chassis(id, tires);
        }

        public IChassis CreateMotorcycleChassis()
        {
            string id = getId<Chassis>();

            // create two summer tires (default)
            List<ITire> tires = Enumerable.Range(0, 2)
                .Select(x => tireFactory.CreateSummerTire())
                .ToList();

            return new Chassis(id, tires);
        }
    }
}
