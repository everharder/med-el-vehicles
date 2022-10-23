using MedEl.Vehicles.Common;
using MedEl.Vehicles.Model.Configuration;
using MedEl.Vehicles.Model.DTO;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Factory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model
{
    public static class Services
    {
        public static IServiceCollection AddModel(this IServiceCollection services)
        {
            return services.AddCommon()
                .AddTransient<SummerTireConfiguration>()
                .AddTransient<WinterTireConfiguration>()
                .AddSingleton<IManufacturerFactory, ManufacturerFactory>()
                .AddSingleton<IVehicleFactory, VehicleFactory>()
                .AddSingleton<IAxleFactory, AxleFactory>()
                .AddSingleton<ITireFactory, TireFactory>()
                .AddSingleton<IChassisFactory, ChassisFactory>();
        }
    }
}
