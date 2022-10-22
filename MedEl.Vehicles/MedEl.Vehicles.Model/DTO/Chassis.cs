using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Model.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class Chassis : DTOBase, IChassis
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Chassis(string id, IEnumerable<ITire> tires) : base(id)
        {
            if (tires is null)
            {
                throw new ArgumentNullException(nameof(tires));
            }

            Tires = tires.ToList();
        }

        /// <summary>
        /// The tires attached to the chassis
        /// </summary>
        public List<ITire> Tires { get; }
    }
}
