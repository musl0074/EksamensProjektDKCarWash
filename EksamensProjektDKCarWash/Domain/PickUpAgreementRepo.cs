using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class PickUpAgreementRepo
    {
        private List<PickUpAgreement> pickUpAgreements = new List<PickUpAgreement>();

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
