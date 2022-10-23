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

        public abstract IEnumerable<ITire> Tires { get; }
        public abstract int TireCount { get; }

        public virtual void SetTires(IEnumerable<ITire> tires)
        {
            ITire[] t = tires.ToArray();
            if (t.Length != TireCount)
            {
                throw new ArgumentException($"Expected {TireCount} tire(s), got {t.Length}");
            }
        }
    }
}
