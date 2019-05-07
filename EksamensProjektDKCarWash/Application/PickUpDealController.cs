using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Application
{
    public class PickUpDealController
    {
        private DBController dbc = new DBController();
        private PickUpDealRepo pudr = new PickUpDealRepo();

        public void CreatePickUpDeal(string driverName, string pickUpTruckName, int postalCode, string streetName, string licensePlate, string brand, string model, string typeOfCar, DateTime pickUpDate, string pickUpTime, double price)
        {
            Driver driver = new Driver(driverName);
            PickUpTruck pickUpTruck = new PickUpTruck(pickUpTruckName);
            Vehicle vehicle = new Vehicle(licensePlate, brand, model, typeOfCar);

            PickUpDeal pud = dbc.Sp_CreatePickUpDeal(driver, pickUpTruck, postalCode, vehicle, price, streetName, pickUpDate, pickUpTime);
            try
            {
                pudr.AddPickUpDealToList(pud);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}
