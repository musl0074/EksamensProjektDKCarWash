using ApplicationLayer;
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
    /// Interaction logic for CreatePickUpAgreement.xaml
    /// </summary>
    public partial class CreatePickUpAgreementWindow : Window
    {
        PickUpAgreementController puac = new PickUpAgreementController();

        public CreatePickUpAgreementWindow()
        {
            InitializeComponent();
            UpdateUpToCurrentDate();
            LoadDrivers();
            LoadPickUpTrucks();

        }


        

        private void TextBox_Address_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_City_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_TimeStamps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TimeStamp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void UpdateUpToCurrentDate()
        {
            Calendar_Main.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }

        private void TextBox_LicensePlate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_Brand_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Calendar_Main_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreatePickUpAgreementButton_Click(object sender, RoutedEventArgs e)
        {
            string licensePlate = TextBox_LicensePlate.Text;
            string pickUpTime = TextBox_TimeStamp.Text;
            DateTime bookingDate = (DateTime)Calendar_Main.SelectedDate;
            string brand = TextBox_Brand.Text;
            string address = TextBox_Address.Text;
            string postalCode = TextBox_PostalCode.Text;
            string price = TextBox_Price.Text;
            string driver = (string)DriverComboBox.SelectedItem;
            string pickUpTruck = (string)PickUpTruckComboBox.SelectedItem;



            puac.CreatePickUpAgreement(driver, pickUpTruck, Convert.ToInt32(postalCode), address, licensePlate, brand, bookingDate, pickUpTime, Convert.ToDouble(price));
            this.Close();
        }

        public void LoadDrivers()
        {
            List<string> driversString = puac.LoadAllDriversToString();
            DriverComboBox.Items.Clear();  // Need to use dispatcher to make changes to UI

            foreach (string driver in driversString)
            {
                DriverComboBox.Items.Add(driver);
            }
        }

        public void LoadPickUpTrucks()
        {
            List<string> pickUpTrucksString = puac.LoadAllPickUpTrucksToString();
            PickUpTruckComboBox.Items.Clear();  // Need to use dispatcher to make changes to UI

            foreach (string pickUpTruck in pickUpTrucksString)
            {
                PickUpTruckComboBox.Items.Add(pickUpTruck);
            }
        }
    }
}
