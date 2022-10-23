using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Model.Factory
{
    internal interface IAxleFactory
    {
        IAxle CreateCarAxle();
        IAxle CreateMotorcycleAxle();
    }
}