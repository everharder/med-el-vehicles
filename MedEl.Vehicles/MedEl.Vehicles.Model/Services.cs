using MedEl.Vehicles.Common;
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
            return services.AddCommon();
        }
    }
}
