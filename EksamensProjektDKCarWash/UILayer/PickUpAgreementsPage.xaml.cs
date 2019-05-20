using ApplicationLayer;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for PickUpAgreementsPage.xaml
    /// </summary>
    public partial class PickUpAgreementsPage : Page, ISubscriber
    {
        PickUpAgreementController puac = new PickUpAgreementController();
       
        public PickUpAgreementsPage()
        {
            InitializeComponent();
            puac.puar.RegisterSubscriber(this);
            Thread pickUpAgreementsThread = new Thread(() => LoadAllPickUpAgreements());
            pickUpAgreementsThread.Start();
        
        }

        private void UpdatePickUpAgreement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadAllPickUpAgreements()
        {
           Dispatcher.Invoke(() => pickUpAgreementsView.Items.Clear());
            List<string> stringPickUpAgreements = puac.GetAllPickUpAgreements();
            foreach (string pickupagreement in stringPickUpAgreements)
            {
                
                string[] pickupagreementsarray = pickupagreement.Split(',');
                var currentRow = new
                {
                    PickUpAgreementId = pickupagreementsarray[0],
                    LicensePlate = pickupagreementsarray[1],
                    Brand = pickupagreementsarray[2],
                    StreetName = pickupagreementsarray[3],
                    PostalCode = pickupagreementsarray[4],
                    City = pickupagreementsarray[5],
                    Driver = pickupagreementsarray[6],
                    PickUpDate = pickupagreementsarray[7],
                    PickUpTime = pickupagreementsarray[8],
                    Price = pickupagreementsarray[9]
                };
                Dispatcher.Invoke(() => this.pickUpAgreementsView.Items.Add(currentRow));
            }
        }

        private void DeletePickUpAgreement_Click(object sender, RoutedEventArgs e)
        {
            string data = pickUpAgreementsView.SelectedItem.ToString();
            string pickUpAgreementId = data.Split(',')[0].Split('=')[1];
            puac.DeletePickUpAgreement(int.Parse(pickUpAgreementId));
           
        }

        private void SearchPickUpAgreement_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hej_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Update()
        {
            LoadAllPickUpAgreements();
        }
    }
}
