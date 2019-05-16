using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainLayer;
using ApplicationLayer;
//using UI;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1

    {
        Booking b1, b2, b3, b4;
        Customer c1, c2, c3;
        PickUpAgreement puaT;
        Package p1, p2, p3;
        Driver d1, d2, d3;
        PickUpTruck put1;
        Vehicle v1;
        List<Package> packages1;
        List<Package> packages2;
        BookingRepo brT;
        BookingController bcT;
        PickUpAgreementController puacT;
        PickUpAgreementRepo pudrT;
        PackageRepo prT;
        DBConnectorTest dbcT;
        List<string> stringPackagesT;
        List<Booking> bookingsT;
        List<List<string>> bookingStringsT;
        List<PickUpAgreement> pickUpAgreementsT;



        [TestInitialize]
        public void TestInitialize()
        {
            dbcT = new DBConnectorTest();
            bcT = new BookingController();
            puacT = new PickUpAgreementController();
            brT = new BookingRepo();
            pudrT = new PickUpAgreementRepo();
            prT = new PackageRepo();
            p1 = new Package("Premium Pakke", "Polering og bilrude", 250.43);
            p2 = new Package("Luksus Pakke", "Polering og bilrude", 25000);
            p3 = new Package("Basis Pakke", "Polering og bilrude", 4444);
            packages1 = new List<Package>();
            packages2 = new List<Package>();
            packages1.Add(p1);
            packages1.Add(p2);
            packages1.Add(p3);
            packages2.Add(p1);
            packages2.Add(p2);
            stringPackagesT = new List<string>();
            d1 = new Driver("Mathias");
            d2 = new Driver("Muslim");
            d3 = new Driver("Adam");
            v1 = new Vehicle("12ty432", "BMW", 1);
            c1 = new Customer("Frank", "Frankeee@eee.dk", "004523438452", 1, "");
            c2 = new Customer("Daniel", "muslim@eal.dk", "004522639513", v1, 2, "");
            c3 = new Customer("muslim", "muslim@avminali.dk", "004523674598", 3, "");
 
            put1 = new PickUpTruck("Autotransporter 1");
            b1 = new Booking(c1, "12.30", new DateTime(2019, 9, 24, 10, 00, 00), packages1, 1);
            b2 = new Booking(c1, "12.30", new DateTime(2019, 9, 24, 10, 00, 00), packages1, 2);
            b3 = new Booking(c2, "12.30", new DateTime(2019, 9, 24, 10, 00, 00), packages2, 3);
            b4 = new Booking(c3, "12.30", new DateTime(2019, 9, 24, 10, 00, 00), packages2, 4);
            bookingsT = new List<Booking>();
            bookingStringsT = new List<List<string>>();
            pickUpAgreementsT = new List<PickUpAgreement>();
            puaT = new PickUpAgreement(1, d1, put1, "Odense C", 5000, v1, 500, "Rødegårdsvej 273", new DateTime(2019, 9, 24, 10, 00, 00), "15.30");
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
            b1 = dbcT.Sp_CreateBooking(c1.CustomerName, "14:00", new DateTime(2019, 9, 24, 10, 00, 00), c1.Email, c1.Telephone, packages1, v1.LicensePlate, v1.Brand, "");
            Assert.IsTrue(b1.Id > 0);
        }

        [TestMethod]
        public void TestCreatePickUpDealRepo()
        {
            pudrT.ClearPickUpAgreements();
            pudrT.AddPickUpAgreementToList(puaT);
            Assert.IsTrue(0 < pudrT.GetPickUpAgreements().Count);
        }

        [TestMethod]
        public void TestCreatePickUpDealId()
        {
            puaT = dbcT.Sp_CreatePickUpAgreement("Mathias", "Autotransporter 1", 5000, "43ty545", "BMW", 500, "Rødegårdsvej 273", new DateTime(2019, 9, 24, 10, 00, 00), "10:30");
            Assert.IsTrue(puaT.PickUpAgreementId > 0);
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
        public void DeleteCompareRepoAndDB()
        {
            List<Booking> bookingDBList = dbcT.Sp_GetAllBookings();
            brT.UpdateAllBookings(bookingDBList);
            brT.DeleteBooking(1);
            foreach (Booking booking in bookingDBList)
            {
                if (booking.Id == 1)
                {
                    b4 = booking;
                }
            }
            dbcT.Sp_DeleteBooking(b4.Id, b4.Customer.Vehicle.VehicleID, b4.Customer.CustomerId);
            bookingDBList = dbcT.Sp_GetAllBookings();
            List<Booking> bookingRepoList = brT.GetBookings();
            Assert.AreEqual(bookingDBList.Count, bookingRepoList.Count);
        }

        [TestMethod]
        public void TestShowBookingToUI() 
        {
            
            foreach (Package package in packages1)
            {
                stringPackagesT.Add(package.Name);
            }
 
            bcT.CreateBooking("muslim", "08:00", new DateTime(2019, 06, 05, 00, 00, 00), "muslim@eal.dk", "+4511223344", stringPackagesT, "123xx66", "MERCEDES");
            bcT.CreateBooking("muslim", "10:00", new DateTime(2019, 06, 06, 00, 00, 00), "muslim@eal.dk", "+4511223344", stringPackagesT, "123xx66", "MERCEDES");
            bcT.CreateBooking("muslim", "12:00", new DateTime(2019, 06, 07, 00, 00, 00), "muslim@eal.dk", "+4511223344", stringPackagesT, "123xx66", "MERCEDES");
            bcT.CreateBooking("muslim", "14:00", new DateTime(2019, 06, 08, 00, 00, 00), "muslim@eal.dk", "+4511223344", stringPackagesT, "123xx66", "MERCEDES");
            bcT.CreateBooking("muslim", "16:00", new DateTime(2019, 06, 10, 00, 00, 00), "muslim@eal.dk", "+4511223344", stringPackagesT, "123xx66", "MERCEDES");
            bcT.CreateBooking("muslim", "18:00", new DateTime(2019, 06, 11, 00, 00, 00), "muslim@eal.dk", "+4511223344", stringPackagesT, "123xx66", "MERCEDES");
            bookingStringsT = bcT.GetWeeklyBookings(new DateTime(2019, 06, 05, 00, 00, 00));
            Assert.IsTrue(bookingStringsT.Count == 6);
        }

        [TestMethod]
        public void TestGetAllPickUpAgreementsFromDB() 
        {
            dbcT.Sp_CreatePickUpAgreement("Mathias", "Autotransporter 1", 5000, "123yt45", "BMW", 500, "Rødegårdsvej 100",  new DateTime(2019, 06, 11, 00, 00, 00), "12:00");
            dbcT.Sp_CreatePickUpAgreement("Adam", "Autotransporter 1", 5000, "123yt45", "BMW", 500, "Rødegårdsvej 100", new DateTime(2019, 06, 11, 00, 00, 00), "12:00");
            dbcT.Sp_CreatePickUpAgreement("Muslim", "Autotransporter 1", 5000, "123yt45", "BMW", 500, "Rødegårdsvej 100", new DateTime(2019, 06, 11, 00, 00, 00), "12:00");
            dbcT.Sp_CreatePickUpAgreement("Muslim", "Autotransporter 1", 5000, "123yt45", "BMW", 500, "Rødegårdsvej 100", new DateTime(2019, 06, 11, 00, 00, 00), "12:00");
            dbcT.Sp_CreatePickUpAgreement("Mathias", "Autotransporter 1", 5000, "123yt45", "BMW", 500, "Rødegårdsvej 100", new DateTime(2019, 06, 11, 00, 00, 00), "12:00");
            pickUpAgreementsT = dbcT.Sp_GetAllPickUpAgreements();
            Assert.IsTrue(bookingStringsT.Count == 5);
        }



        [TestMethod]
        public void TestFindBooking()
        {
            Booking b1 = new Booking(c1, "10.30", new DateTime(2019, 12, 03, 00, 00, 00), packages2, 2);
            brT.AddBookingToList(b1);
            Booking b2 = brT.GetBooking(2);
            Assert.AreEqual(b2.Id, b1.Id);
        }

        [TestMethod]
        public void TestUpdateBookingRepo()
        {
            Booking b1 = new Booking(c1, "10.30", new DateTime(2019, 12, 03, 00, 00, 00), packages2, 2);
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
            Booking b2 = new Booking(c2, "12:30", new DateTime(2019, 12, 03, 00, 00, 00), packages2, 1);
            dbcT.Sp_UpdateBooking(b2);
            bookingsT = dbcT.Sp_GetAllBookings();
            brT.UpdateAllBookings(bookingsT);

            Booking b3 = brT.GetBooking(2);


            Assert.AreEqual(b2.Customer.CustomerName, b3.Customer.CustomerName);
        }
    }
}
