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
        public PickUpAgreement CurrentPickUpAgreement;

        public void CreatePickUpAgreement(string driverName, string pickUpTruckName, int postalCode, string streetName, string licensePlate, string brand, string model, string typeOfCar, DateTime pickUpDate, string pickUpTime, double price)
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
            puar.DeletePickUpAgreement(pickUpAgreementId);
            try
            {
                dbc.Sp_DeletePickUpAgreement(pickUpAgreementId);
            }
            catch (NullReferenceException e)
            {
                //MessageBox.Show(e.Message);
                throw;
            }
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
            List<string> stringPickUpAgreements = null;
            List<PickUpAgreement> pickUpAgreements = dbc.Sp_GetAllPickUpAgreements();
            foreach (PickUpAgreement pickUpAgreement in pickUpAgreements)
            {
                pickUpAgreementString = pickUpAgreement.ToString();
                stringPickUpAgreements.Add(pickUpAgreementString);
            }

            return stringPickUpAgreements;
        }
    }
}
