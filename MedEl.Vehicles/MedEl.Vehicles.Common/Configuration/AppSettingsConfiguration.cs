using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Configuration
{
    internal class AppSettingsConfiguration : IConfigurationDictionary
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration appSettings;

        public AppSettingsConfiguration(Microsoft.Extensions.Configuration.IConfiguration appSettings)
        {
            this.appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        }

        /// <inheritdoc/>
        public string this[string key] 
        {
            get => this.appSettings[key];
            set => this.appSettings[key] = value;
        }
    }
}
