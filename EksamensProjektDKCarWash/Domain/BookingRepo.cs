using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BookingRepo
    {
        private List<Booking> bookings = new List<Booking>();
        private List<Booking> showBookingsList = new List<Booking>();
        public void CreateBooking(Booking b)
        {
            bookings.Add(b);
        }

        public List<Booking> GetBookings()
        {
            return bookings;
        }

        public void DeleteBooking(int bookingId)
        {
            foreach (Booking item in bookings.ToList())
            {
                if (item.Id == bookingId)
                {
                    bookings.Remove(item);
                }
            }
        }

        public void AddToShowBookingsList(Booking b)
        {
            showBookingsList.Add(b);
        }

        public List<Booking> getShowBookingsList()
        {
            return showBookingsList;
        }
    }
}
