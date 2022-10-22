using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Configuration
{
    public class SimpleConfiguration : IConfigurationDictionary
    {
        private readonly Dictionary<string, string> configuration = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <inheritdoc/>
        public string this[string key]
        {
            get => this.configuration.TryGetValue(key, out string? value) ? value : string.Empty;
            set => this.configuration[key] = value;
        }
    }
}
