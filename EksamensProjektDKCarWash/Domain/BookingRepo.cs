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
            foreach (Booking item in bookings)
            {
                if (item.Id == bookingId)
                {
                    bookings.Remove(item);
                }
            }
        }
    }
}
