using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class PickUpAgreement
    {
        public int PickUpAgreementId { get; private set; }
        public Driver Driver { get; private set; }
        public PickUpTruck PickUpTruck { get; private set; }
        public string City { get; private set; }
        public int PostalCode { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public double Price { get; private set; }
        public string StreetName { get; private set; }
        public DateTime PickUpDate { get; private set; }
        public string PickUpTime { get; private set; }

        public PickUpAgreement(int pickUpAgreementId, Driver driver, PickUpTruck pickUpTruck, string city, int postalCode, Vehicle vehicle, double price, string streetName, DateTime pickUpDate, string pickUpTime)
        {
            PickUpAgreementId = pickUpAgreementId;
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

        public void UpdatePickUpAgreement(Driver driver, PickUpTruck pickUpTruck, string city, int postalCode, Vehicle vehicle, double price, string streetName, DateTime pickUpDate, string pickUpTime)
        {
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

        public override string ToString()
        {
            return PickUpAgreementId.ToString() + "," + Vehicle.LicensePlate + "," + Vehicle.Brand + "," + StreetName + "," + PostalCode.ToString() 
                + "," + City + "," + Driver.Name + "," + PickUpDate.ToString() + "," + PickUpTime + "," + Price.ToString() + "," + Vehicle.VehicleID.ToString();
        } 
    }
}
