using System.Runtime.CompilerServices;

namespace MedEl.Vehicles.Common.Configuration
{
    public interface IConfiguration
    {
        TValue? GetConfiguration<TValue>(TValue? defaultValue = default, [CallerMemberName] string? settingName = null);
    }
}