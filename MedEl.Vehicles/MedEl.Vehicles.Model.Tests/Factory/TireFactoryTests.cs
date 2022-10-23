using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.DTO;
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
    public class TireFactoryTests : TestBase
    {
        [Fact]
        public void CreateSummerTire_NoParam_Works()
        {
            IServiceProvider services = createServiceProvider();
            ITireFactory factory = services.GetRequiredService<ITireFactory>();
            factory.CreateSummerTire();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100,  200)]
        [InlineData( 100, -200)]
        [InlineData(-100, -200)]
        [InlineData( 100,  200)]
        public void CreateSummerTire_Config_Works(float pressure, float maxTemp)
        {
            IServiceProvider services = createServiceProvider();
            ITireFactory factory = services.GetRequiredService<ITireFactory>();

            SummerTireConfiguration config = services.GetRequiredService<SummerTireConfiguration>();
            config.Pressure = pressure;
            config.MaximumTemperature = maxTemp;

            if (pressure < 0)
            {
                Assert.Throws<ArgumentException>(() => factory.CreateSummerTire(config));
            }
            else
            {
                ISummerTire tire = factory.CreateSummerTire(config);
                Assert.Equal(ETireType.SummerTire, tire.Type);
                Assert.Equal(tire.Pressure, pressure);
                Assert.Equal(tire.MaximumTemperature, maxTemp);
            }
        }

        [Fact]
        public void CreateWinterTire_NoParam_Works()
        {
            IServiceProvider services = createServiceProvider();
            ITireFactory factory = services.GetRequiredService<ITireFactory>();
            factory.CreateWinterTire();
        }

        [Theory]
        [InlineData( 0, 0, 0)]
        [InlineData( 0, 0, 3)]
        [InlineData( 0, 0,-3)]
        [InlineData( 0, 2, 0)]
        [InlineData( 0,-2, 0)]
        [InlineData( 1, 0, 0)]
        [InlineData(-1, 0, 0)]
        [InlineData(-1,-2,-3)]
        [InlineData( 1, 2, 3)]
        public void CreateWinterTire_Config_Works(float pressure, float minTemp, float thickness)
        {
            IServiceProvider services = createServiceProvider();
            ITireFactory factory = services.GetRequiredService<ITireFactory>();

            WinterTireConfiguration config = services.GetRequiredService<WinterTireConfiguration>();
            config.Pressure = pressure;
            config.MinimumTemperature = minTemp;
            config.Thickness = thickness;

            if (pressure < 0 || thickness <= 0)
            {
                Assert.Throws<ArgumentException>(() => factory.CreateWinterTire(config));
            }
            else
            {
                IWinterTire tire = factory.CreateWinterTire(config);
                Assert.Equal(ETireType.WinterTire, tire.Type);
                Assert.Equal(tire.Pressure, pressure);
                Assert.Equal(tire.MinimumTemperature, minTemp);
                Assert.Equal(tire.Thickness, thickness);
            }
        }

        [Theory]
        [InlineData(ETireType.SummerTire)]
        [InlineData(ETireType.WinterTire)]
        public void CreateTire_NoParam_Works(ETireType tireType)
        {
            IServiceProvider services = createServiceProvider();
            ITireFactory factory = services.GetRequiredService<ITireFactory>();
            ITire tire = factory.CreateTire(tireType);
            Assert.Equal(tireType, tire.Type);
        }

        [Theory]
        [InlineData(EVehicleType.Car, ETireType.SummerTire, 4)]
        [InlineData(EVehicleType.Car, ETireType.WinterTire, 4)]
        [InlineData(EVehicleType.Motorcycle, ETireType.SummerTire, 2)]
        [InlineData(EVehicleType.Motorcycle, ETireType.WinterTire, 2)]
        public void CreateTires_NoParam_Works(EVehicleType vehicleType, ETireType tireType, int expectedCount)
        {
            IServiceProvider services = createServiceProvider();
            ITireFactory factory = services.GetRequiredService<ITireFactory>();

            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>()
                .CreateManufacturer("foobar", EVehicleType.Motorcycle | EVehicleType.Car);

            IVehicle vehicle = services.GetRequiredService<IVehicleFactory>()
                .Create(vehicleType, manufacturer);

            IEnumerable<ITire> tires = factory.CreateTires(vehicle, tireType);
            Assert.Equal(expectedCount, tires.Count());
            Assert.True(tires.All(x => x.Type == tireType));
        }
    }
}
