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
using System.Windows.Shapes;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for UpdatePickUpAgreement.xaml
    /// </summary>
    public partial class UpdatePickUpAgreement : Window
    {
        PickUpAgreementController puac = new PickUpAgreementController();
        string data;
        int pickUpAgreementId;
        int vehicleId;

        public UpdatePickUpAgreement(string newData)
        {
            InitializeComponent();
            data = newData;
            UpdateUpToCurrentDate();
            LoadDrivers();
            LoadPickUpTrucks();
            InsertPickUpAgreement();

        }

        private void Calendar_Main_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
            CheckEnableUpdateButton();
        }

        private void TextBox_LicensePlate_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }

        private void TextBox_Brand_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }

        private void TextBox_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }

        private void TextBox_City_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }

        private void TextBox_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }

        private void TextBox_TimeStamp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }

        private void UpdatePickUpAgreementButton_Click(object sender, RoutedEventArgs e)
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



            puac.UpdatePickUpAgreement(pickUpAgreementId, vehicleId, driver, pickUpTruck, Convert.ToInt32(postalCode), licensePlate, brand, address, bookingDate, pickUpTime, Convert.ToDouble(price));
            this.Close();

        }

        private void InsertPickUpAgreement()
        {
            List<string> pickUpAgreementData = new List<string>();
            string[] dataSplit = new string[10];
            string newData = data.Split('}')[0];
            dataSplit = newData.Split(',');
            pickUpAgreementId = int.Parse(data.Split(',')[0].Split('=')[1]);
            for (int i = 1; i < dataSplit.Length; i++)
            {
                pickUpAgreementData.Add(dataSplit[i].Split('=')[1].Trim());
            }
            string[] InsertPickUpData = pickUpAgreementData.ToArray();

            TextBox_LicensePlate.Text = InsertPickUpData[0];
            TextBox_Brand.Text = InsertPickUpData[1];
            TextBox_Address.Text = InsertPickUpData[2];
            TextBox_PostalCode.Text = InsertPickUpData[3];
            TextBox_City.Text = InsertPickUpData[4];
            DriverComboBox.SelectedItem = InsertPickUpData[5];
            string[] dateSplit = InsertPickUpData[6].Split('/');
            string[] dateSplit2 = dateSplit[2].Split(' ');
            Calendar_Main.SelectedDate = new DateTime?(new DateTime(int.Parse(dateSplit2[0]), int.Parse(dateSplit[0]), int.Parse(dateSplit[1])));
            TextBox_TimeStamp.Text = InsertPickUpData[7];
            TextBox_Price.Text = InsertPickUpData[8];
            PickUpTruckComboBox.Text = "Autotransporter 1";
            vehicleId = int.Parse(InsertPickUpData[9]);
            CheckEnableUpdateButton();


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

        public void UpdateUpToCurrentDate()
        {
            Calendar_Main.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }
        public void CheckEnableUpdateButton()
        {

            if (TextBox_LicensePlate.Text != string.Empty &&
            TextBox_Brand.Text != string.Empty &&
            TextBox_PostalCode.Text != string.Empty &&
            TextBox_TimeStamp.Text != string.Empty &&
            TextBox_City.Text != string.Empty &&
            TextBox_Price.Text != string.Empty &&
            DriverComboBox.SelectedItem != null &&
            PickUpTruckComboBox.SelectedItem != null)
            {
                UpdatePickUpAgreementButton.IsEnabled = true;
            }
            else
            {
                UpdatePickUpAgreementButton.IsEnabled = false;
            }
        }

        private void TextBox_Address_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnableUpdateButton();
        }
    }
}
