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
    public class ChassisFactoryTests : TestBase
    {
        [Fact]
        public void CreateCarChassis_NoParam_Works()
        {
            IServiceProvider services = createServiceProvider();
            IChassisFactory factory = services.GetRequiredService<IChassisFactory>();
            factory.CreateCarChassis();
        }

        [Fact]
        public void CreateMotorcycleChassis_NoParam_Works()
        {
            IServiceProvider services = createServiceProvider();
            IChassisFactory factory = services.GetRequiredService<IChassisFactory>();
            factory.CreateMotorcycleChassis();
        }

        [Theory]
        [InlineData(EVehicleType.Car)]
        [InlineData(EVehicleType.Motorcycle)]
        public void CreateAxle_SpecificType_Works(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            IChassisFactory factory = services.GetRequiredService<IChassisFactory>();
            IChassis chassis = factory.CreateChassis(vehicleType);
            Assert.Equal(vehicleType, chassis.VehicleType);
        }
    }
}
