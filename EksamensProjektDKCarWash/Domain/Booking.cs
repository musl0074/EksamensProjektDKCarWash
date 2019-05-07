using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Booking
    {
        public Customer Customer { get; private set; }
        public string StartTime { get; private set; }
        public DateTime BookingDate { get; private set; }
        public int Id { get; private set; }
        public Package Package { get; private set; }


 
        public Booking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, Package package, int id, string vat = "")
        {
            Customer = new Customer(customerName, email, telephone, vat);
            StartTime = startTime;
            BookingDate = bookingDate;
            Package = package;     
            Id = id;
        }

        public Booking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, string packageName, int id, string vat = "")
        {
            Customer = new Customer(customerName, email, telephone, vat);
            StartTime = startTime;
            BookingDate = bookingDate;
            Package = new Package(packageName);
            Id = id;
         
        }

        public Booking(string customerName, string startTime, DateTime bookingDate, string packageName)
        {
            Customer = new Customer(customerName);
            StartTime = startTime;
            BookingDate = bookingDate;
            Package = new Package(packageName);
        }

        public void UpdateBooking(string customerName, DateTime bookingDate, string startTime, string email, string telephone, string vat, Package package)
        {
            Customer = new Customer(customerName, email, telephone, vat);
            BookingDate = bookingDate;
            StartTime = startTime;
            Package = package;
        }

        public override string ToString()
        {
            return Customer.CustomerName + ":" + BookingDate.ToString() + ":" + StartTime + ":" + Customer.Email + ":" + Customer.Telephone + ":" + Customer.Vat + ":" + Package.Name + ":";
        }
    }
}
