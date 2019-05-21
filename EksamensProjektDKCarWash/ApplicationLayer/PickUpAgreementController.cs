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
        public PickUpAgreementRepo puar = PickUpAgreementRepo.GetInstance();
        public PickUpAgreement CurrentPickUpAgreement;

        public void CreatePickUpAgreement(string driverName, string pickUpTruckName, int postalCode, string streetName, string licensePlate, string brand, DateTime pickUpDate, string pickUpTime, double price)
        {

            PickUpAgreement pua = dbc.Sp_CreatePickUpAgreement(driverName, pickUpTruckName, postalCode, licensePlate, brand, price, streetName, pickUpDate, pickUpTime);
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

        public void DeletePickUpAgreement(int pickUpAgreementId)
        {
            dbc.Sp_DeletePickUpAgreement(pickUpAgreementId);
            puar.DeletePickUpAgreement(pickUpAgreementId);
        }

        public string GetPickUpAgreementWithId (int pickUpAgreementId)
        {
            CurrentPickUpAgreement = puar.GetSinglePickUpAgreement(pickUpAgreementId);
            return CurrentPickUpAgreement.ToString();
        }

        public string UpdatePickUpAgreement(int pickUpAgreementID, int vehicleID, string driverName, string pickUpTruckName, int postalCode, string licensePlate, 
            string brand, string streetName, DateTime pickUpDate, string pickUpTime, double price)
        {
            string city = dbc.Sp_GetCityByPostalCode(postalCode);
            PickUpTruck pickUpTruck = new PickUpTruck(pickUpTruckName);
            Driver driver = new Driver(driverName);
            Vehicle vehicle = new Vehicle(licensePlate, brand, vehicleID);
            CurrentPickUpAgreement.UpdatePickUpAgreement(driver, pickUpTruck, city, postalCode, vehicle, price, streetName, pickUpDate, pickUpTime);
            dbc.Sp_UpdatePickUpAgreement(CurrentPickUpAgreement);
            return puar.UpdatePickUpAgreement(CurrentPickUpAgreement);
        }

        public List<string> GetAllPickUpAgreements()
        {
            string pickUpAgreementString;
            List<string> stringPickUpAgreements = new List<string>();
            List<PickUpAgreement> pickUpAgreements = dbc.Sp_GetAllPickUpAgreements();
            foreach (PickUpAgreement pickUpAgreement in pickUpAgreements)
            {
                pickUpAgreementString = pickUpAgreement.ToString();
                stringPickUpAgreements.Add(pickUpAgreementString);
            }

            return stringPickUpAgreements;
        }

        public List<string> LoadAllDriversToString()
        {
            string stringDriver;
            List<string> stringDrivers = new List<string>();
            List<Driver> drivers = dbc.Sp_GetAllDrivers();
            foreach (Driver driver in drivers)
            {
                stringDriver = driver.Name;
                stringDrivers.Add(stringDriver);
            }
            return stringDrivers;
        }

        public List<string> LoadAllPickUpTrucksToString()
        {
            string stringPickUpTruck;
            List<string> stringPickUpTrucks = new List<string>();
            List<PickUpTruck> pickUpTrucks = dbc.Sp_GetAllPickUpTrucks();
            foreach (PickUpTruck pickUpTruck in pickUpTrucks)
            {
                stringPickUpTruck = pickUpTruck.PickUpTruckName;
                stringPickUpTrucks.Add(stringPickUpTruck);
            }
            return stringPickUpTrucks;
        }
    }
}
