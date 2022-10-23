using MedEl.Vehicles.Logic.TireChange;
using MedEl.Vehicles.Model.DTO;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Factory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Logic.Tests.TireChange
{
    public class TireChangeServiceTests : TestBase
    {
        [Theory]
        [InlineData(EVehicleType.Motorcycle)]
        [InlineData(EVehicleType.Car)]
        public void ChangeTires_TooFewTires_Throws(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            ITireChangeService service = services.GetRequiredService<ITireChangeService>();

            IVehicleFactory factory = services.GetRequiredService<IVehicleFactory>();
            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>().CreateManufacturer("foobar", EVehicleType.Car | EVehicleType.Motorcycle);
            IVehicle vehicle = factory.Create(vehicleType, manufacturer);

            Assert.Throws<ArgumentException>(() => service.ChangeTires(vehicle, new List<ITire>()));
        }

        [Theory]
        [InlineData(EVehicleType.Motorcycle)]
        [InlineData(EVehicleType.Car)]
        public void ChangeTires_TooManyTires_Throws(EVehicleType vehicleType)
        {
            IServiceProvider services = createServiceProvider();
            ITireChangeService service = services.GetRequiredService<ITireChangeService>();

            IVehicleFactory factory = services.GetRequiredService<IVehicleFactory>();
            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>().CreateManufacturer("foobar", EVehicleType.Car | EVehicleType.Motorcycle);
            IVehicle vehicle = factory.Create(vehicleType, manufacturer);

            ITireFactory tireFactory = services.GetRequiredService<ITireFactory>();
            List<IWinterTire> tires = Enumerable.Range(0, 10)
                .Select(x => tireFactory.CreateWinterTire())
                .ToList();
            Assert.Throws<ArgumentException>(() => service.ChangeTires(vehicle, tires));
        }

        [Theory]
        [InlineData(EVehicleType.Motorcycle, ETireType.WinterTire)]
        [InlineData(EVehicleType.Motorcycle, ETireType.SummerTire)]
        [InlineData(EVehicleType.Car, ETireType.WinterTire)]
        [InlineData(EVehicleType.Car, ETireType.SummerTire)]
        public void ChangeTires_CorrectTires_Changed(EVehicleType vehicleType, ETireType tireType)
        {
            IServiceProvider services = createServiceProvider();
            ITireChangeService service = services.GetRequiredService<ITireChangeService>();

            IVehicleFactory factory = services.GetRequiredService<IVehicleFactory>();
            IManufacturer manufacturer = services.GetRequiredService<IManufacturerFactory>()
                .CreateManufacturer("foobar", EVehicleType.Car | EVehicleType.Motorcycle);
            IVehicle vehicle = factory.Create(vehicleType, manufacturer);

            ITireFactory tireFactory = services.GetRequiredService<ITireFactory>();

            List<ITire> tires = Enumerable.Range(0, vehicle.Chassis.Axles.Sum(x => x.TireCount))
                .Select(x => tireFactory.CreateTire(tireType))
                .ToList();
            service.ChangeTires(vehicle, tires);

            List<ITire> changedTires = vehicle.Chassis.Axles.SelectMany(x => x.Tires).ToList();
            Assert.True(changedTires.All(x => x.Type == tireType));
        }
    }
}
