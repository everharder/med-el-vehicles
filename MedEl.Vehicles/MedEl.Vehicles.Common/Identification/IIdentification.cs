using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Common.Identification
{
    /// <summary>
    /// Interface for objects that are persistable 
    /// </summary>
    public interface IIdentification
    {
        /// <summary>
        /// The id (unique primary key)
        /// </summary>
        public string Id { get; }
    }
}
