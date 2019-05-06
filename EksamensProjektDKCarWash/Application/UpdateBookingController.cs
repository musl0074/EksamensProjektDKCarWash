using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UpdateBookingController
    {
       public Booking currentBooking { get; set; }
       private DBController dbc = new DBController();
       private BookingRepo br = new BookingRepo();


        public string GetBooking(int bookingID)
        {
            currentBooking = br.GetBooking(bookingID);
            return currentBooking.ToString();
        }

        public string UpdateBooking(string customerName, DateTime bookingDate, string startTime, string email, string telephone, string vat, Package package)
        {
            currentBooking.UpdateBooking(customerName, bookingDate, startTime, email, telephone, vat, package);
            dbc.Sp_UpdateBooking(currentBooking);
            return br.UpdateBooking(currentBooking);
        }


    }
}
