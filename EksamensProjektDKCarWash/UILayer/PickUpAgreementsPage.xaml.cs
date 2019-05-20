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
    public partial class PickUpAgreementsPage : Page
    {
        PickUpAgreementController puac = new PickUpAgreementController();
       
        public PickUpAgreementsPage()
        {
            InitializeComponent();

            Thread pickUpAgreementsThread = new Thread(() => LoadAllPickUpAgreements());
            pickUpAgreementsThread.Start();
        
        }

        private void UpdatePickUpAgreement_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadAllPickUpAgreements()
        {
            List<string> stringPickUpAgreements = puac.GetAllPickUpAgreements();
            foreach (string pickupagreement in stringPickUpAgreements)
            {
                // Populate list
                string[] pickupagreementsarray = pickupagreement.Split(',');
                var currentRow = new
                {
                    LicensePlate = pickupagreementsarray[0],
                    Brand = pickupagreementsarray[1],
                    StreetName = pickupagreementsarray[2],
                    PostalCode = pickupagreementsarray[3],
                    City = pickupagreementsarray[4],
                    Driver = pickupagreementsarray[5],
                    PickUpDate = pickupagreementsarray[6],
                    PickUpTime = pickupagreementsarray[7],
                    Price = pickupagreementsarray[8]
                };
                Dispatcher.Invoke(() => this.pickUpAgreementsView.Items.Add(currentRow));
            }
        }

        private void DeletePickUpAgreement_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
