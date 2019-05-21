using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace ApplicationLayer
{
    public class BookingRepo : IPublisher
    {
        private static BookingRepo instance;

        private List<Booking> bookings = new List<Booking>();
        private List<Booking> showBookingsList = new List<Booking>();
        private List<string> bookingsData = new List<string>();
        private List<ISubscriber> subscribers = new List<ISubscriber>();

        public static BookingRepo GetInstance ()
        {
            if (instance == null)
                instance = new BookingRepo();

            return instance;
        }


        public void AddBookingToList(Booking b)
        {
            bookings.Add(b);

            NotifySubscribers();
        }

        public List<Booking> GetBookings()
        {
            return bookings;
        }

        public Booking GetBooking(int bookingId)
        {
            foreach (Booking booking in bookings)
            {
                if(booking.Id == bookingId)
                {
                    return booking;
                }
            }
            return null;
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

            NotifySubscribers();
        }

        public void AddToShowBookingsList(Booking b) // Tror ikke denne er nødvendig længere??
        {
            showBookingsList.Add(b);
        }


        public string UpdateBooking(Booking currentBooking)
        {
            foreach (Booking booking in bookings)
            {
                if(currentBooking.Id == booking.Id)
                {
                    bookings.Remove(booking);
                    bookings.Add(currentBooking);

                    NotifySubscribers();

                    return "Booking er ændret!";
                }
            }

            return "En fejl er forekommet og booking er ikke ændret!";
        }

        public void UpdateAllBookings(List<Booking> allBookings)
        {
            bookings.Clear();
            foreach (Booking booking in allBookings)
            {
                bookings.Add(booking);
            }

        }

        public void ClearAllBookings()
        {
            bookings.Clear();
        }

        public void AddBookingToBookingsData(Booking booking)
        {
            bookingsData.Add(booking.ToString());
        }

        public List<string> GetBookingsData()
        {
            return bookingsData;
        }



        // Observer Pattern Specific Methods
        public void RegisterSubscriber(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        public void NotifySubscribers()
        {
            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.Update();
            }
        }
    }
}
