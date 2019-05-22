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
    public class DBConnector
    {
        PackageRepo pr = new PackageRepo();
        private static string connectionString = "Server=EALSQL1.eal.local; Database= B_DB26_2018; User Id=B_STUDENT26; Password=B_OPENDB26;";
        private static string connectionString2 = "Server=EALSQL1.eal.local; Database= B_DB26_2018; User Id=B_STUDENT26; Password=B_OPENDB26;";
        public Booking Sp_CreateBooking(string customerName, string startTime, DateTime bookingDate, string email, string telephone, List<Package> packages, string licensePlate, string brand, string vat = "")
        {

            Booking b = null;
            int id = 0;
            int vehicleId = 0;
            int customerId = 0;

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
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", licensePlate));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", brand));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = int.Parse(reader["BookingID"].ToString());
                            vehicleId = int.Parse(reader["VehicleID"].ToString());
                            customerId = int.Parse(reader["CustomerID"].ToString());
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
                Vehicle vehicle = new Vehicle(licensePlate, brand, vehicleId);
                Customer customer = new Customer(customerName, email, telephone, vehicle, customerId, vat);
                b = new Booking(customer, startTime, bookingDate, packages, id);
            }
            return b;
        }


        public PickUpAgreement Sp_CreatePickUpAgreement(string driverName, string pickUpTruckName, int postalCode, string licensePlate, string brand, double price, string address, DateTime pickUpDate, string pickUpTime)
        {
            PickUpAgreement pua = null;
            int pickUpId = 0;
            string city = "";
            int vehicleId = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_CreatePickUpAgreement", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@DriverName", driverName));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpTruckName", pickUpTruckName));
                    cmd1.Parameters.Add(new SqlParameter("@PostalCode", postalCode));
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", licensePlate));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", brand));
                    cmd1.Parameters.Add(new SqlParameter("@Price", price));
                    cmd1.Parameters.Add(new SqlParameter("@StreetName", address));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpDate", pickUpDate));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpTime", pickUpTime));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pickUpId = int.Parse(reader["PickUpAgreementID"].ToString());
                            city = reader["City"].ToString();
                            vehicleId = int.Parse(reader["VehicleID"].ToString());
                        }
                    }
                    Driver driver = new Driver(driverName);
                    PickUpTruck put = new PickUpTruck(pickUpTruckName);
                    Vehicle vehicle = new Vehicle(licensePlate, brand, vehicleId);
                    pua = new PickUpAgreement(pickUpId, driver, put, city, postalCode, vehicle, price, ad, pickUpDate, pickUpTime);
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }
            }
            return pua;
        }

     

        public void Sp_UpdatePickUpAgreement(PickUpAgreement currentPickUpAgreement)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_UpdatePickUpAgreement", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@PickUpAgreementID", currentPickUpAgreement.PickUpAgreementId));
                    cmd1.Parameters.Add(new SqlParameter("@VehicleID", currentPickUpAgreement.Vehicle.VehicleID));
                    cmd1.Parameters.Add(new SqlParameter("@DriverName", currentPickUpAgreement.Driver.Name));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpTruckName", currentPickUpAgreement.PickUpTruck.PickUpTruckName));
                    cmd1.Parameters.Add(new SqlParameter("@PostalCode", currentPickUpAgreement.PostalCode));
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", currentPickUpAgreement.Vehicle.LicensePlate));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", currentPickUpAgreement.Vehicle.Brand));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpDate", currentPickUpAgreement.PickUpDate));
                    cmd1.Parameters.Add(new SqlParameter("@PickUpTime", currentPickUpAgreement.PickUpTime));
                    cmd1.Parameters.Add(new SqlParameter("@Price", currentPickUpAgreement.Price));
                    cmd1.Parameters.Add(new SqlParameter("StreetName", currentPickUpAgreement.StreetName));
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);

                }

            }
        }


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
                    cmd1.Parameters.Add(new SqlParameter("@CustomerID", currentBooking.Customer.CustomerId));
                    cmd1.Parameters.Add(new SqlParameter("@VehicleID", currentBooking.Customer.Vehicle.VehicleID));
                    cmd1.Parameters.Add(new SqlParameter("@Brand", currentBooking.Customer.Vehicle.Brand));
                    cmd1.Parameters.Add(new SqlParameter("@LicensePlate", currentBooking.Customer.Vehicle.LicensePlate));
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

        public void Sp_DeletePickUpAgreement(int pickUpAgreementId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_DeletePickupAgreement", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@PickUpAgreementId", pickUpAgreementId));
                    cmd1.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
        }

        public string Sp_GetCityByPostalCode(int postalCode)
        {
            string city = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_FindCityByPostal", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add(new SqlParameter("@PostalCode", postalCode));

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            city = reader["City"].ToString();
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }
                return city;
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
                                                packageName = reader2["PackageName"].ToString();
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

                            Customer customer = new Customer(customerName);
                            b = new Booking(customer, startTime, bookingDate, packages);
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



        public List<PickUpAgreement> Sp_GetAllPickUpAgreements()
        {
            List<PickUpAgreement> pickUpAgreements = new List<PickUpAgreement>();
            PickUpAgreement pickUpAgreement;



            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_GetAllPickUpAgreements", con);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int pickUpAgreementId = int.Parse(reader["PickUpAgreementId"].ToString());
                            string driverName = reader["DriverName"].ToString();
                            string pickUpTruckName = reader["PickUpTruckName"].ToString();
                            string streetName = reader["StreetName"].ToString();
                            int postalCode = int.Parse(reader["PostalCode"].ToString());
                            string city = reader["City"].ToString();
                            string pickUpTime = reader["PickUpTime"].ToString();
                            DateTime pickUpDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("PickUpDate"));
                            string licensePlate = reader["LicensePlate"].ToString();
                            string brand = reader["Brand"].ToString();
                            double price = double.Parse(reader["Price"].ToString());
                            int vehicleId = int.Parse(reader["VehicleID"].ToString());

                            Vehicle vehicle = new Vehicle(licensePlate, brand, vehicleId);
                            Driver driver = new Driver(driverName);
                            PickUpTruck pickUpTruck = new PickUpTruck(pickUpTruckName);
                            pickUpAgreement = new PickUpAgreement(pickUpAgreementId, driver, pickUpTruck, city, postalCode, vehicle, price, streetName, pickUpDate, pickUpTime);
                            pickUpAgreements.Add(pickUpAgreement);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }
            }

            return pickUpAgreements;
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
                            int vehicleId = int.Parse(reader["VehicleID"].ToString());
                            int customerId = int.Parse(reader["CustomerID"].ToString());

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
                                                packageName = reader2["PackageName"].ToString();
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

                            Vehicle vehicle = new Vehicle(licensePlate, brand, vehicleId);
                            Customer customer = new Customer(customerName, email, telephone, vehicle, customerId, VAT);
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


        public List<Driver> Sp_GetAllDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            Driver d;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_GetAllDrivers", con);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string driverName = reader["DriverName"].ToString();
                            d = new Driver(driverName);
                            drivers.Add(d);
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
            return drivers;
        }

        public List<PickUpTruck> Sp_GetAllPickUpTrucks()
        {
            List<PickUpTruck> pickUpTrucks = new List<PickUpTruck>();
            PickUpTruck p;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("Sp_GetAllPickUpTrucks", con);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string pickUpTruckName = reader["PickUpTruckName"].ToString();
                            p = new PickUpTruck(pickUpTruckName);
                            pickUpTrucks.Add(p);
                        }
                    }
                }

                catch (SqlException e)
                {
                    Console.WriteLine("Ups" + e.Message);
                }

            }
            return pickUpTrucks;
        }
    }
}