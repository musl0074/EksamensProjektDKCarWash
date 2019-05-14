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
using ApplicationLayer;

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
        private BookingController bc = new BookingController();

        public DateTime CurrentMonday { get; set; }

        public WeeklyBookingsPage()
        {
            InitializeComponent();

            Label_MonthWeekYear.Content = GetMondayToSaturday();
            CurrentMonday = GetCurrentMonday();
            LoadWeeklyBookings(CurrentMonday);
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

        public void LoadWeeklyBookings (DateTime monday)
        {
            // Draw the grid
            for (int i = 1; i < 7; i++)
            {
                for (int y = 2; y < 7; y++)
                {
                    Border b = new Border();
                    b.BorderThickness = new Thickness(1);
                    b.BorderBrush = Brushes.Gray;

                    Grid.SetColumn(b, i);
                    Grid.SetRow(b, y);
                    Grid_Main.Children.Add(b);
                }
            }




            List<List<string>> bookings = bc.ShowBooking(monday); // Get all bookings.ToString() from Monday to Saturday
            
            int column = 1; // Determines which column to place the UI element in.

            foreach (List<string> day in bookings) // Loop through every day
            {
                int t8 = 0, t10 = 0, t12 = 0, t14 = 0, t16 = 0;  // Counters for bookings @ timespan


                foreach (string booking in day) // Loop through each booking in a day
                {
                    string[] split = booking.Split(';'); // Split the ToString() to get data. 

                    switch (split[2]) // StartTime
                    {
                        case "08:00":
                            t8++;
                            break;

                        case "10:00":
                            t10++;
                            break;

                        case "12:00":
                            t12++;
                            break;

                        case "14:00":
                            t14++;
                            break;

                        case "16:00":
                            t16++;
                            break;
                    }
                }

                // Insert Borders at their respective rows and columns
                if (t8 != 0)
                {
                    Label l = new Label();
                    l.Content = t8;
                    l.VerticalContentAlignment = VerticalAlignment.Center;
                    l.HorizontalContentAlignment = HorizontalAlignment.Center;
                    l.FontSize = 75;

                    Grid.SetColumn(l, column);
                    Grid.SetRow(l, 2);
                    Grid_Main.Children.Add(l);
                }
                if (t10 != 0)
                {
                    Label l = new Label();
                    l.Content = t8;
                    l.VerticalContentAlignment = VerticalAlignment.Center;
                    l.HorizontalContentAlignment = HorizontalAlignment.Center;
                    l.FontSize = 75;

                    Grid.SetColumn(l, column);
                    Grid.SetRow(l, 3);
                    Grid_Main.Children.Add(l);
                }
                if (t12 != 0)
                {
                    Label l = new Label();
                    l.Content = t8;
                    l.VerticalContentAlignment = VerticalAlignment.Center;
                    l.HorizontalContentAlignment = HorizontalAlignment.Center;
                    l.FontSize = 75;

                    Grid.SetColumn(l, column);
                    Grid.SetRow(l, 4);
                    Grid_Main.Children.Add(l);
                }
                if (t14 != 0)
                {
                    Label l = new Label();
                    l.Content = t8;
                    l.VerticalContentAlignment = VerticalAlignment.Center;
                    l.HorizontalContentAlignment = HorizontalAlignment.Center;
                    l.FontSize = 75;

                    Grid.SetColumn(l, column);
                    Grid.SetRow(l, 5);
                    Grid_Main.Children.Add(l);
                }
                if (t16 != 0)
                {
                    Label l = new Label();
                    l.Content = t8;
                    l.VerticalContentAlignment = VerticalAlignment.Center;
                    l.HorizontalContentAlignment = HorizontalAlignment.Center;
                    l.FontSize = 75;

                    Grid.SetColumn(l, column);
                    Grid.SetRow(l, 6);
                    Grid_Main.Children.Add(l);
                }

                column++;  // Increment column, so the next iteration of "Day" will use the correct column
            }


        }

        
        // Button - NAVIGATE LEFT
        private void Button_NavigateLeft_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonday = CurrentMonday.AddDays(-7); // The new CurrentMonday
            Label_MonthWeekYear.Content = GetMonthWeekYear(CurrentMonday);

            LoadWeeklyBookings(CurrentMonday);
        }

        // Button - NAVIGATE RIGHT
        private void Button_NavigateRight_Click(object sender, RoutedEventArgs e)
        {
            CurrentMonday = CurrentMonday.AddDays(+7); // The new CurrentMonday
            Label_MonthWeekYear.Content = GetMonthWeekYear(CurrentMonday);

            LoadWeeklyBookings(CurrentMonday);
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
