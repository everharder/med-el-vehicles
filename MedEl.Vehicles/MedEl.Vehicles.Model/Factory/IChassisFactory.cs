using MedEl.Vehicles.Model.DTO.Interfaces;

namespace MedEl.Vehicles.Model.Factory
{
    public interface IChassisFactory
    {
        IChassis CreateCarChassis();
        IChassis CreateMotorcycleChassis();
    }
}