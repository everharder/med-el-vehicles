using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Interfaces
{
    /// <summary>
    /// Interface for human readable ToString
    /// </summary>
    public interface IPrettyPrintable
    {
        /// <summary>
        /// Summerizes the objects state in a user-friendly way
        /// </summary>
        public string ToPrettyString();
    }
}
