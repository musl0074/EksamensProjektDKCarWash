using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Domain;

namespace Application
{
   public class BookingController
    {
        public Booking currentBooking { get; private set; }

        private BookingRepo br = new BookingRepo();
        private IConnector dbc = new DBController();
        



        public void CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, Package package, string vat = "")
        {
            Booking b = dbc.Sp_CreateBooking(customerName, startTime, bookingDate, email, telephone, package, vat);
            try
            {
                br.AddBookingToList(b);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }   
        }

        public void DeleteBooking(int bookingId)
        {
            br.DeleteBooking(bookingId);
            try
            {
                dbc.Sp_DeleteBooking(bookingId);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        public List<Booking> ShowBooking(DateTime bookingDate)
        {
            for (int i = 0; i < 5; i++)
            {
                List<Booking> b1 = dbc.Sp_ShowBooking(bookingDate);
                foreach (Booking booking in b1)
                {
                    br.AddToShowBookingsList(booking);
                }
                bookingDate.AddDays(1);
            }
            return br.getShowBookingsList();
        }

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
