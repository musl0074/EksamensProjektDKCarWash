using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Booking
    {
        public string CustomerName { get; set; }
        public string StartTime { get; set; }
        public DateTime BookingDate { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Vat { get; set; }
        public int Id { get; set; }
        public Package Package { get; set; }
        public string PackageName { get; set; }


 
        public Booking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, Package package, int id, string vat = "")
        {
            CustomerName = customerName;
            StartTime = startTime;
            BookingDate = bookingDate;
            Email = email;
            Telephone = telephone;
            Package = package;     
            Id = id;
            Vat = vat;
        }

        public Booking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, string packageName, int id, string vat = "")
        {
            CustomerName = customerName;
            StartTime = startTime;
            BookingDate = bookingDate;
            Email = email;
            Telephone = telephone;
            PackageName = packageName;
            Id = id;
            Vat = vat;
        }

        public Booking(string customerName, string startTime, DateTime bookingDate, string packageName)
        {
            CustomerName = customerName;
            StartTime = startTime;
            BookingDate = bookingDate;
            PackageName = packageName;
        }

        public void UpdateBooking(string customerName, DateTime bookingDate, string startTime, string email, string telephone, string vat, Package package)
        {
            CustomerName = customerName;
            BookingDate = bookingDate;
            StartTime = startTime;
            Email = email;
            Telephone = telephone;
            Vat = vat;
            Package = package;
        }

        public override string ToString()
        {
            return CustomerName + ":" + BookingDate.ToString() + ":" + StartTime + ":" + Email + ":" + Telephone + ":" + Vat + ":" + Package.Name + ":";
        }
    }
}
