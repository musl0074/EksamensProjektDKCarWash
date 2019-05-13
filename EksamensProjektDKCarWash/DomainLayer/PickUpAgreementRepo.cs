using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
   public class PickUpAgreementRepo
    {
        private static PickUpAgreementRepo instance;

        private List<PickUpAgreement> pickUpAgreements = new List<PickUpAgreement>();


        public static PickUpAgreementRepo GetInstance ()
        {
            if (instance == null)
                instance = new PickUpAgreementRepo();

            return instance;
        }

        public void AddPickUpAgreementToList(PickUpAgreement pua)
        {
            pickUpAgreements.Add(pua);
        }


        public List<PickUpAgreement> GetPickUpAgreements()
        {
            return pickUpAgreements;
        }


        public void ClearPickUpAgreements()
        {
            pickUpAgreements.Clear();
        }


    }
}
