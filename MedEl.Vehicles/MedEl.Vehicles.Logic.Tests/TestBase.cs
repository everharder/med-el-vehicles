using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace MedEl.Vehicles.Logic.Tests
{
    public class TestBase : IDisposable
    {
        public TestBase()
        {
        }

        protected virtual IServiceCollection createServiceCollection()
        {
            return new ServiceCollection()
                .AddLogicTests();
        }

        protected virtual IServiceProvider createServiceProvider()
        {
            return createServiceCollection()
                .BuildServiceProvider();
        }

        public virtual void Dispose()
        {
        }
    }
}