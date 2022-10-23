using MedEl.Vehicles.Common.Interfaces;
using MedEl.Vehicles.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IManufacturer : IDTO, ITablePrintable
    {
        public string Name { get; }

        public EVehicleType SupportedVehicleTypes { get; }
    }
}
