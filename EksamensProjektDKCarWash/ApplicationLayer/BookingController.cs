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
        public Booking CurrentBooking { get; private set; }
        private BookingRepo br = BookingRepo.GetInstance();
        private PackageRepo pr = PackageRepo.GetInstance();
        private DBConnector dbc = new DBConnector();    


        

        public void CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, List<string> packageNames, string licensePlate, string brand, string vat = "")
        {
            List<Package> packagesFromDB = dbc.Sp_GetAllPackages();
            foreach (Package package in packagesFromDB)
            {
                pr.AddPackageToList(package);
            }
            List<Package> packages = new List<Package>();
            foreach (string packageName in packageNames)
            {
                packages.Add(pr.FindPackage(packageName));
            }
            Booking b = dbc.Sp_CreateBooking(customerName, startTime, bookingDate, email, telephone, packages, licensePlate, brand, vat);
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
            dbc.Sp_DeleteBooking(bookingId);
            
            
        }

        // Input er en MANDAG
        // Går ind og finder alle datoerne for mandagen, bruger Booking.ToString() og tilføjer den til en liste af strings
        // Dette gør den for Mandag til Lørdag
        public List<List<string>> GetWeeklyBookings(DateTime bookingDate)
        {
            List<List<string>> daysWithBooking = new List<List<string>>();
            List<Booking> bookings = br.GetBookings();

            for (int i = 0; i < 6; i++)
            {
                List<string> day = new List<string>();

                foreach (Booking booking in bookings)
                {
                    if (booking.BookingDate == bookingDate)
                        day.Add(booking.ToString());
                }

                daysWithBooking.Add(day);

                bookingDate = bookingDate.AddDays(+1);
            }

            return daysWithBooking;
        }

        public void LoadAllBookingsFromDatabase () // Updates the BookingRepository
        {
            br.UpdateAllBookings(dbc.Sp_GetAllBookings());
        }

        public string GetBooking(int bookingID)
        {
            CurrentBooking = br.GetBooking(bookingID);
            return CurrentBooking.ToString();
        }

        public string UpdateBooking(int customerId, int vehicleId, string customerName, DateTime bookingDate, string startTime, string email, string licensePlate, string brand, string telephone, string vat, List<string> packageNames)
        {

            List<Package> packages = new List<Package>();
            foreach (string packageName in packageNames)
            {
                packages.Add(pr.FindPackage(packageName));
            }
            Vehicle vehicle = new Vehicle(licensePlate, brand, vehicleId);
            Customer customer = new Customer(customerName, email, telephone, vehicle, customerId, vat);
            CurrentBooking.UpdateBooking(customer, startTime, bookingDate, packages);
            dbc.Sp_UpdateBooking(CurrentBooking);
            return br.UpdateBooking(CurrentBooking);
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

            if (t8 < 4)
                timestamps.Add("08:00");

            if (t10 < 4)
                timestamps.Add("10:00");

            if (t12 < 4)
                timestamps.Add("12:00");

            if (t14 < 4)
                timestamps.Add("14:00");

            if (t16 < 4)
                timestamps.Add("16:00");

            return timestamps;
        }
        public List<string>GetDailyBookings(DateTime DailyDateTime)
        {
            List<string> dailyBookingsStrings = new List<string>();
            List<Booking> dailybookings = br.GetBookings();

            foreach (Booking item in dailybookings)
            {
                if (item.BookingDate == DailyDateTime)
                {
                    string tostring = item.ToString();
                    string[] split = tostring.Split(';');
                    string combineString = split[0] + ";" + split[3] + ";" + split[7];
                    dailyBookingsStrings.Add(combineString);
                }
            }

            return dailyBookingsStrings = SortedByStartTime(dailyBookingsStrings);
        }

        public List<string> SortedByStartTime(List<string> unsortedList)
        {
            List<string> list8 = new List<string>();
            List<string> list10 = new List<string>();
            List<string> list12 = new List<string>();
            List<string> list14 = new List<string>();
            List<string> list16 = new List<string>();
            List<string> listSorted = new List<string>();




            foreach (var item in unsortedList)
            {
                string[] split = item.Split(';');
                if (split[1] == "08:00")
                {
                    list8.Add(item);
                }
                if (split[1] == "10:00")
                {
                    list10.Add(item);
                }
                if (split[1] == "12:00")
                {
                    list12.Add(item);
                }
                if (split[1] == "14:00")
                {
                    list14.Add(item);
                }
                if (split[1] == "16:00")
                {
                    list16.Add(item);
                }
            }
            foreach (var item in list8)
            {
                listSorted.Add(item);
            }
            foreach (var item in list10)
            {
                listSorted.Add(item);
            }
            foreach (var item in list12)
            {
                listSorted.Add(item);
            }
            foreach (var item in list14)
            {
                listSorted.Add(item);
            }
            foreach (var item in list16)

            {
                listSorted.Add(item);
            }
            return listSorted;
        }

    }
}
