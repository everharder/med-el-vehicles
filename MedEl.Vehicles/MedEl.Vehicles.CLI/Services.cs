using MedEl.Vehicles.CLI.Commands;
using MedEl.Vehicles.Common;
using MedEl.Vehicles.Common.Configuration;
using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Logic;
using MedEl.Vehicles.Model;
using MedEl.Vehicles.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI
{
    public static class Services
    {
        public static IServiceCollection AddCli(this IServiceCollection services)
        {
            return services.AddCommon()
                .AddModel()
                .AddRepository()
                .AddLogic()
                .AddScoped<Context>()
                .AddSingleton<CommandFactory>()
                .AddTransient<CreateCommand>()
                .AddTransient<SelectCommand>()
                .AddTransient<DeleteCommand>()
                .AddTransient<ChangeTiresCommand>()
                .AddTransient<PrintCommand>()
                .AddTransient<ListCommand>();
        }
    }
}
