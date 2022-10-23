using MedEl.Vehicles.Model.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal abstract class Axle : DTOBase, IAxle
    {
        protected Axle(string id) : base(id)
        {
        }
    }
}
