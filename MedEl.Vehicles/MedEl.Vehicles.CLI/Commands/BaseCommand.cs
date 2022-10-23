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

        protected Type parseEntityType(CliInput input)
        {
            if (!input.Type.HasValue)
            {
                throw new Exception($"Parameter {nameof(CliInput.Type)} is required for command {input.Command}");
            }

            switch (input.Type)
            {
                case EEntityTypeName.Car:
                    return typeof(ICar);
                case EEntityTypeName.Motorcycle:
                    return typeof(IMotorcycle);
                case EEntityTypeName.Manufacturer:
                    return typeof(IManufacturer);
                default:
                    throw new NotSupportedException(input.Type.ToString());
            }
        }

        protected IDAC getDac(CliInput input)
        {
            Type entityType = parseEntityType(input);
            IDACFactory dacFactory = services.GetRequiredService<IDACFactory>();
            IDAC dac = dacFactory.CreateDAC(entityType);
            return dac;
        }
    }
}
