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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EksamensProjektDKCarWash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FrameMain.Content = new WeeklyBookingsPage();
        }


        // Button - CREATE BOOKING
        private void Button_CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            Window createBookingWindow = new CreateBookingWindow();
            createBookingWindow.WindowState = WindowState.Maximized;
            createBookingWindow.Show();
        }

        // Button - SHOW WEEKLY BOOKINGS
        private void Button_ShowWeeklyBookings_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Content = new WeeklyBookingsPage();
        }
    }
}
