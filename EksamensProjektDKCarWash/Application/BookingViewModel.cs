using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Domain;

namespace Application
{
   public class BookingViewModel
    {
        private BookingRepo br = new BookingRepo();
        private DBViewModel dbvm = new DBViewModel();



        public void CreateBooking(string customerName,DateTime startTime,string email, string telephone,int package)
        {
         Booking b =  dbvm.Sp_CreateBooking(customerName, startTime, email, telephone, package);
            try
            {
                br.CreateBooking(b);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            
        }
    }
}
