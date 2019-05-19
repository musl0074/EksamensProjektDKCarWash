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
    /// Interaction logic for PickUpAgreementsPage.xaml
    /// </summary>
    public partial class PickUpAgreementsPage : Page
    {
        PickUpAgreementController puac = new PickUpAgreementController();
        public PickUpAgreementsPage()
        {
            InitializeComponent();
            List<string> stringPickUpAgreements = puac.GetAllPickUpAgreements();
            foreach (string pickupagreement in stringPickUpAgreements)
            {
                // Populate list
                string[] pickupagreementsarray =  pickupagreement.Split(',');
                var currentRow = new
                                 {
                                     LicensePlate = pickupagreementsarray[0],
                                     Name2 = pickupagreementsarray[1],
                                     Name3 = pickupagreementsarray[2],
                                     Name4 = pickupagreementsarray[3],
                                     Name5 = pickupagreementsarray[4],
                                     Name6 = pickupagreementsarray[5],
                                     Name7 = pickupagreementsarray[6],
                                     Name8 = pickupagreementsarray[7],
                                     Name9 = pickupagreementsarray[8]
                                 };
                this.listView.Items.Add(currentRow);
            }
        }

        private void UpdatePickUpAgreement_Click(object sender, RoutedEventArgs e)
        {

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
