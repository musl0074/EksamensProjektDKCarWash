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
        CreateBookingViewModel cbvmT = new CreateBookingViewModel();
        BookingViewModel bvmT = new BookingViewModel();
        BookingRepo brT = new BookingRepo();
        Booking b1;
        Package p1 = new Package("Premium Pakke", "Polering og bilrude", 250.43);
        
        [TestMethod]
        public void TestBookingId()
        {
            b1 = dbvmT.Sp_CreatePrivateBooking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 22, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
            Assert.IsTrue(0<b1.Id);
        }

        [TestMethod]
        public void TestAtToBookingRepo()
        {
            List<Booking> bookingsT;
            b1 = dbvmT.Sp_CreatePrivateBooking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 22, 00, 00, 00), "frank@eal.dk", "+4511223344", p1);
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
            b1 = new Booking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 2, "");
           Booking b2 = new Booking ("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 4, "");
           Booking b3 = new Booking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 3, "");
           Booking b4 = new Booking("Frank", new DateTime(2019, 9, 22, 10, 00, 00), new DateTime(2019, 9, 24, 10, 00, 00), "frank@eal.dk", "+4511223344", p1, 5, "");
            // brT.CreateBooking(b1);
            List<Booking> bookingsT = brT.GetBookings();
            bookingsT.Add(b1);
            bookingsT.Add(b2);
            bookingsT.Add(b3);
            bookingsT.Add(b4);
            brT.DeleteBooking(b1.Id);


            Assert.AreEqual(bookingsT.Count, 3);
            
          //  bvmT.DeleteBooking(b1.Id);

            //Booking b2 = null;
            //foreach (var item in bookingsT)
            //{
            //    if (item.Id == b1.Id)
            //        b2 = item;
            //}


           // Assert.IsTrue(b2 == null); // Hvis b2 aldrig bliver sat, så er testen korrekt gennemført, da den ikke kunne finde den nylige oprettede booking
        }

    }
}
