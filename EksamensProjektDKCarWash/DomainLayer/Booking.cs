using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class Booking
    {
        public Customer Customer { get; private set; }
        public string StartTime { get; private set; }
        public DateTime BookingDate { get; private set; }
        public int Id { get; private set; }
        public List<Package> Packages { get; private set; }


 
        public Booking(Customer customer, string startTime, DateTime bookingDate, List<Package> packages, int id)
        {
            Customer = customer;
            StartTime = startTime;
            BookingDate = bookingDate;
            Packages = packages;     
            Id = id;
        }
        public Booking(Customer customer, string startTime, DateTime bookingDate, List<Package> packages)
        {
            Customer = customer;
            StartTime = startTime;
            BookingDate = bookingDate;
            Packages = packages;
        }

        public void UpdateBooking(Customer customer, string startTime, DateTime bookingDate, List<Package> packages)
        {
            Customer = customer;
            BookingDate = bookingDate;
            StartTime = startTime;
            Packages = packages;
        }

        public override string ToString()
        {
            List<string> stringPackages = new List<string>(); 
            string packages = string.Empty; // The combined string, providing all the packageNames

            foreach (Package package in Packages)
            {
                packages += "\n" + package.Name + ",";
            }
            // Split[nrs]       0                   1                                   2                               3                     4                     5                       6                   7                       8                                   9                               10                         11
            return "        " + Id + ";" + Customer.CustomerName + ";" + BookingDate.ToString("yyyy:MM:dd") + ";" + StartTime + ";" + Customer.Email + ";" + Customer.Telephone + ";" + Customer.Vat + ";" + packages + ";" + Customer.Vehicle.LicensePlate + ";" + Customer.Vehicle.Brand + ";" + Customer.CustomerId + ";" + Customer.Vehicle.VehicleID;
        }
    }
}
