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
    public class AxleFactoryTests : TestBase
    {
        [Fact]
        public void CreateCarAxle_NoParam_Works()
        {
            IServiceProvider services = createServiceProvider();
            IAxleFactory factory = services.GetRequiredService<IAxleFactory>();
            factory.CreateCarAxle();
        }

        [Fact]
        public void CreateMotorcycleAxle_NoParam_Works()
        {
            IServiceProvider services = createServiceProvider();
            IAxleFactory factory = services.GetRequiredService<IAxleFactory>();
            factory.CreateMotorcycleAxle();
        }

        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        public void CreateAxle_SpecificType_Works(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            IAxleFactory factory = services.GetRequiredService<IAxleFactory>();
            IAxle axle = factory.CreateAxle(vehicleType);
            Assert.Equal(vehicleType, axle.VehicleType);
        }
    }
}
