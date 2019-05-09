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
        public Package Package { get; private set; }


 
        public Booking(Customer customer, string startTime, DateTime bookingDate, Package package, int id)
        {
            Customer = customer;
            StartTime = startTime;
            BookingDate = bookingDate;
            Package = package;     
            Id = id;
        }
        public Booking(Customer customer, string startTime, DateTime bookingDate, Package package)
        {
            Customer = customer;
            StartTime = startTime;
            BookingDate = bookingDate;
            Package = package;
        }

        public void UpdateBooking(Customer customer, string startTime, DateTime bookingDate, Package package)
        {
            Customer = customer;
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
