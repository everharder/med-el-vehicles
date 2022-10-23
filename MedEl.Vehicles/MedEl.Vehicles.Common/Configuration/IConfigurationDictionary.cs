using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Configuration
{
    public interface IConfigurationDictionary
    {
        /// <summary>
        /// Indexer for settings read/write
        /// </summary>
        string this[string key] { get; set; }
    }
}
