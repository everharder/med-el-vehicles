using MedEl.Vehicles.Common.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Configuration
{
    public class RepositoryConfiguration : BaseConfiguration
    {
        public RepositoryConfiguration(IConfigurationDictionary configuration) : base(configuration)
        {
        }

        protected override string settingPrefix => "Repository";

        public bool UseCaching => GetConfiguration(defaultValue: true);

        public string? FileSystemRepositoryPath => GetConfiguration(defaultValue: string.Empty);
    }
}
