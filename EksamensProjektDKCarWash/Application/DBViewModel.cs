using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application
{
   public class DBViewModel
    {
        private string connectionstring;
        public Booking Sp_CreateBooking(string customerName, DateTime startTime, string email, string telephone, int package)
        {
            throw new NotImplementedException();
        }
    }
}
