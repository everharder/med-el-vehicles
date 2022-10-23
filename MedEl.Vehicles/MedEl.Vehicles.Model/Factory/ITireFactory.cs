using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.DTO;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.Factory
{
    public interface ITireFactory
    {
        ISummerTire CreateSummerTire();
        ISummerTire CreateSummerTire(SummerTireConfiguration configuration);
        IEnumerable<ISummerTire> CreateSummerTires(IVehicle vehicle);
        IEnumerable<ISummerTire> CreateSummerTires(IVehicle vehicle, SummerTireConfiguration configuration);
        ITire CreateTire(ETireType tireType);
        IEnumerable<ITire> CreateTires(IVehicle vehicle, ETireType tireType);
        IWinterTire CreateWinterTire();
        IWinterTire CreateWinterTire(WinterTireConfiguration configuration);
        IEnumerable<IWinterTire> CreateWinterTires(IVehicle vehicle);
        IEnumerable<IWinterTire> CreateWinterTires(IVehicle vehicle, WinterTireConfiguration configuration);
    }
}