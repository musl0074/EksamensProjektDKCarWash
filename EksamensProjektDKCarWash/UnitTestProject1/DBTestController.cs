using Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class DBTestController : IConnector
    {
        public Booking Sp_CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, string packageName, string vat = "")
        {
            throw new NotImplementedException();
        }

        public void Sp_DeleteBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public int Sp_FindSingleBookingWithID(int bookingId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> Sp_GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public List<Booking> Sp_ShowBooking(DateTime bookingDate)
        {
            throw new NotImplementedException();
        }

        public void Sp_UpdateBooking(Booking currentBooking)
        {
            throw new NotImplementedException();
        }
    }
}
