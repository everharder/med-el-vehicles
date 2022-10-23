using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Model.DTO
{
    public interface ISummerTire : ITire
    {
        float MaximumTemperature { get; }
    }
}