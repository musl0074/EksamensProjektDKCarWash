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
        Package p1 = new Package("Premium Pakke", "Polering og bilrude", 250.43);
        
        [TestMethod]
        public void TestBookingId()
        {
            b1 = dbvmT.CreatePrivateBooking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), "frank@eal.dk", "+4511223344", p1);
            Assert.IsTrue(0<b1.Id);
        }

        [TestMethod]
        public void TestAtToBookingRepo()
        {
            List<Booking> bookingsT;
            b1 = dbvmT.CreatePrivateBooking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 22, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            brT.CreateBooking(b1);
            bookingsT = brT.GetBookings();
            Assert.IsTrue(bookingsT.Count > 0);

        }

    }
}
