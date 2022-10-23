using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Factory;
using MedEl.Vehicles.Repository.Tests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Tests.Factory
{
    public class VehicleFactoryTests : TestBase
    {
        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        [InlineData(EVehicleType.Car | EVehicleType.Motorcycle)]
        public void CreateCar_DifferentManufacturers_Works(EVehicleType manufacturerVehicleTypes)
        {
            IServiceProvider services = createServiceProvider();
            IVehicleFactory factory = services.GetRequiredService<IVehicleFactory>();

            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>()
                .CreateManufacturer("foobar", manufacturerVehicleTypes);

            if (!manufacturerVehicleTypes.HasFlag(EVehicleType.Car))
            {
                Assert.Throws<NotSupportedException>(() => factory.CreateCar(manufacturer));
            }
            else
            {
                ICar car = factory.CreateCar(manufacturer);
                Assert.Equal(manufacturer, car.Manufacturer);
                Assert.Equal(EVehicleType.Car, car.VehicleType);
                Assert.Equal(2, car.Chassis.Axles.Count);
            }
        }

        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        [InlineData(EVehicleType.Car | EVehicleType.Motorcycle)]
        public void CreateMotorcycle_DifferentManufacturers_Works(EVehicleType manufacturerVehicleTypes)
        {
            IServiceProvider services = createServiceProvider();
            IVehicleFactory factory = services.GetRequiredService<IVehicleFactory>();

            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>()
                .CreateManufacturer("foobar", manufacturerVehicleTypes);

            if (!manufacturerVehicleTypes.HasFlag(EVehicleType.Motorcycle))
            {
                Assert.Throws<NotSupportedException>(() => factory.CreateMotorcycle(manufacturer));
            }
            else
            {
                IMotorcycle motorcycle = factory.CreateMotorcycle(manufacturer);
                Assert.Equal(manufacturer, motorcycle.Manufacturer);
                Assert.Equal(EVehicleType.Motorcycle, motorcycle.VehicleType);
                Assert.Equal(2, motorcycle.Chassis.Axles.Count);
            }
        }

        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        [InlineData(EVehicleType.Car | EVehicleType.Motorcycle)]
        public void CreateVehicle_DifferentManufacturers_Works(EVehicleType vehicleToCreate)
        {
            IServiceProvider services = createServiceProvider();
            IVehicleFactory factory = services.GetRequiredService<IVehicleFactory>();

            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>()
                .CreateManufacturer("foobar", EVehicleType.Car | EVehicleType.Motorcycle);

            if (vehicleToCreate.HasFlag(EVehicleType.Motorcycle) && vehicleToCreate.HasFlag(EVehicleType.Car))
            {
                Assert.Throws<NotImplementedException>(() => factory.Create(vehicleToCreate, manufacturer));
            }
            else
            {
                IVehicle vehicle = factory.Create(vehicleToCreate, manufacturer);
                Assert.Equal(manufacturer, vehicle.Manufacturer);
                Assert.Equal(vehicleToCreate, vehicle.VehicleType);
                Assert.Equal(2, vehicle.Chassis.Axles.Count);
            }
        }
    }
}
