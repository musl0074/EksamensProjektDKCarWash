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
    /// Interaction logic for CreateBookingWindow.xaml
    /// </summary>
    public partial class CreateBookingWindow : Window
    {
        public CreateBookingWindow()
        {
            InitializeComponent();

            UpdateUpToCurrentDate();
        }

        // RadioButton
        private void RadioButton_Corporate_Checked(object sender, RoutedEventArgs e)
        {
            Label_VAT.Visibility = Visibility.Visible;
            TextBox_VAT.Visibility = Visibility.Visible;
        }

        private void RadioButton_Private_Checked(object sender, RoutedEventArgs e)
        {
            Label_VAT.Visibility = Visibility.Collapsed;
            TextBox_VAT.Visibility = Visibility.Collapsed;
        }



        // Calendar - ON SELECTED DATE
        private void Calendar_Main_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null); // Remove Mouse Focus from Calender, after selecting a date


        }

        // Calendar - Removes all days prior to today
        public void UpdateUpToCurrentDate()
        {
            Calendar_Main.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }


        // Check wether or not to enable the CREATE BUTTON
        public void CheckEnableCreateButton ()
        {
            // Check for private 
            if (RadioButton_Private.IsChecked == true)
            {
                if (TextBox_CustomerName.Text != string.Empty &&
                TextBox_Email.Text != string.Empty &&
                TextBox_Phonenumber.Text != string.Empty &&
                ComboBox_TimeStamps.SelectedItem != null &&
                ListBox_Packages.HasItems == true)
                {
                    Button_CreateBooking.IsEnabled = true;
                }
                else
                    Button_CreateBooking.IsEnabled = false;
            }

            // Check for corporate
            if (RadioButton_Corporate.IsChecked == true)
            {
                if (TextBox_CustomerName.Text != string.Empty &&
                TextBox_Email.Text != string.Empty &&
                TextBox_Phonenumber.Text != string.Empty &&
                TextBox_VAT.Text != string.Empty &&
                ComboBox_TimeStamps.SelectedItem != null &&
                ListBox_Packages.HasItems == true)
                {
                    Button_CreateBooking.IsEnabled = true;
                }
                else
                    Button_CreateBooking.IsEnabled = false;
            }
        }

        private void TextBox_CustomerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }

        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }

        private void TextBox_Phonenumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }

        private void TextBox_VAT_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }

        private void ComboBox_TimeStamps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }

        private void ListBox_Packages_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }
    }
}
