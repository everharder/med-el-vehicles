using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Interfaces
{
    /// <summary>
    /// Interface for objects that are persistable 
    /// </summary>
    public interface IPersistable
    {
        /// <summary>
        /// The id (unique primary key)
        /// </summary>
        public string Id { get; }
    }
}
