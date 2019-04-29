﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Domain;

namespace Application
{
   public class DBViewModel
    {
        private static string connectionString = "Server=EALSQL1.eal.local; Database= B_DB26_2018; User Id=B_STUDENT26; Password=B_OPENDB26;";
        public Booking CreatePrivateBooking(string customerName, DateTime startTime, DateTime bookingDate, string email, string telephone, Package package, string vat = "")
        {

            Booking b  = null;
            int id = 0;
            string dateTime = startTime.ToString();
            string bookingDateTime = bookingDate.ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("spCreatePrivateBooking", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@CustomerName", customerName));
                    cmd1.Parameters.Add(new SqlParameter("@StartTime", dateTime));
                    cmd1.Parameters.Add(new SqlParameter("@BookingDate", bookingDate));
                    cmd1.Parameters.Add(new SqlParameter("@Email", email));
                    cmd1.Parameters.Add(new SqlParameter("@PhoneNumber", telephone));
                    cmd1.Parameters.Add(new SqlParameter("@PackageName", package.Name));
                    cmd1.Parameters.Add(new SqlParameter("@VAT", vat));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["BookingID"].ToString());
                        }
                    }                 
                    b = new Booking(customerName, startTime, bookingDate, email, telephone, package, id, vat);
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }               
            }
            return b;
        }
        //public Booking Sp_CreateBookingStump(string customerName, DateTime startTime, string email, string telephone, int package)
        //{
        //    try
        //    {
        //        return new Booking(customerName, startTime, email, telephone, package,3);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message); 
        //        throw;
        //    } 
        //}
    }
}
