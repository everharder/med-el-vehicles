using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Interfaces
{
    /// <summary>
    /// Interface for object instance validation
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Checks constraints of this instance and validates its current state
        /// </summary>
        /// <returns>True if the validation succeeds, False if there are issues</returns>
        public bool Validate(out string issues);
    }
}
