using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PickUpTruck
    {
        string PickUpTruckName { get; set; }

        public PickUpTruck(string pickUpTruckName)
        {
            PickUpTruckName = pickUpTruckName;
        }
    }
}
