using MedEl.Vehicles.Model.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    internal class CarAxle : Axle
    {
        public CarAxle(string id, ITire leftTire, ITire rightTire) : base(id)
        {
            LeftTire = leftTire ?? throw new ArgumentNullException(nameof(leftTire));
            RightTire = rightTire ?? throw new ArgumentNullException(nameof(rightTire));
        }

        public ITire LeftTire { get; private set; }

        public ITire RightTire { get; private set; }

        public override IEnumerable<ITire> Tires => new ITire[] { LeftTire, RightTire };

        public override int TireCount => 2;

        public override string ToPrettyString()
        {
            return new StringBuilder()
                .AppendLine($"\tLeft Tire:\t {LeftTire.ToPrettyString()}")
                .AppendLine($"\tRight Tire:\t {RightTire.ToPrettyString()}")
                .ToString();
        }

        public override void SetTires(IEnumerable<ITire> tires)
        {
            base.SetTires(tires);
            this.LeftTire = tires.First();
            this.RightTire = tires.Last();
        }
    }
}
