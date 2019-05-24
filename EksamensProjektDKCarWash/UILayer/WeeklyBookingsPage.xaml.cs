using System;
using System.Collections.Generic;
using System.Globalization;
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
using ApplicationLayer;

namespace UILayer
{
    public partial class WeeklyBookingsPage : Page, ISubscriber
    {
        private BookingController bc = new BookingController();
        public Thread updateThread;
        public DailyBookingsPage dailyBookingsPage;
        private List<Label> labelsInGrid = new List<Label>();
        private object loadWeeklyLock = new object();
        public DateTime CurrentMonday { get; set; }


        public WeeklyBookingsPage()
        {
            InitializeComponent();

            bc.br.RegisterSubscriber(this); // Subscribe to BookingRepo, to recieve updates

            CurrentMonday = GetCurrentMonday(); // Get the current monday in THIS week (Local time on computer)
            Label_MonthWeekYear.Content = GetMondayToSaturday(CurrentMonday); // Load in dates monday to saturday in this week

            updateThread = new Thread(() => LoadWeeklyBookings(CurrentMonday)); // A thread that updates the UI every "x" seconds
            updateThread.Start();
        }




        public DateTime GetCurrentMonday ()
        {
            return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        }
        

        public string GetMondayToSaturday(DateTime dateTime)
        {
            return $"Mandag d. {dateTime.ToString("dd/MM/yyyy")} - Lørdag d. {dateTime.AddDays(+5).ToString("dd/MM/yyyy")}";
        }

        public void LoadWeeklyBookings (DateTime monday, bool runOnce = false)
        {
            bool running = true;
            while(running)
            {
                lock (loadWeeklyLock)
                {
                    ClearGrid(); // Clear the labels in grid, before updating it.

                    // Draw the grid
                    for (int i = 1; i < 7; i++)
                    {
                        for (int y = 2; y < 7; y++)
                        {
                            try
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    Border b = new Border();
                                    b.BorderThickness = new Thickness(1);
                                    b.BorderBrush = Brushes.Gray;

                                    Grid.SetColumn(b, i);
                                    Grid.SetRow(b, y);
                                    Grid_Main.Children.Add(b);
                                });
                            }
                            catch (TaskCanceledException e)
                            {
                                MessageBox.Show("Fejl");
                            }
                        }
                    }

                    // Load all bookings to BR
                    bc.LoadAllBookingsFromDatabase();

                    // -> BR -> Find all with Monday to Saturday
                    List<List<string>> bookings = bc.GetWeeklyBookings(monday); // Get all bookings.ToString() from Monday to Saturday


                    int column = 1; // Determines which column to place the UI element in.

                    foreach (List<string> day in bookings) // Loop through every day
                    {
                        int t8 = 0, t10 = 0, t12 = 0, t14 = 0, t16 = 0;  // Counters for bookings @ timespan


                        foreach (string booking in day) // Loop through each booking in a day
                        {
                            string[] split = booking.Split(';'); // Split the ToString() to get data. 

                            switch (split[3]) // StartTime
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
                            NewLabelToGrid(t8, column, 2);
                        }
                        if (t10 != 0)
                        {
                            NewLabelToGrid(t10, column, 3);
                        }
                        if (t12 != 0)
                        {
                            NewLabelToGrid(t12, column, 4);
                        }
                        if (t14 != 0)
                        {
                            NewLabelToGrid(t14, column, 5);
                        }
                        if (t16 != 0)
                        {
                            NewLabelToGrid(t16, column, 6);
                        }

                        column++;  // Increment column, so the next iteration of "Day" will use the correct column
                    }
                }

                if (runOnce == true) // If true, stop the loop
                    running = false;

                
                Thread.Sleep(5000);
            }
        }


        // Creates a new label and adds it to the Grid_Main
        private void NewLabelToGrid (int content, int column, int row)
        {
            Dispatcher.Invoke(() =>
            {
                Label l = new Label();
                l.Content = content;
                l.VerticalContentAlignment = VerticalAlignment.Center;
                l.HorizontalContentAlignment = HorizontalAlignment.Center;
                l.FontSize = 75;

                Grid.SetColumn(l, column);
                Grid.SetRow(l, row);
                Grid_Main.Children.Add(l);

                labelsInGrid.Add(l); // Save the instance, so it can be cleared every update
            });
        }

        
        

        // Button - NAVIGATE LEFT
        private void Button_NavigateLeft_Click(object sender, RoutedEventArgs e)
        {
            updateThread.Abort(); // Stop the thread from updating while changing weeks
            ClearGrid();

            CurrentMonday = CurrentMonday.AddDays(-7); // The new CurrentMonday
            Label_MonthWeekYear.Content = GetMondayToSaturday(CurrentMonday);

            updateThread = new Thread(() => LoadWeeklyBookings(CurrentMonday));
            updateThread.Start();
        }

        // Button - NAVIGATE RIGHT
        private void Button_NavigateRight_Click(object sender, RoutedEventArgs e)
        {
            updateThread.Abort(); // Stop the thread from updating while changing weeks
            ClearGrid();

            CurrentMonday = CurrentMonday.AddDays(+7); // The new CurrentMonday
            Label_MonthWeekYear.Content = GetMondayToSaturday(CurrentMonday);

            updateThread = new Thread(() => LoadWeeklyBookings(CurrentMonday));
            updateThread.Start();
        }
        
        // Clears the grid for all the labels that has been added
        private void ClearGrid ()
        {
            foreach (Label label in labelsInGrid)
            {
                Dispatcher.Invoke(() => Grid_Main.Children.Remove(label));
            }
        }



        // Load in a specific day
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


        // Opens a new window using the Date to show the respective bookings & day to set the Label to ex: "Mandag"
        public void LoadDay (DateTime date, string day)
        {
            Window dailyBookingsWindow = new DailyBookingsPage(date, day);
            dailyBookingsWindow.WindowState = WindowState.Maximized;
            dailyBookingsWindow.Show();
        }




        // Observer Pattern Specific Method
        // Starts a new thread that runs once and loads all weekly bookings. This thread is synchronized and controlled with the updateThread using lock(object)
        public void Update()
        {
            Thread observerUpdate = new Thread(() => LoadWeeklyBookings(CurrentMonday, true));
            observerUpdate.Start();
        }
    }


}
