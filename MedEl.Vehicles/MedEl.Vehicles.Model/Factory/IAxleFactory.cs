using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.Factory
{
    public interface IAxleFactory
    {
        IAxle CreateAxle(EVehicleType vehicleType);
        IAxle CreateCarAxle();
        IAxle CreateMotorcycleAxle();
    }
}