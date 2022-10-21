using MedEl.Vehicles.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository
{
    public class RepositoryConfiguration : BaseConfiguration
    {
        public RepositoryConfiguration(IConfiguration configuration) : base(configuration)
        {
        }

        protected override string settingPrefix => "Repository";

        public bool UseCaching => getConfig(defaultValue: true);

        public string? FileSystemRepositoryPath => getConfig(defaultValue: string.Empty);
    }
}
