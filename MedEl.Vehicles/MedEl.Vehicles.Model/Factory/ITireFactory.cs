using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Model.Factory
{
    public interface ITireFactory
    {
        ITire CreateSummerTire();
        ITire CreateSummerTire(SummerTireConfiguration configuration);
        ITire CreateWinterTire();
        ITire CreateWinterTire(WinterTireConfiguration configuration);
    }
}