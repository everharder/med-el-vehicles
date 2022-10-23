using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.Factory
{
    public interface IVehicleFactory
    {
        IVehicle Create(EVehicleType vehicleType, IManufacturer manufacturer);
        ICar CreateCar(IManufacturer manufacturer);
        IMotorcycle CreateMotorcycle(IManufacturer manufacturer);
    }
}