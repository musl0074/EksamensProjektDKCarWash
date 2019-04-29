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
        public DateTime StartTime { get; set; }
        public DateTime BookingDate { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Vat { get; set; }
        public int Id { get; set; }
        public Package Package { get; set; }


 
        public Booking(string customerName, DateTime startTime, DateTime bookingDate, string email, string telephone, Package package, int id, string vat = "")
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
    }
}
