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

namespace EksamensProjektDKCarWash
{
    /// <summary>
    /// Interaction logic for CreateBookingPage.xaml
    /// </summary>
    public partial class CreateBookingPage : Page
    {
        public CreateBookingPage()
        {
            InitializeComponent();
        }

        private void RadioButton_Corporate_Checked(object sender, RoutedEventArgs e)
        {
            Label_VAT.Visibility = Visibility.Visible;
            TextBox_Vat.Visibility = Visibility.Visible;
        }

        private void RadioButton_Private_Checked(object sender, RoutedEventArgs e)
        {
            Label_VAT.Visibility = Visibility.Collapsed;
            TextBox_Vat.Visibility = Visibility.Collapsed;
        }
    }
}
