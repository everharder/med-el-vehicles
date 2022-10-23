using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Common.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Factory
{
    internal class FactoryBase : ServiceBase
    {
        private readonly IIdentificationProvider identificationProvider;

        public FactoryBase(ILogger logger, IIdentificationProvider identificationProvider) : base(logger)
        {
            this.identificationProvider = identificationProvider ?? throw new ArgumentNullException(nameof(identificationProvider));
        }

        protected string getId<TCreation>() where TCreation : IIdentification
        {
            string id = identificationProvider.GetId<TCreation>();
            logger.LogTrace("Created a {entityType} ({id})", typeof(TCreation).Name, id);
            return id;
        }
    }
}
