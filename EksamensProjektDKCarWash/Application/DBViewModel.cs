using System;
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
        public Booking Sp_CreatePrivateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, Package package, string vat = "")
        {

            Booking b = null;
            int id = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_CreatePrivateBooking", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@CustomerName", customerName));
                    cmd1.Parameters.Add(new SqlParameter("@StartTime", startTime));
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

        public void Sp_DeleteBooking(int bookingId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_DeleteBooking", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingID", bookingId));
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message);
                    throw;
                }

            }
        }

        public List<Booking> Sp_ShowBooking(DateTime bookingDate)
        {
            List<Booking> bookings = new List<Booking>();
            Booking b = null;
            string customerName = "";
            string startTime = "";
            string packageName = "";


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_ShowDailyBookings", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingDate", bookingDate));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            customerName = (reader["CustomerName"].ToString());
                            startTime = reader["StartTime"].ToString();
                            packageName = reader["PackageName"].ToString();
                            b = new Booking(customerName, startTime, bookingDate, packageName);
                            bookings.Add(b);
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
            return bookings;
        }

        public int Sp_FindsingleBookingWithID(int v)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_FindSingleBookingWithID", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingID", v));

                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = int.Parse(reader["BookingID"].ToString());

                        }
                    }

                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message);
                    throw;
                }
                return result;


            }
        }
    }
}
