using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Model.Factory
{
    public interface IVehicleFactory
    {
        ICar CreateCar(IManufacturer manufacturer);
        IMotorcycle CreateMotorcycle(IManufacturer manufacturer);
    }
}