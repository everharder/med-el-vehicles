using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.Enums
{
    [Flags]
    public enum EVehicleType : byte
    {
        Car = 0,
        Motorcycle = 1,
    }
}
