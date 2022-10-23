using MedEl.Vehicles.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Configuration
{
    public class SummerTireConfiguration
    {
        public float Pressure { get; set; }
        public float MaximumTemperature { get; set; }

        public SummerTireConfiguration(IConfiguration configuration)
        {
            this.Pressure = configuration.GetConfiguration(settingName: "Defaults:SummerTire:Pressure", defaultValue: 2.5f);
            this.MaximumTemperature = configuration.GetConfiguration(settingName: "Defaults:SummerTire:MaximumTemperature", defaultValue: 50f);
        }
    }
}
