using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using Application;
//using UI;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1

    {
        DBViewModel dbvmT = new DBViewModel();
        BookingViewModel bvmT = new BookingViewModel();
        BookingRepo brT = new BookingRepo();
        Booking b1;

        
        [TestMethod]
        public void TestBookingId()
        {
            b1 = dbvmT.Sp_CreateBooking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), "frank@eal.dk", "+4511223344", 3);
            Assert.AreEqual(3, b1.Id);
        }

        [TestMethod]
        public void TestAtToBookingRepo()
        {
            List<Booking> bookingsT;
            b1 = dbvmT.Sp_CreateBookingStump("Frank", new DateTime(2019, 9, 22, 10, 00, 00), "frank@eal.dk", "+4511223344", 3);
            brT.CreateBooking(b1);
            bookingsT = brT.GetBookings();
            Assert.IsTrue(bookingsT.Count > 0);

        }

    }
}
