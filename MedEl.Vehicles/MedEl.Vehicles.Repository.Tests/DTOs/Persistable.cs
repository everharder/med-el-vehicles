using MedEl.Vehicles.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Tests.DTOs
{
    internal class Persistable : IPersistable
    {
        public Persistable(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public string Payload { get; set; }
    }
}
