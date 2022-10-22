using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IVehicle : IDTO
    {
        public IManufacturer Manufacturer { get; }
        public EVehicleType VehicleType { get; }

        public void Move();
    }
}