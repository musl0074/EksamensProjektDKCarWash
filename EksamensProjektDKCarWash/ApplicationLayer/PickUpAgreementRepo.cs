using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace ApplicationLayer
{
    public class PickUpAgreementRepo
    {
        private static PickUpAgreementRepo instance;

        private List<PickUpAgreement> pickUpAgreements = new List<PickUpAgreement>();

        private List<ISubscriber> subscribers = new List<ISubscriber>();


        public static PickUpAgreementRepo GetInstance()
        {
            if (instance == null)
                instance = new PickUpAgreementRepo();

            return instance;
        }

        public void AddPickUpAgreementToList(PickUpAgreement pua)
        {
            pickUpAgreements.Add(pua);
            NotifySubscribers();
        }


        public List<PickUpAgreement> GetPickUpAgreements()
        {
            return pickUpAgreements;
        }


        public void ClearPickUpAgreements()
        {
            pickUpAgreements.Clear();
        }

        public void DeletePickUpAgreement(int pickUpAgreementId)
        {
            foreach (PickUpAgreement pickUpAgreement in pickUpAgreements)
            {
                if (pickUpAgreement.PickUpAgreementId == pickUpAgreementId)
                {
                    pickUpAgreements.Remove(pickUpAgreement);
                }
            }
            NotifySubscribers();
        }

        public string UpdatePickUpAgreement(PickUpAgreement currentPickUpAgreement)
        {
            foreach (PickUpAgreement pickUpAgreement in pickUpAgreements)
            {
                if (currentPickUpAgreement.PickUpAgreementId == pickUpAgreement.PickUpAgreementId)
                {
                    pickUpAgreements.Remove(pickUpAgreement);
                    pickUpAgreements.Add(currentPickUpAgreement);
                    NotifySubscribers();
                    return "Afhentnings aftale er ændret!";                   
                }
            }
           
            return "En fejl er forekommet og afhentnings aftale er ikke ændret!";
        }

        public PickUpAgreement GetSinglePickUpAgreement(int pickUpAgreementId)
        {
            foreach (PickUpAgreement pickUpAgreement in pickUpAgreements)
            {
                if (pickUpAgreement.PickUpAgreementId == pickUpAgreementId)
                {
                    return pickUpAgreement;
                }
            }
            return null;
        }
        public void RegisterSubscriber(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }
        public void RemoveSubscriber(ISubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }
        public void NotifySubscribers()
        {
            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.Update();
            }
        }

    }
}
