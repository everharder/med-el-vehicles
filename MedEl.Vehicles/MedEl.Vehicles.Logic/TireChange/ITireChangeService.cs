using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Logic.TireChange
{
    public interface ITireChangeService
    {
        void ChangeTires(IVehicle vehicle, IEnumerable<ITire> tires);
        void ChangeTires(IVehicle vehicle, int axleIndex, IEnumerable<ITire> tires);
    }
}