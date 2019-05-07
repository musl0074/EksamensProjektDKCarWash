using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class Vehicle
    {
        string LicensePlate { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        string TypeOfCar { get; set; }

        public Vehicle(string licensePlate, string brand, string model, string typeOfCar)
        {
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            TypeOfCar = typeOfCar;
        }
    }
}
