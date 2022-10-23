using MedEl.Vehicles.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Configuration
{
    public class WinterTireConfiguration
    {
        public float Pressure { get; set; }
        public float MinimumTemperature { get; set; }
        public float Thickness { get; set; }

        public WinterTireConfiguration(IConfiguration configuration)
        {
            this.Pressure = configuration.GetConfiguration(settingName: "Defaults:WinterTire:Pressure", defaultValue: 2.5f);
            this.MinimumTemperature = configuration.GetConfiguration(settingName: "Defaults:WinterTire:MinimumTemperature", defaultValue: -75f);
            this.Thickness = configuration.GetConfiguration(settingName: "Defaults:WinterTire:Thickness", defaultValue: 0.01f);
        }
    }
}
