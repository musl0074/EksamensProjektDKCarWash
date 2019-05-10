﻿using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IConnector
    {
        Booking Sp_CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, List<Package> packages, Vehicle vehicle, string vat = "");
        void Sp_UpdateBooking(Booking currentBooking);
        void Sp_DeleteBooking(int bookingId);
        List<Booking> Sp_ShowBooking(DateTime bookingDate);
        int Sp_FindSingleBookingWithID(int bookingId);
        List<Booking> Sp_GetAllBookings();
        List<Package> Sp_GetAllPackages();
    }
}
     