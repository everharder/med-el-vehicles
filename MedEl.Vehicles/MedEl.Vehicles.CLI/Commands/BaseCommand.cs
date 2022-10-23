using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Model.DTO.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal abstract class BaseCommand : ICommand
    {
        protected readonly IServiceProvider services;

        public BaseCommand(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public abstract string Execute(CliInput input);

        protected Type parseEntityType(EEntityTypeName? type)
        {
            if (!type.HasValue)
            {
                throw new Exception($"Parameter {nameof(CliInput.Type)} is required.");
            }

            switch (type)
            {
                case EEntityTypeName.Car:
                    return typeof(ICar);
                case EEntityTypeName.Motorcycle:
                    return typeof(IMotorcycle);
                case EEntityTypeName.Manufacturer:
                    return typeof(IManufacturer);
                default:
                    throw new NotSupportedException(type.ToString());
            }
        }

        protected IDAC getDac(EEntityTypeName? type)
        {
            Type entityType = parseEntityType(type);
            return getDac(entityType);
        }

        protected IDAC getDac(Type entityType)
        {
            IDACFactory dacFactory = services.GetRequiredService<IDACFactory>();
            IDAC dac = dacFactory.CreateDAC(entityType);
            return dac;
        }
    }
}
