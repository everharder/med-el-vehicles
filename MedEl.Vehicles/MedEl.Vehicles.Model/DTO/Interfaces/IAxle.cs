using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IAxle : IDTO
    {
        EVehicleType VehicleType { get; }
        int TireCount { get; } 
        IEnumerable<ITire> Tires { get; }

        void SetTires(IEnumerable<ITire> tires);
    }
}