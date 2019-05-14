﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
   public class Vehicle
    {
        public string LicensePlate { get; private set; }
        public string Brand { get; private set; }
        public int VehicleID { get; private set; }

        public Vehicle(string licensePlate, string brand, int vehicleID)
        {
            LicensePlate = licensePlate;
            Brand = brand;
            VehicleID = vehicleID;
        }
    }
}
