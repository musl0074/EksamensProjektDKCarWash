using System;
using System.Collections.Generic;
using System.Globalization;
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

            /*
            Border b = new Border();
            b.Background = Brushes.Blue;
            Grid.SetRow(b, 2);
            Grid.SetColumn(b, 1);
            Grid_Main.Children.Add(b);
            */

    public partial class WeeklyBookingsPage : Page
    {
        public DateTime CurrentMonday { get; set; }

        public WeeklyBookingsPage()
        {
            InitializeComponent();

            Label_MonthWeekYear.Content = GetMondayToSaturday();
            CurrentMonday = GetCurrentMonday();
        }

        public DateTime GetCurrentMonday ()
        {
            return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        }
        public string GetMondayToSaturday ()
        {
            DateTime monday = GetCurrentMonday();
            return $"Mandag d. {monday.ToString("dd/MM/yyyy")} - Lørdag d. {monday.AddDays(+5).ToString("dd/MM/yyyy")}";
        }
        public string GetMonthWeekYear (DateTime dateTime)
        {
            return $"Mandag d. {dateTime.ToString("dd/MM/yyyy")} - Lørdag d. {dateTime.AddDays(+5).ToString("dd/MM/yyyy")}";
        }


        // MonthToString
        /*
        public string MonthToString (int month)
        {
            switch (month)
            {
                case 1:
                    return "Januar";

                case 2:
                    return "Februar";

                case 3:
                    return "Marts";

                case 4:
                    return "April";

                case 5:
                    return "Maj";

                case 6:
                    return "Juni";

                case 7:
                    return "Juli";

                case 8:
                    return "August";

                case 9:
                    return "September";

                case 10:
                    return "Oktober";

                case 11:
                    return "November";

                case 12:
                    return "December";
            }

            return "UNKNOWN";
        }  // MULIGVIS SLETTES
        */

        
        // Button - NAVIGATE LEFT
        private void Button_NavigateLeft_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonday = CurrentMonday.AddDays(-7); // The new CurrentMonday
            Label_MonthWeekYear.Content = GetMonthWeekYear(CurrentMonday);

            LoadWeek(CurrentMonday);
        }

        // Button - NAVIGATE RIGHT
        private void Button_NavigateRight_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonday = CurrentMonday.AddDays(+7); // The new CurrentMonday
            Label_MonthWeekYear.Content = GetMonthWeekYear(CurrentMonday);

            LoadWeek(CurrentMonday);
        }

        // Load all bookings for that week
        public void LoadWeek (DateTime currentMonday)
        {

        }




        private void Button_Monday_Click(object sender, RoutedEventArgs e)
        {
            DateTime monday = CurrentMonday;
            LoadDay(monday, "Mandag");
        }

        private void Button_Tuesday_Click(object sender, RoutedEventArgs e)
        {
            DateTime tuesday = CurrentMonday.AddDays(+1);
            LoadDay(tuesday, "Tirsdag");

        }

        private void Button_Wednesday_Click(object sender, RoutedEventArgs e)
        {
            DateTime wednesday = CurrentMonday.AddDays(+2);
            LoadDay(wednesday, "Onsdag");
        }

        private void Button_Thursday_Click(object sender, RoutedEventArgs e)
        {
            DateTime thursday = CurrentMonday.AddDays(+3);
            LoadDay(thursday, "Torsdag");
        }

        private void Button_Friday_Click(object sender, RoutedEventArgs e)
        {
            DateTime friday = CurrentMonday.AddDays(+4);
            LoadDay(friday, "Fredag");
        }

        private void Button_Saturday_Click(object sender, RoutedEventArgs e)
        {
            DateTime saturday = CurrentMonday.AddDays(+5);
            LoadDay(saturday, "Lørdag");
        }


        public void LoadDay (DateTime date, string day)
        {
            Window dailyBookingsWindow = new Window();
            dailyBookingsWindow.WindowState = WindowState.Maximized;
            dailyBookingsWindow.Content = new DailyBookingsPage(date, day);
            dailyBookingsWindow.Show();
        }

       
    }


}
