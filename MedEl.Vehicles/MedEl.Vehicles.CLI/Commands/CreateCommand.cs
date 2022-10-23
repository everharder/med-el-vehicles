using MedEl.Vehicles.CLI.Input;
using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI.Commands
{
    internal class CreateCommand : BaseCommand
    {
        private readonly IManufacturerFactory manufacturerFactory;
        private readonly ITypedDAC<IManufacturer> manufacturerDAC;
        private readonly ITypedDAC<ICar> carDAC;
        private readonly ITypedDAC<IMotorcycle> motorcycleDAC;
        private readonly IVehicleFactory vehicleFactory;

        public CreateCommand(IServiceProvider services, 
            IManufacturerFactory manufacturerFactory,
            ITypedDAC<IManufacturer> manufacturerDAC,
            ITypedDAC<ICar> carDAC,
            ITypedDAC<IMotorcycle> motorcycleDAC,
            IVehicleFactory vehicleFactory) : base(services)
        {
            this.manufacturerFactory = manufacturerFactory ?? throw new ArgumentNullException(nameof(manufacturerFactory));
            this.manufacturerDAC = manufacturerDAC ?? throw new ArgumentNullException(nameof(manufacturerDAC));
            this.carDAC = carDAC;
            this.motorcycleDAC = motorcycleDAC;
            this.vehicleFactory = vehicleFactory ?? throw new ArgumentNullException(nameof(vehicleFactory));
        }

        public override string Execute(CliInput input)
        {
            StringBuilder sb = new StringBuilder();
            IIdentification? created;
            switch(input.Type)
            {
                case EEntityTypeName.Car:
                    created = createVehicle(EVehicleType.Car, sb);
                    break;
                case EEntityTypeName.Motorcycle:
                    created = createVehicle(EVehicleType.Motorcycle, sb);
                    break;
                case EEntityTypeName.Manufacturer:
                    created = createManufacturer(sb);
                    break;
                default:
                    throw new NotSupportedException(input.Type.ToString());
            }

            if(created != null)
            {
                Console.WriteLine($"Created: {created}");
            }
            else
            {
                Console.WriteLine($"Nothing created...");
            }

            return sb.ToString();
        }

        private IVehicle? createVehicle(EVehicleType vehicleType, StringBuilder sb)
        {
            string? name = null;
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Manufacturer: ");
                name = Console.ReadLine();
            }

            IManufacturer? manufacturer = manufacturerDAC.Find(name);
            if(manufacturer == null)
            {
                sb.AppendLine($"Cannot find manufacturer with name {name}");
                return null;
            }

            switch(vehicleType)
            {
                case EVehicleType.Motorcycle:
                    IMotorcycle motorcycle = vehicleFactory.CreateMotorcycle(manufacturer!);
                    motorcycleDAC.Save(motorcycle);
                    return motorcycle;
                case EVehicleType.Car:
                    ICar car = vehicleFactory.CreateCar(manufacturer!);
                    carDAC.Save(car);
                    return car;
                default:
                    throw new NotSupportedException(vehicleType.ToString());
            }
        }

        private IIdentification? createManufacturer(StringBuilder sb)
        {
            string? name = null;
            while(string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
            }

            // read supported vehicle types
            EVehicleType vehicleTypes = 0;
            while (vehicleTypes == 0)
            {
                Console.Write("What does the manufacturer make ([Car|Motorcycle], multiple comma-separated values supported): ");
                string? vehicleTypeString = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(vehicleTypeString))
                {
                    // parse input
                    List<EVehicleType> types = vehicleTypeString
                        .Split(',', ';')
                        .Select(x => Enum.Parse<EVehicleType>(x))
                        .ToList();

                    // set result
                    foreach(EVehicleType t in types)
                    {
                        vehicleTypes |= t;
                    }
                }
            }

            IManufacturer manufacturer = manufacturerFactory.CreateManufacturer(name!, vehicleTypes);
            manufacturerDAC.Save(manufacturer);
            return manufacturer;
        }
    }
}
