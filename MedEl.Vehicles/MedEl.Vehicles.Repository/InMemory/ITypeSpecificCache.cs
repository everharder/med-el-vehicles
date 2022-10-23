namespace MedEl.Vehicles.Repository.InMemory
{
    internal interface ITypeSpecificCache
    {
        Type ElementType { get; }
    }
}