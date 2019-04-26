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
        public void CreateBooking(string customerName, DateTime startTime, string email, string telephone, int package)
        {
            throw new NotImplementedException();
        }
    }
}
