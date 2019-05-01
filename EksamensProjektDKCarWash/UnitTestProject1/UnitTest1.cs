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
        Booking b1, b2, b3, b4, b5;
        Package p1, p2, p3;
        BookingRepo brT;
        BookingViewModel bvmT;
        ShowBookingViewModel sbvmT;
        CreateBookingViewModel cbvmT;
        DBViewModel dbvmT;


        [TestInitialize]
        public void TestInitialize()
        {
            dbvmT = new DBViewModel();
            cbvmT = new CreateBookingViewModel();
            bvmT = new BookingViewModel();
            brT = new BookingRepo();
            sbvmT = new ShowBookingViewModel();
            p1 = new Package("Premium Pakke", "Polering og bilrude", 250.43);
            p2 = new Package("Luksus Pakke", "Polering og bilrude", 25000);
            p3 = new Package("Basis Pakke", "Polering og bilrude", 4444);
            b1 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 2, "");
            b2 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 4, "");
            b3 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 3, "");
            b4 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 5, "");
        }

        
        [TestMethod]
        public void TestBookingId()
        {
            cbvmT.CreateBooking("Frank", "12.30", new DateTime(2019, 9, 22, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            Assert.IsTrue(0<b1.Id);
        }

        [TestMethod]
        public void TestAtToBookingRepo()
        {
            List<Booking> bookingsT;
            brT.CreateBooking(b1);
            bookingsT = brT.GetBookings();
            Assert.IsTrue(bookingsT.Count > 0);
        }

        [TestMethod]
        public void DeletedFromBookingRepo () // Testen bliver KUN grøn, hvis der KUN er ÉN booking inde i systemet, når testen bliver kørt
        {
            //  b1 = brT.Sp_CreatePrivateBooking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 22, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            //b1.CustomerName = "Frank";
            //b1.StartTime = new DateTime(2019, 9, 22, 10, 00, 00);
            //b1.BookingDate = new DateTime(2019, 9, 24, 10, 00, 00);
            //b1.Email = "frank@eal.dk";
            //b1.Telephone = "+4522334455";
            //b1.Package = p1;
            //b1.Vat = "88888888";
            //b1.Id = 2;
           
            // brT.CreateBooking(b1);
            // List<Booking> bookingsT = brT.GetBookings();
            //bookingsT.Add(b1);
            //bookingsT.Add(b2);
            //bookingsT.Add(b3);
            //bookingsT.Add(b4);
            brT.CreateBooking(b1);
            brT.CreateBooking(b2);
            brT.CreateBooking(b3);
            brT.CreateBooking(b4);
            //List<Booking> bookingT = brT.GetBookings();
            brT.DeleteBooking(2);
            Assert.AreEqual(brT.GetBookings().Count, 3);
        }

        [TestMethod]
        public void DeleteBookingFromDB() // testen virker kun hvis der eksister en booking med det givne ID
        {
            dbvmT.Sp_DeleteBooking(20);
          //  dbvmT.Sp_FindsingleBookingWithID;
            Assert.IsTrue(dbvmT.Sp_FindsingleBookingWithID(20) == 0);
        }

        [TestMethod]
        public void TestShowBooking()
        {
            List<Booking> bookingsT;
            cbvmT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 01, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            cbvmT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 02, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            cbvmT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 03, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            cbvmT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 04, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            cbvmT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 05, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            bookingsT = sbvmT.ShowBooking(new DateTime(2019, 12, 01, 00, 00, 00));           
            Assert.IsTrue(bookingsT.Count == 5);
        }

        [TestMethod]
        public void TestFindBooking()
        {
            Booking b1 = new Booking("Frank", "10.30", new DateTime(2019, 12, 03, 00, 00, 00), "frank@eal.dk", "+4511223344", p2, 2, "");
            brT.CreateBooking(b1);
            Booking b2 = brT.GetBooking(2);
            Assert.AreSame(b2.Id, b1.Id);

        }

        [TestMethod]
        public void TestUpdateBookingRepo()
        {
            Booking b1 = new Booking("Frank", "10.30", new DateTime(2019, 12, 03, 00, 00, 00), "frank@eal.dk", "+4511223344", p2, 2, "");
            brT.CreateBooking(b1);
            Booking b2 = brT.GetBooking(2);
            b2.CustomerName = "Ricky";
            brT.UpdateBooking(b2);
            Booking b3 = brT.GetBooking(2);
            Assert.AreSame(b2.CustomerName, b3.CustomerName);
        }

        [TestMethod]
        public void TestUpdateBookingDB() // testen virker kun en gang ellers skal CustomerName ændres.
        {
            List <Booking> bookingsT  =dbvmT.Sp_GetAllBookings();
            brT.GetAllBookings(bookingsT);
            Booking b1 = brT.GetBooking(22);
            Booking b2 = new Booking("Ricky", "10.30", new DateTime(2019, 12, 03, 00, 00, 00), "frank@eal.dk", "+4511223344", p2, 22, "");
            dbvmT.Sp_UpdateBooking(b2);
            bookingsT = dbvmT.Sp_GetAllBookings();
            brT.GetAllBookings(bookingsT);
            Booking b3 = brT.GetBooking(22);


            Assert.AreSame(b2.CustomerName, b3.CustomerName);
        }
    }
}
