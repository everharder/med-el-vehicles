using MedEl.Vehicles.Model.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Logic.TireChange
{
    internal class TireChangeService : ITireChangeService
    {
        public void ChangeTires(IVehicle vehicle, IEnumerable<ITire> tires)
        {
            int expectedtireCount = vehicle.Chassis.Axles.Sum(x => x.Tires.Count());
            int actualTireCount = tires.Count();
            if (expectedtireCount != actualTireCount)
            {
                throw new ArgumentException($"Expected {expectedtireCount}, but got {actualTireCount} tires");
            }

            for (int i = 0; i < vehicle.Chassis.Axles.Count; i++)
            {
                IAxle axle = vehicle.Chassis.Axles[i];
                ChangeTires(vehicle, i, tires.Take(axle.TireCount));
                tires = tires.Skip(axle.TireCount);
            }
        }

        public void ChangeTires(IVehicle vehicle, int axleIndex, IEnumerable<ITire> tires)
        {
            IAxle? axle = vehicle.Chassis.Axles.Skip(axleIndex).FirstOrDefault();
            if (axle == null)
            {
                throw new ArgumentOutOfRangeException($"Invalid axle index {axleIndex}. Must be between zero and {vehicle.Chassis.Axles.Count} for vehicle {vehicle.Id}");
            }
            axle.SetTires(tires);
        }
    }
}
