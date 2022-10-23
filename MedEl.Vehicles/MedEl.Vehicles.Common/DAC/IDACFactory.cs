using MedEl.Vehicles.Common.DAC;
using MedEl.Vehicles.Common.Identification;

namespace MedEl.Vehicles.Common.DAC
{
    public interface IDACFactory
    {
        IDAC CreateDAC(Type entityType);
        IDAC CreateDAC<TEntity>() where TEntity : IIdentification;
    }
}