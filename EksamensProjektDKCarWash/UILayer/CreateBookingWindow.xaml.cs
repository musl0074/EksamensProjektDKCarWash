﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for CreateBookingWindow.xaml
    /// </summary>
    public partial class CreateBookingWindow : Window
    {
        private BookingController bc = new BookingController();
        private PackageController pc = new PackageController();

        public CreateBookingWindow()
        {
            InitializeComponent();

            UpdateUpToCurrentDate(); // Add BlackOutDates to all days prior to "today", makes sure that the user cant accidently create a booking with a faulty date.
            
            LoadPackages(); // Loads packages from the repository into the ComboBox. Makes sure that we only have to add new packages in the database.
        }

        // RadioButton - Controls whether or not "VAT" can be set to anything other than string.Empty
        private void RadioButton_Corporate_Checked(object sender, RoutedEventArgs e)
        {
            TextBox_VAT.IsEnabled = true;

            CheckEnableCreateButton();
        }

        private void RadioButton_Private_Checked(object sender, RoutedEventArgs e)
        {
            TextBox_VAT.IsEnabled = false;

            CheckEnableCreateButton();
        }



        // Calendar - ON SELECTED DATE
        private void Calendar_Main_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null); // Remove Mouse Focus from Calender, after selecting a date so the user doesn't have to double click elsewhere
            ComboBox_TimeStamps.Items.Clear(); // Clear all current timestamps in the combobox

            List<string> timestamps = bc.GetTimestamps((DateTime)Calendar_Main.SelectedDate); // Retrieves all available timestamps for the given date

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
        public void LoadPackages ()
        {
            List<string> packagesString = pc.LoadAllPackagesToString(); // Get all packages in string format
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
                string package = (string)ComboBox_Packages.SelectedItem;  // Add the selected package in string format 
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

        // Button - Create booking
        private void Button_CreateBooking_Click(object sender, RoutedEventArgs e)
        {
            string customerName = TextBox_CustomerName.Text;
            string startTime = (string)ComboBox_TimeStamps.SelectedItem;
            DateTime bookingDate = (DateTime)Calendar_Main.SelectedDate;
            string email = TextBox_Email.Text;
            string telephoneNumber = TextBox_Phonenumber.Text;
            string vat = TextBox_VAT.Text;
            List<string> packages = new List<string>();

            foreach (string package in ListBox_Packages.Items)
            {
                packages.Add(package);
            }

            string licensePlate = TextBox_LicensePlate.Text;
            string brand = TextBox_Brand.Text;



            bc.CreateBooking(customerName, startTime, bookingDate, email, telephoneNumber, packages, licensePlate, brand, vat);
            this.Close();
            
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
                ListBox_Packages.HasItems == true &&
                TextBox_LicensePlate.Text != string.Empty &&

                TextBox_Brand.Text != string.Empty) 
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
                ListBox_Packages.HasItems == true &&
                TextBox_LicensePlate.Text != string.Empty &&
                TextBox_Brand.Text != string.Empty) ;


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

  

        private void TextBox_LicensePlate_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }


        private void TextBox_Brand_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableCreateButton();
        }
 
    }
}
