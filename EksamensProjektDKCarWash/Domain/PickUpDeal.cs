using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PickUpDeal
    {
        int PickUpId { get; set; }
        Driver Driver { get; set; }
        PickUpTruck PickUpTruck { get; set; }
        string City { get; set; }
        int PostalCode { get; set; }
        Vehicle Vehicle { get; set; }
        double Price { get; set; }
        string StreetName { get; set; }
        DateTime PickUpDate { get; set; }
        string PickUpTime { get; set; }

        public PickUpDeal(int pickUpId, Driver driver, PickUpTruck pickUpTruck, string city, int postalCode, Vehicle vehicle, double price, string streetName, DateTime pickUpDate, string pickUpTime)
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
