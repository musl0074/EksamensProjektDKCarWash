using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class PickUpDealRepo
    {
        private List<PickUpDeal> pickUpDeals = new List<PickUpDeal>();

        public void AddPickUpDealToList(PickUpDeal pud)
        {
            pickUpDeals.Add(pud);
        }


        public List<PickUpDeal> GetPickUpDeals()
        {
            return pickUpDeals;
        }


        public void ClearPickUpDeals()
        {
            pickUpDeals.Clear();
        }


    }
}
