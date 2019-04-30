﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ShowBookingViewModel
    {
        DBViewModel dbvm = new DBViewModel();
        BookingRepo br = new BookingRepo();


        public List<Booking> ShowBooking(DateTime bookingDate)
        {
            for (int i = 0; i < 5; i++)
            {
                List<Booking> b1 = dbvm.Sp_ShowBooking(bookingDate);
                foreach (Booking booking in b1)
                {
                    br.AddToShowBookingsList(booking);
                }
                bookingDate.AddDays(1);
            }
           return br.getShowBookingsList();
        }
    }
}