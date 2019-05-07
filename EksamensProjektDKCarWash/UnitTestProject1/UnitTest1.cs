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
        PickUpDeal pud1;
        Package p1, p2, p3;
        Driver d1, d2, d3, d4;
        PickUpTruck put1, put2, put3;
        Vehicle v1, v2, v3, v4;
        BookingRepo brT;
        BookingController bcT;
        PickUpDealController pudcT;
        PickUpDealRepo pudrT;
        DBController dbcT;


        [TestInitialize]
        public void TestInitialize()
        {
            dbcT = new DBController();
            bcT = new BookingController();
            pudcT = new PickUpDealController();
            brT = new BookingRepo();
            p1 = new Package("Premium Pakke", "Polering og bilrude", 250.43);
            p2 = new Package("Luksus Pakke", "Polering og bilrude", 25000);
            p3 = new Package("Basis Pakke", "Polering og bilrude", 4444);
            d1 = new Driver("Mathias");
            d2 = new Driver("Muslim");
            d3 = new Driver("Adam");
            put1 = new PickUpTruck("Autotransporter 1");
            v1 = new Vehicle("12ty432", "BMW", "X5", "SUV");
            b1 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 2, "");
            b2 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 4, "");
            b3 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 3, "");
            b4 = new Booking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 5, "");
            pud1 = new PickUpDeal(1, d1, put1, "Odense M", 5230, v1, 500, "Rødegårdsvej 273", new DateTime(2019, 9, 24, 10, 00, 00), "15.30");
        }


        [TestMethod]
        public void TestCreateBookingRepo()
        {
            brT.ClearAllBookings();
            brT.AddBookingToList(b1);
            Assert.IsTrue(0 < brT.GetBookings().Count);
        }

        [TestMethod]
        public void TestCreateBookingId()
        {
            b1 = dbcT.Sp_CreateBooking("Frank", "12.30", new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1);
            Assert.IsTrue(b1.Id > 0);
        }

        [TestMethod]
        public void TestCreatePickUpDealRepo()
        {
            pudrT.ClearPickUpDeals();
            pudrT.AddPickUpDealToList(pud1);
            Assert.IsTrue(0 < pudrT.GetPickUpDeals().Count);
        }

        [TestMethod]
        public void TestCreatePickUpDealId()
        {
            pud1 = dbcT.Sp_CreatePickUpDeal(d1, put1, 5230, v1, 2000, "rødegårdsvej", new DateTime(2019, 9, 24, 10, 00, 00), "15.30");
            Assert.IsTrue(pud1.PickUpId > 0);
        }

        [TestMethod]
        public void DeletedFromBookingRepo() // Testen bliver KUN grøn, hvis der KUN er ÉN booking inde i systemet, når testen bliver kørt
        {
   
            brT.AddBookingToList(b1);
            brT.AddBookingToList(b2);
            brT.AddBookingToList(b3);
            brT.AddBookingToList(b4);
            brT.DeleteBooking(2);
            Assert.AreEqual(brT.GetBookings().Count, 3);
        }

        [TestMethod]
        public void DeleteBookingFromDB() // testen virker kun hvis der eksister en booking med det givne ID
        {
            dbcT.Sp_DeleteBooking(20);
            Assert.IsTrue(dbcT.Sp_FindsingleBookingWithID(20) == 0);
        }

        [TestMethod]
        public void DeleteCompareRepoAndDB()
        {
            List<Booking> bookingDBList = dbcT.Sp_GetAllBookings();
            brT.UpdateAllBookings(bookingDBList);
            brT.DeleteBooking(4);
            dbcT.Sp_DeleteBooking(4);
            bookingDBList = dbcT.Sp_GetAllBookings();
            List<Booking> bookingRepoList = brT.GetBookings();
            Assert.AreEqual(bookingDBList.Count, bookingRepoList.Count);
        }

        [TestMethod]
        public void TestShowBooking()
        {
            List<Booking> bookingsT;
            bcT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 24, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            bcT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 25, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            bcT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 26, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            bcT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 27, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            bcT.CreateBooking("Frank", "12.30", new DateTime(2019, 12, 28, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            bookingsT = bcT.ShowBooking(new DateTime(2019, 12, 24, 00, 00, 00));
            Assert.IsTrue(bookingsT.Count == 5);
        }

        [TestMethod]
        public void TestFindBooking()
        {
            Booking b1 = new Booking("Frank", "10.30", new DateTime(2019, 12, 03, 00, 00, 00), "frank@eal.dk", "+4511223344", p2, 2, "");
            brT.AddBookingToList(b1);
            Booking b2 = brT.GetBooking(2);
            Assert.AreEqual(b2.Id, b1.Id);
        }

        [TestMethod]
        public void TestUpdateBookingRepo()
        {
            Booking b1 = new Booking("Frank", "10.30", new DateTime(2019, 12, 03, 00, 00, 00), "frank@eal.dk", "+4511223344", p2, 2, "");
            brT.AddBookingToList(b1);
            Booking b2 = brT.GetBooking(2);
            //b2.Customer.CustomerName = "Ricky"; DEN KAN IKKE SETTES DA DEN ER PRIVAT
            brT.UpdateBooking(b2);
            Booking b3 = brT.GetBooking(2);
            Assert.AreSame(b2.Customer.CustomerName, b3.Customer.CustomerName);
        }

        [TestMethod]
        public void TestUpdateBookingDB() // testen virker kun en gang ellers skal CustomerName ændres.
        {
            List<Booking> bookingsT = dbcT.Sp_GetAllBookings();
            brT.UpdateAllBookings(bookingsT);
            Booking b1 = brT.GetBooking(22);
            dbcT.Sp_DeleteBooking(1);
            Booking b2 = new Booking("Ricky", "10.30", new DateTime(2019, 12, 03, 00, 00, 00), "muslim@ali.dk", "+451111111", p2, 53, "");
            dbcT.Sp_UpdateBooking(b2);
            bookingsT = dbcT.Sp_GetAllBookings();
            brT.UpdateAllBookings(bookingsT);

            Booking b3 = brT.GetBooking(53);


            Assert.AreEqual(b2.Customer.CustomerName, b3.Customer.CustomerName);
        }
    }
}
