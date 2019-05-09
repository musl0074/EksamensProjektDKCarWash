using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class PickUpAgreement
    {
        public int PickUpId { get; private set; }
        public Driver Driver { get; private set; }
        public PickUpTruck PickUpTruck { get; private set; }
        public string City { get; private set; }
        public int PostalCode { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public double Price { get; private set; }
        public string StreetName { get; private set; }
        public DateTime PickUpDate { get; private set; }
        public string PickUpTime { get; private set; }

        public PickUpAgreement(int pickUpId, Driver driver, PickUpTruck pickUpTruck, string city, int postalCode, Vehicle vehicle, double price, string streetName, DateTime pickUpDate, string pickUpTime)
        {
            PickUpId = pickUpId;
            Driver = driver;
            PickUpTruck = pickUpTruck;
            City = city;
            PostalCode = postalCode;
            Vehicle = vehicle;
            Price = price;
            StreetName = streetName;
            PickUpDate = pickUpDate;
            PickUpTime = pickUpTime;

        }
    }
}
