using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.Factory
{
    public interface IManufacturerFactory
    {
        IManufacturer CreateManufacturer(string name, EVehicleType vehicleTypes);
    }
}