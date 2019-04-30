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
        private DBViewModel dbvm = new DBViewModel();
        private BookingRepo br = new BookingRepo();


        public void DeleteBooking(int bookingId)
        {
            br.DeleteBooking(bookingId);
            try
            {
                dbvm.Sp_DeleteBooking(bookingId);
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        
    }
}
