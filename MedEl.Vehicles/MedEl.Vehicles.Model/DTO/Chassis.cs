using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    public class Chassis : IChassis, IPersistable
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Chassis(string id, IEnumerable<ITire> tires)
        {
            if (tires is null)
            {
                throw new ArgumentNullException(nameof(tires));
            }

            Tires = tires.ToList();
            Id = id;
        }

        /// <summary>
        /// The tires attached to the chassis
        /// </summary>
        public List<ITire> Tires { get; }

        public string Id { get; }
    }
}
