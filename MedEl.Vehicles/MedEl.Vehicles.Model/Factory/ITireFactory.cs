using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.Factory
{
    public interface ITireFactory
    {
        ITire CreateSummerTire();
        ITire CreateSummerTire(SummerTireConfiguration configuration);
        IEnumerable<ITire> CreateSummerTires(IVehicle vehicle);
        IEnumerable<ITire> CreateSummerTires(IVehicle vehicle, SummerTireConfiguration configuration);
        ITire CreateTire(ETireType tireType);
        ITire CreateWinterTire();
        ITire CreateWinterTire(WinterTireConfiguration configuration);
        IEnumerable<ITire> CreateWinterTires(IVehicle vehicle);
        IEnumerable<ITire> CreateWinterTires(IVehicle vehicle, WinterTireConfiguration configuration);
    }
}