using MedEl.Vehicles.Logic.TireChange;
using MedEl.Vehicles.Model;
using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.Factory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Logic
{
    public static class Services
    {
        public static IServiceCollection AddLogic(this IServiceCollection services)
        {
            return services.AddModel()
                .AddSingleton<ITireChangeService, TireChangeService>();
        }
    }
}
