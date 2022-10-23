namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IAxle : IDTO
    {
        int TireCount { get; } 
        IEnumerable<ITire> Tires { get; }

        void SetTires(IEnumerable<ITire> tires);
    }
}