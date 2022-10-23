using MedEl.Vehicles.Model.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class MotorcycleAxle : Axle
    {
        public MotorcycleAxle(string id, ITire tire) : base(id)
        {
            Tire = tire ?? throw new ArgumentNullException(nameof(tire));
        }

        public ITire Tire { get; private set; }

        public override IEnumerable<ITire> Tires => new ITire[] { Tire };

        public override int TireCount => 1;


        public override string ToPrettyString()
        {
            return new StringBuilder()
                .AppendLine($"\tTire:\t {Tire}")
                .ToString();
        }

        public override void SetTires(IEnumerable<ITire> tires)
        {
            base.SetTires(tires);
            this.Tire = tires.First();
        }
    }
}
