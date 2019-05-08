using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class Vehicle
    {
        public string LicensePlate { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string TypeOfCar { get; private set; }

        public Vehicle(string licensePlate, string brand, string model, string typeOfCar)
        {
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            TypeOfCar = typeOfCar;
        }
    }
}
