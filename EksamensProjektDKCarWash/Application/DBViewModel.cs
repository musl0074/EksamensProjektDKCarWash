using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Domain;

namespace Application
{
   public class DBViewModel
    {
        private string connectionstring;
        public Booking Sp_CreateBooking(string customerName, DateTime startTime, string email, string telephone, int package)
        {
          return  Sp_CreateBookingStump(customerName, startTime, email, telephone, package);  
        }
        public Booking Sp_CreateBookingStump(string customerName, DateTime startTime, string email, string telephone, int package)
        {
            try
            {
                return new Booking(customerName, startTime, email, telephone, package,3);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message); 
                throw;
            } 
        }
    }
}
