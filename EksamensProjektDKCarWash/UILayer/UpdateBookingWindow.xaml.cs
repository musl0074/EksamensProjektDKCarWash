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
using ApplicationLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for UpdateBookingWindow.xaml
    /// </summary>
    public partial class UpdateBookingWindow : Window
    {
        private BookingController bc = new BookingController();
        private PackageController pc = new PackageController();
        int vechleId;
        int customerId;



        public UpdateBookingWindow(int bookingId)
        {
            InitializeComponent();
            

            LoadBooking(bookingId);
            UpdateUpToCurrentDate();
            CheckEnableCreateButton();
        }

        // Load all informations about the booking
        public void LoadBooking(int bookingId)
        {
            string specificBooking = bc.GetBooking(bookingId);
            string[] split = specificBooking.Split(';');
            string[] dateSplit = split[2].Split(':');
            string[] packagesSplit = split[7].Split(',');
            vechleId = int.Parse(split[11]);
            customerId = int.Parse(split[10]);
            TextBox_CustomerName.Text = split[1];
            TextBox_Email.Text = split[4];
            Textbox_Licensplate.Text = split[8];
            TextBox_Phonenumber.Text = split[5];

            TextBox_Vat.Text = split[6];
            if (TextBox_Vat.Text == string.Empty) { RadioButton_Private.IsChecked = true; }
            else { RadioButton_Corporate.IsChecked = true; }


            Textbox_Brand.Text = split[9];
            ComboBox_TimeStamps.SelectedItem = split[3];
            Calendar_Main.SelectedDate = new DateTime?(new DateTime(int.Parse(dateSplit[0]), int.Parse(dateSplit[1]), int.Parse(dateSplit[2])));

            foreach (var item in packagesSplit)
            {
                if (item != string.Empty) // Some splits gives us a "" string
                {
                    ListBox_Packages.Items.Add(item.Substring(1)); // Substring(1), because all packages have "\n" infront of them, we dont want that for this function
                }
            }
            

            LoadPackages();
        }


        // Button - Update the booking
        private void Button_UpdateBooking_Click(object sender, RoutedEventArgs e)
        {
            List<string> packagesString = new List<string>();

            foreach (string package in ListBox_Packages.Items)
            {
                packagesString.Add(package);
            }


            bc.UpdateBooking(customerId, vechleId, TextBox_CustomerName.Text, (DateTime)Calendar_Main.SelectedDate, (string)ComboBox_TimeStamps.SelectedItem, TextBox_Email.Text, Textbox_Licensplate.Text, Textbox_Brand.Text, TextBox_Phonenumber.Text, TextBox_Vat.Text, packagesString);

            this.Close();
        }



        // RadioButton
        private void RadioButton_Corporate_Checked(object sender, RoutedEventArgs e)
        {
            //Label_VAT.Visibility = Visibility.Visible;
            //TextBox_VAT.Visibility = Visibility.Visible;

            CheckEnableCreateButton();
        }

        private void RadioButton_Private_Checked(object sender, RoutedEventArgs e)
        {
            //Label_VAT.Visibility = Visibility.Collapsed;
            //TextBox_VAT.Visibility = Visibility.Collapsed;

            CheckEnableCreateButton();
        }

        // Calendar - ON SELECTED DATE
        private void Calendar_Main_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null); // Remove Mouse Focus from Calender, after selecting a date
            ComboBox_TimeStamps.Items.Clear();

            List<string> timestamps = bc.GetTimestamps((DateTime)Calendar_Main.SelectedDate);

            foreach (string timestamp in timestamps)
            {
                ComboBox_TimeStamps.Items.Add(timestamp);
            }

            ComboBox_TimeStamps.IsEnabled = true;
        }

        // Calendar - Removes all days prior to today
        public void UpdateUpToCurrentDate()
        {
            Calendar_Main.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }


        // LOAD PACKAGES
        public void LoadPackages()
        {
            List<string> packagesString = pc.LoadAllPackagesToString();
            ComboBox_Packages.Items.Clear();

            foreach (string package in packagesString)
            {
                ComboBox_Packages.Items.Add(package);
            }
        }

        // Button - Add package to ListBox
        private void Button_AddPackage_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Packages.SelectedItem != null)
            {
                string package = (string)ComboBox_Packages.SelectedItem;
                ListBox_Packages.Items.Add(package);

                CheckEnableCreateButton();
            }
        }

        // Button - Remove package from ListBox
        private void Button_RemovePackage_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Packages.SelectedItem != null)
                ListBox_Packages.Items.Remove(ListBox_Packages.SelectedItem);

            CheckEnableCreateButton();
        }



        






        // Check wether or not to enable the CREATE BUTTON
        public void CheckEnableCreateButton()
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
                    Button_UpdateBooking.IsEnabled = true;
                }
                else
                    Button_UpdateBooking.IsEnabled = false;
            }

            // Check for corporate
            if (RadioButton_Corporate.IsChecked == true)
            {
                if (TextBox_CustomerName.Text != string.Empty &&
                TextBox_Email.Text != string.Empty &&
                TextBox_Phonenumber.Text != string.Empty &&
                //TextBox_VAT.Text != string.Empty &&
                ComboBox_TimeStamps.SelectedItem != null &&
                ListBox_Packages.HasItems == true)
                {
                    Button_UpdateBooking.IsEnabled = true;
                }
                else
                    Button_UpdateBooking.IsEnabled = false;
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
