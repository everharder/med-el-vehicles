using MedEl.Vehicles.Common.Identification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.CLI
{
    internal class Context
    {
        public IIdentification? SelectedElement { get; set; }
    }
}
