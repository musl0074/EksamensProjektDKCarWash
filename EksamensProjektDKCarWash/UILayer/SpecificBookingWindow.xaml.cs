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
using ApplicationLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class SpecificBookingWindow : Window
    {
        
        BookingController bc = new BookingController();
        string CurrentBooking;
        public SpecificBookingWindow(string bookingId)
        {
            InitializeComponent();
            CurrentBooking = LoadBooking(bookingId);



        }

        public string LoadBooking(string bookingId)
        {
            string specificBooking = bc.GetBooking(int.Parse(bookingId));
            string[] split = specificBooking.Split(';');
            string customerName = split[1];
            string email = split[4];
            string telephone = split[5];
            string vat = split[6];

            TextBox_CustomerName.Text = customerName;
            TextBox_Email.Text = email;
            TextBox_Phonenumber.Text = telephone;
            TextBox_VAT.Text = vat;

            return specificBooking;

        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            string[] split = CurrentBooking.Split(';');
            Window UpdateWindow = new UpdateBookingWindow(int.Parse(split[0]));
            UpdateWindow.WindowState = WindowState.Maximized;
            UpdateWindow.Show();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
