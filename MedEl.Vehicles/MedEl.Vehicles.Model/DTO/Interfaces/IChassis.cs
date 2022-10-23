namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IChassis : IDTO
    {
        List<IAxle> Axles { get; }
    }
}