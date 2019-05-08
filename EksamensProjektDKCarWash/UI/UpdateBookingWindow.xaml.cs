using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EksamensProjektDKCarWash
{
    /// <summary>
    /// Interaction logic for UpdateBookingWindow.xaml
    /// </summary>
    public partial class UpdateBookingWindow : Window
    {
        public UpdateBookingWindow(int bookingId)
        {
            InitializeComponent();

            LoadBooking(bookingId);
        }

        // Load all informations about the booking
        public void LoadBooking(int bookingId)
        {

        }




        // Button - SAVE ALL CHANGES
        private void Button_CreateBooking_Click(object sender, RoutedEventArgs e)
        {

        }

        // RadioButton - CHECKED
        private void RadioButton_Private_Checked(object sender, RoutedEventArgs e)
        {

        }

        // RadioButton - CHECKED
        private void RadioButton_Corporate_Checked(object sender, RoutedEventArgs e)
        {

        }

        // Calendar - SELECTED DATE CHANGED
        private void Calendar_Main_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
