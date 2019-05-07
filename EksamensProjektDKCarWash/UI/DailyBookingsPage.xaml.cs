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
    /// Interaction logic for DailyBookingsPage.xaml
    /// </summary>
    public partial class DailyBookingsPage : Page
    {
        public DateTime CurrentDateTime { get; set; }

        public DailyBookingsPage(DateTime currentDateTime, string day)
        {
            InitializeComponent();

            CurrentDateTime = currentDateTime;

            Label_Day.Content = day;

            LoadDay();
        }

        // Load all the bookings for this day
        public void LoadDay ()
        {

        }

    }
}
