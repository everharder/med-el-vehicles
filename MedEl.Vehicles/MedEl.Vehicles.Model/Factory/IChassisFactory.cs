using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;

namespace MedEl.Vehicles.Model.Factory
{
    public interface IChassisFactory
    {
        IChassis CreateCarChassis();
        IChassis CreateChassis(EVehicleType vehicleType);
        IChassis CreateMotorcycleChassis();
    }
}