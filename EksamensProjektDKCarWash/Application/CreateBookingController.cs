using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Domain;

namespace Application
{
   public class CreateBookingController
    {
        private BookingRepo br = new BookingRepo();
        private DBController dbc = new DBController();



        public void CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, Package package)
        {
            Booking b = dbc.Sp_CreatePrivateBooking(customerName, startTime, bookingDate, email, telephone, package);
            try
            {
                br.CreateBooking(b);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            
        }
    }
}
