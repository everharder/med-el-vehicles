﻿using MedEl.Vehicles.Model.Enums;
using MedEl.Vehicles.Model.Interfaces;

namespace MedEl.Vehicles.Model.DTO.Interfaces
{
    public interface IVehicle : IPrettyPrintable, IValidatable, IPersistable
    {
        public IManufacturer Manufacturer { get; }
        public EVehicleType VehicleType { get; }

        public void Move();
    }
}