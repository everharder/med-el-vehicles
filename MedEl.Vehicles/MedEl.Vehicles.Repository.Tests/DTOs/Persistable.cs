using MedEl.Vehicles.Common.Identification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Repository.Tests.DTOs
{
    internal class Persistable : IIdentification
    {
        public Persistable(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public string? Payload { get; set; }
    }
}
