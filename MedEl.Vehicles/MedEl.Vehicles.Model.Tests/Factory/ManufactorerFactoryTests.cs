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
    public class ManufactorerFactoryTests : TestBase
    {
        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        public void CreateManufacturer_EmptyName_Throws(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            IManufacturerFactory factory = services.GetRequiredService<IManufacturerFactory>();
            Assert.Throws<ArgumentException>(() => factory.CreateManufacturer(string.Empty, vehicleType));
        }

        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        public void CreateManufacturer_WhiteSpaceName_Throws(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            IManufacturerFactory factory = services.GetRequiredService<IManufacturerFactory>();
            Assert.Throws<ArgumentException>(() => factory.CreateManufacturer(" ", vehicleType));
        }

        [Fact]
        public void CreateManufacturer_NoVehicleTypes_Throws()
        {
            IServiceProvider services = createServiceProvider();
            IManufacturerFactory factory = services.GetRequiredService<IManufacturerFactory>();
            Assert.Throws<ArgumentException>(() => factory.CreateManufacturer("foobar", 0));
        }

        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        [InlineData(EVehicleType.Car | EVehicleType.Motorcycle)]
        public void CreateAxle_SpecificType_Works(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            IManufacturerFactory factory = services.GetRequiredService<IManufacturerFactory>();
            IManufacturer manufacturer = factory.CreateManufacturer("foobar", vehicleType);
            Assert.Equal("foobar", manufacturer.Name);
            Assert.Equal(vehicleType, manufacturer.SupportedVehicleTypes);
        }
    }
}
