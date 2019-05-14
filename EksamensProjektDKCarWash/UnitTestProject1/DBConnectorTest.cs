using DomainLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class DBConnectorTest
    {
        PackageRepo pr = new PackageRepo();
        private static string connectionString = "Server=EALSQL1.eal.local; Database= B_DB26_2018; User Id=B_STUDENT26; Password=B_OPENDB26;";
        private static string connectionString2 = "Server=EALSQL1.eal.local; Database= B_DB26_2018; User Id=B_STUDENT26; Password=B_OPENDB26;";
        public Booking Sp_CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, List<Package> packages, Vehicle vehicle, string vat = "")
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
                    cmd1.Parameters.Add(new SqlParameter("@VAT", vat));
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", vehicle.LicensePlate));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", vehicle.Brand));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["BookingID"].ToString());
                        }
                    }
                    reader.Close();
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }




                foreach (Package package in packages)
                {
                    try
                    {
                        SqlCommand cmd2 = new SqlCommand("Sp_AddPackagesToBooking", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.Add(new SqlParameter("@BookingId", id));
                        cmd2.Parameters.Add(new SqlParameter("@PackageName", package.Name));


                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                        //if (reader.HasRows)
                        //{
                        //    while (reader.Read())
                        //    {
                        //        id = int.Parse(reader["BookingId"].ToString());
                        //    }
                        //}


                    }

                    catch (SqlException e)
                    {
                        Console.WriteLine("Ups" + e.Message);
                    }
                }
                Customer customer = new Customer(customerName, email, telephone, vat);
                b = new Booking(customer, startTime, bookingDate, packages, id);
            }
            return b;
        }


        public PickUpAgreement Sp_CreatePickUpAgreement(Driver driver, PickUpTruck pickUpTruck, int postalCode, Vehicle vehicle, double price, string streetName, DateTime pickUpDate, string pickUpTime)
        {
            PickUpAgreement pua = null;
            int pickUpId = 0;
            string city = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_CreatePickUpAgreement", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@DriverName", driver.Name));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpTruckName", pickUpTruck.PickUpTruckName));
                    cmd1.Parameters.Add(new SqlParameter("@PostalCode", postalCode));
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", vehicle.LicensePlate));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", vehicle.Brand));
                    cmd1.Parameters.Add(new SqlParameter("@Price", price));
                    cmd1.Parameters.Add(new SqlParameter("@StreetName", streetName));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpDate", pickUpDate));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpTime", pickUpTime));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pickUpId = int.Parse(reader["PickUpID"].ToString());
                            city = reader["City"].ToString();
                        }
                    }
                    pua = new PickUpAgreement(pickUpId, driver, pickUpTruck, city, postalCode, vehicle, price, streetName, pickUpDate, pickUpTime);
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }
            }
            return pua;
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
                    cmd1.Parameters.Add(new SqlParameter("@StartTime", currentBooking.StartTime));
                    cmd1.Parameters.Add(new SqlParameter("@CustomerName", currentBooking.Customer.CustomerName));
                    cmd1.Parameters.Add(new SqlParameter("@BookingDate", currentBooking.BookingDate));
                    cmd1.Parameters.Add(new SqlParameter("@Email", currentBooking.Customer.Email));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", currentBooking.Customer.Vehicle.Brand));
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", currentBooking.Customer.Vehicle.LicensePlate));
                    cmd1.Parameters.Add(new SqlParameter("@PhoneNumber", currentBooking.Customer.Telephone));
                    cmd1.Parameters.Add(new SqlParameter("@VAT", currentBooking.Customer.Vat));

                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);

                }

                try
                {
                    SqlCommand cmd1 = new SqlCommand("Sp_DeletePackagesConnectedToBookingID", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingID", currentBooking.Id));

                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);

                }
                foreach (Package package in currentBooking.Packages)
                {
                    try
                    {
                        SqlCommand cmd1 = new SqlCommand("Sp_AddPackagesToBooking", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@BookingID", currentBooking.Id));
                        cmd1.Parameters.Add(new SqlParameter("@PackageName", package.Name));

                        cmd1.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Ups" + e.Message);

                    }
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
                    cmd1.Parameters.Add(new SqlParameter("@BookingId", bookingId));
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
        }

        public void Sp_DeletePackagesFromBooking(int bookingId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_DeletePackagesWithID", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@BookingId", bookingId));
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
        }

        public List<Booking> Sp_ShowBooking(DateTime bookingDate)
        {
            List<Booking> bookings = new List<Booking>();
            List<Package> packages = new List<Package>();
            List<Package> packagesFromDB = new List<Package>();
            Booking b = null;
            string customerName = "";
            string startTime = "";
            string packageName = "";
            string bookingId = "";
            pr.AddPackageFromDBToList(Sp_GetAllPackages());




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
                            bookingId = reader["BookingID"].ToString();
                            using (SqlConnection con2 = new SqlConnection(connectionString2))
                            {
                                try
                                {
                                    con2.Open();
                                    SqlCommand cmd2 = new SqlCommand("Sp_FindAllPackagesForBooking", con2);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.Add(new SqlParameter("@BookingID", bookingId));

                                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.HasRows)
                                        {
                                            while (reader2.Read())
                                            {
                                                packageName = reader2["packageName"].ToString();
                                                packages.Add(pr.FindPackage(packageName));
                                            }
                                        }
                                    }

                                }

                                catch (SqlException e)
                                {
                                    Console.WriteLine("Ups" + e.Message);
                                }

                            }

                        }
                        Customer customer = new Customer(customerName);
                        b = new Booking(customer, startTime, bookingDate, packages);
                        bookings.Add(b);
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
                    Console.WriteLine("Ups" + e.Message);
                }
                return result;


            }
        }



        public List<Booking> Sp_GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            List<Package> packages = new List<Package>();
            Booking b;
            pr.AddPackageFromDBToList(Sp_GetAllPackages());
            string packageName = "";

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
                            string startTime = reader["StartTime"].ToString();
                            string customerName = reader["CustomerName"].ToString();
                            DateTime bookingDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("BookingDate"));
                            string email = reader["Email"].ToString();
                            string telephone = reader["PhoneNumber"].ToString();
                            string VAT = reader["VAT"].ToString();
                            string licensePlate = reader["LicensePlate"].ToString();
                            string brand = reader["Brand"].ToString();
                            string model = reader["Model"].ToString();
                            string typeOfCar = reader["typeOfCar"].ToString();

                            using (SqlConnection con2 = new SqlConnection(connectionString2))
                            {
                                try
                                {
                                    con2.Open();
                                    SqlCommand cmd2 = new SqlCommand("Sp_FindAllPackagesForBooking", con2);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.Add(new SqlParameter("@BookingID", bookingId));
                                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.HasRows)
                                        {
                                            while (reader2.Read())
                                            {
                                                packageName = reader2["packageName"].ToString();
                                                packages.Add(pr.FindPackage(packageName));
    
                                            }
                                        }
                                    }

                                }

                                catch (SqlException e)
                                {
                                    Console.WriteLine("Ups" + e.Message);
                                }
                            }

                            Vehicle vehicle = new Vehicle(licensePlate, brand);
                            Customer customer = new Customer(customerName, email, telephone, vehicle, VAT);
                            b = new Booking(customer, startTime, bookingDate, packages, bookingId);
                            bookings.Add(b);
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
            packages.Clear();
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