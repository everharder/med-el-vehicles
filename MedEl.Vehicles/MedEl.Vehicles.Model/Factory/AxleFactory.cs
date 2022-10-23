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
    internal class AxleFactory : FactoryBase, IAxleFactory
    {
        private readonly ITireFactory tireFactory;

        public AxleFactory(ILogger logger, IIdentificationProvider identificationProvider, ITireFactory tireFactory) : base(logger, identificationProvider)
        {
            this.tireFactory = tireFactory ?? throw new ArgumentNullException(nameof(tireFactory));
        }

        public IAxle CreateCarAxle()
        {
            string id = getId<Axle>();
            return new CarAxle(id, tireFactory.CreateSummerTire(), tireFactory.CreateSummerTire());
        }

        public IAxle CreateMotorcycleAxle()
        {
            string id = getId<Axle>();
            return new MotorcycleAxle(id, tireFactory.CreateSummerTire());
        }
    }
}
