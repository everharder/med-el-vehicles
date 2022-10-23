using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IChassis : IDTO
    {
        EVehicleType VehicleType { get; }
        List<IAxle> Axles { get; }
    }
}