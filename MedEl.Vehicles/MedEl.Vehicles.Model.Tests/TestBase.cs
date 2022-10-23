using MedEl.Vehicles.Model.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace MedEl.Vehicles.Repository.Tests
{
    public class TestBase : IDisposable
    {
        public TestBase()
        {
        }

        protected virtual IServiceCollection createServiceCollection()
        {
            return new ServiceCollection()
                .AddModelTests();
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