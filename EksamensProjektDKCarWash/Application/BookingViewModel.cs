using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application
{
   public class BookingViewModel
    {
        private BookingRepo br = new BookingRepo();


        public void CreateBooking(string customerName,DateTime startTime,string email, string telephone,int package)
        {
            br.CreateBooking(customerName, startTime, email, telephone, package);
        }
    }
}
