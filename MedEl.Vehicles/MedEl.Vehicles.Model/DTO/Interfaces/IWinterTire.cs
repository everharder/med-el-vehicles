using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Model.DTO
{
    public interface IWinterTire : ITire
    {
        float MinimumTemperature { get; }
        float Thickness { get; }
    }
}