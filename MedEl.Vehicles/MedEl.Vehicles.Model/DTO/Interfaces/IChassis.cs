namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IChassis : IDTO
    {
        List<ITire> Tires { get; }
    }
}