using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DomainLayer;

namespace ApplicationLayer
{
    public class DBConnector : IConnector
    {
        PackageRepo pr = new PackageRepo();
        private static string connectionString = "Server=EALSQL1.eal.local; Database= B_DB26_2018; User Id=B_STUDENT26; Password=B_OPENDB26;";
        public Booking Sp_CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, string packageName, string vat = "")
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
                    cmd1.Parameters.Add(new SqlParameter("@PackageName", packageName));
                    cmd1.Parameters.Add(new SqlParameter("@VAT", vat));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["BookingID"].ToString());
                        }
                    }
                    Package package = pr.FindPackage(packageName);
                    Customer customer = new Customer(customerName, email, telephone, vat);
                    b = new Booking(customer, startTime, bookingDate, package, id);
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

        public void Sp_UpdateBooking(Booking currentBooking)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_UpdateBooking", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingID", currentBooking.Id));
                    cmd1.Parameters.Add(new SqlParameter("@PackageName", currentBooking.Package.Name));
                    cmd1.Parameters.Add(new SqlParameter("@StartTime", currentBooking.StartTime));
                    cmd1.Parameters.Add(new SqlParameter("@CustomerName", currentBooking.Customer.CustomerName));
                    cmd1.Parameters.Add(new SqlParameter("@BookingDate", currentBooking.BookingDate));
                    cmd1.Parameters.Add(new SqlParameter("@Email", currentBooking.Customer.Email));
                    cmd1.Parameters.Add(new SqlParameter("@PhoneNumber", currentBooking.Customer.Telephone));
                    cmd1.Parameters.Add(new SqlParameter("@VAT", currentBooking.Customer.Vat));

                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    //MessageBox.Show(e.Message);
                    
                }

            }
        }

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
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    //MessageBox.Show(e.Message);                    
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
                            Customer customer = new Customer(customerName);
                            Package package = pr.FindPackage(packageName);
                            b = new Booking(customer, startTime, bookingDate, package);
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

        public int Sp_FindSingleBookingWithID(int bookingId)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_GetBookingWithId", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingID", bookingId));

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
                    //MessageBox.Show(e.Message);
                    throw;
                }
                return result;


            }
        }

        public PickUpAgreement Sp_CreatePickUpAgreement(Driver driver, PickUpTruck pickUpTruck, int postalCode, Vehicle vehicle, double price, string streetName, DateTime pickUpDate, string pickUpTime)
        {
            PickUpAgreement pud = new PickUpAgreement(0, driver, pickUpTruck, "odense", postalCode, vehicle, price, streetName, pickUpDate, pickUpTime);
            return pud;
        }

        public List<Booking> Sp_GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            Booking b;


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_GetAllBookings", con);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int bookingId = int.Parse(reader["BookingId"].ToString());
                            string packageName = reader["PackageName"].ToString();
                            string startTime = reader["StartTime"].ToString();
                            string customerName = reader["CustomerName"].ToString();
                            DateTime bookingDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("BookingDate"));
                            string email = reader["Email"].ToString();
                            string telephone = reader["PhoneNumber"].ToString();
                            string VAT = reader["VAT"].ToString();
                            Package package = pr.FindPackage(packageName);
                            Customer customer = new Customer(customerName, email, telephone, VAT);
                            b = new Booking(customer, startTime, bookingDate, package, bookingId);
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

        public List<Package> Sp_GetAllPackages()
        {
            List<Package> packages = new List<Package>();
            Package p;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_GetAllPackages", con);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string packageName = reader["PackageName"].ToString();
                            string packageDesc = reader["PackageDesc"].ToString();
                            double packagePrice = double.Parse(reader["PackagePrice"].ToString());
                            p = new Package(packageName, packageDesc, packagePrice);
                            packages.Add(p);
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
            return packages;
        }
    }
}
