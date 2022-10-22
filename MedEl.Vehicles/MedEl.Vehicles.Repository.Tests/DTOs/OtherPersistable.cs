using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Tests.DTOs
{
    internal class OtherPersistable : Persistable
    {
        public OtherPersistable(string id) : base(id)
        {
        }
    }
}
