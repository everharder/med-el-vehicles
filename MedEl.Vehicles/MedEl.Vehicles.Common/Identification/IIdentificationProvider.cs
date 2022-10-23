using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedEl.Vehicles.Common.Identification;

namespace MedEl.Vehicles.Common.Interfaces
{
    public interface IIdentificationProvider
    {
        public string GetId<TIdentification>() where TIdentification : IIdentification;
    }
}
