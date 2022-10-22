using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Configuration
{
    /// <summary>
    /// Common base class for all configurations
    /// Allows to access optional appsettings.json config values and use fallback values if desired
    /// </summary>
    public abstract class BaseConfiguration
    {
        private readonly IConfigurationDictionary configuration;

        /// <summary>
        /// Creates a new instance of the base configuration
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseConfiguration(IConfigurationDictionary configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected virtual string settingPrefix => string.Empty;

        protected TValue? getConfig<TValue>(TValue? defaultValue = default, [CallerMemberName] string? settingName = null)
        {
            if (string.IsNullOrWhiteSpace(settingName))
            {
                throw new ArgumentNullException(nameof(settingName));
            }

            string prefix = settingPrefix;
            if (!string.IsNullOrWhiteSpace(prefix) && !prefix.EndsWith(":"))
            {
                prefix += ":";
            }

            string settingValue = configuration[$"{prefix}{settingName}"];
            if (string.IsNullOrWhiteSpace(settingValue))
            {
                return defaultValue;
            }
            return (TValue)Convert.ChangeType(settingValue, typeof(TValue));
        }
    }
}
