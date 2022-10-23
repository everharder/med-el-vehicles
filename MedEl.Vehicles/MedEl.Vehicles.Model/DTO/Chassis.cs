using MedEl.Vehicles.Common.Identification;
using MedEl.Vehicles.Model.DTO.Interfaces;
using MedEl.Vehicles.Model.Enums;
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
        public Chassis(string id, EVehicleType vehicleType, IEnumerable<IAxle> axles) : base(id)
        {
            if (axles is null)
            {
                throw new ArgumentNullException(nameof(axles));
            }

            Axles = axles.ToList();
            VehicleType = vehicleType;
        }

        /// <summary>
        /// The tires attached to the chassis
        /// </summary>
        public List<IAxle> Axles { get; }

        public EVehicleType VehicleType { get; }

        /// <inheritdoc/>
        public override string ToPrettyString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < Axles.Count; i++)
            {
                sb.AppendLine($"Axle {i + 1}")
                    .AppendLine(Axles[i].ToPrettyString());
            }
            return sb.ToString();
        }
    }
}
