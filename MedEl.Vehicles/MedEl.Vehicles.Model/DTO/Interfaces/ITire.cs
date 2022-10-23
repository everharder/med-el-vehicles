using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface ITire : IDTO
    {
        ETireType Type { get; }
        float Pressure { get; }
    }
}