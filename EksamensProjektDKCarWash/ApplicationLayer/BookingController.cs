using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DomainLayer;

namespace ApplicationLayer
{
   public class BookingController
    {
        public Booking currentBooking { get; private set; }
        private BookingRepo br = new BookingRepo();
        private PackageRepo pr = new PackageRepo();
        private IConnector dbc = new DBConnector();    



        public void CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, string packageName, string vat = "")
        {
         
            Booking b = dbc.Sp_CreateBooking(customerName, startTime, bookingDate, email, telephone, packageName, vat);
            try
            {
                br.AddBookingToList(b);
            }
            catch (NullReferenceException e)
            {
                //MessageBox.Show(e.Message);
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
                //MessageBox.Show(e.Message);
                throw;
            }
        }

        // Input er en MANDAG
            // Går ind og finder alle datoerne for mandagen, bruger Booking.ToString() og tilføjer den til en liste af strings
                // Dette gør den for Mandag til Lørdag
        public List<Booking> ShowBooking(DateTime bookingDate)
        {
            for (int i = 0; i < 6; i++)
            {
                List<Booking> bookingsList = dbc.Sp_ShowBooking(bookingDate);
                foreach (Booking booking in bookingsList)
                {
                    br.AddBookingToBookingsData(booking);
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

        public string UpdateBooking(string customerName, DateTime bookingDate, string startTime, string email, string telephone, string vat, string packageName)
        {

            Package package = pr.FindPackage(packageName);
            Customer customer = new Customer(customerName, email, telephone, vat);
            currentBooking.UpdateBooking(customer, startTime, bookingDate, package);
            dbc.Sp_UpdateBooking(currentBooking);
            return br.UpdateBooking(currentBooking);
        }

        public List<string> GetTimestamps (DateTime day)
        {
            List<string> timestamps = new List<string>();

            int t8 = 0, t10 = 0, t12 = 0, t14 = 0, t16 = 0;

            List<Booking> bookingsForTheDay = dbc.Sp_ShowBooking(day);

            foreach (Booking booking in bookingsForTheDay)
            {
                switch(booking.StartTime)
                {
                    case "08:00":
                        t8++;
                        break;

                    case "10:00":
                        t10++;
                        break;

                    case "12:00":
                        t12++;
                        break;

                    case "14:00":
                        t14++;
                        break;

                    case "16:00":
                        t16++;
                        break;
                }
            }

            if (t8 != 4)
                timestamps.Add("08:00");

            if (t10 != 4)
                timestamps.Add("10:00");

            if (t12 != 4)
                timestamps.Add("12:00");

            if (t14 != 4)
                timestamps.Add("14:00");

            if (t16 != 4)
                timestamps.Add("16:00");

            return timestamps;
        }
    }
}
