using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationLayer
{
    public class PickUpAgreementController
    {
        private DBConnector dbc = new DBConnector();
        private PickUpAgreementRepo puar = PickUpAgreementRepo.GetInstance();

        public void CreatePickUpAgreement(string driverName, string pickUpTruckName, int postalCode, string streetName, string licensePlate, string brand, string model, string typeOfCar, DateTime pickUpDate, string pickUpTime, double price)
        {
            Driver driver = new Driver(driverName);
            PickUpTruck pickUpTruck = new PickUpTruck(pickUpTruckName);
            Vehicle vehicle = new Vehicle(licensePlate, brand, model, typeOfCar);

            PickUpAgreement pua = dbc.Sp_CreatePickUpAgreement(driver, pickUpTruck, postalCode, vehicle, price, streetName, pickUpDate, pickUpTime);
            try
            {
                puar.AddPickUpAgreementToList(pua);
            }
            catch (NullReferenceException e)
            {
                //MessageBox.Show(e.Message);
                throw;
            }
        }
    }
}
