using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IVehicle : IDTO, ITablePrintable
    {
        public IManufacturer Manufacturer { get; }
        public EVehicleType VehicleType { get; }
        IChassis Chassis { get; }

        public void Move();
    }
}