﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedEl.Vehicles.Model.DTO
{
    public class SummerTire : Tire
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public SummerTire(string id, float pressure, float maximumTemperature) : base(id, pressure)
        {
            MaximumTemperature = maximumTemperature;
        }

        /// <summary>
        /// The maximum temperature where this tire is operable
        /// </summary>
        public float MaximumTemperature { get; }
    }
}
