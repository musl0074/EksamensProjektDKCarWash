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

namespace UILayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    delegate void ReturnToPickUpAgreementsPage(object sender, RoutedEventArgs e);
    public partial class MainWindow : Window
    {
        private WeeklyBookingsPage wbp;
        


        public MainWindow()
        {
            InitializeComponent();

            wbp = new WeeklyBookingsPage();
            FrameMain.Content = wbp;
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

        private void Window_Closed(object sender, EventArgs e)
        {
            // Close all open windows
            if (wbp.dailyBookingsPage.IsActive)
            {
                wbp.dailyBookingsPage.Close(); // This also aborts the dailyBookingsPage.updateThread due to Closed Event
            }

            wbp.updateThread.Abort();
        }

        public void Button_ShowPickUpAgreements_Click(object sender, RoutedEventArgs e)
        {
            if(wbp.updateThread.IsAlive == true)
            {
                wbp.updateThread.Abort();
            }
            FrameMain.Content = new PickUpAgreementsPage();
        }



        private void Button_CreatePickUpAgreement_Click(object sender, RoutedEventArgs e)
        {
            Window createPickUpAgreementWindow = new CreatePickUpAgreementWindow(() => {
                if (wbp.updateThread.IsAlive == true)
                {
                    wbp.updateThread.Abort();
                }
                FrameMain.Content = new PickUpAgreementsPage();
            });
            createPickUpAgreementWindow.WindowState = WindowState.Maximized;
            createPickUpAgreementWindow.Show();
        }
    }
}
