﻿using System;
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
using ApplicationLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for DailyBookingsPage.xaml
    /// </summary>
    public partial class DailyBookingsPage : Window, ISubscriber
    {
        private BookingController bc = new BookingController();
        private int fontSize = 20;
        public Thread updateThread;
        private List<Button> buttonsInGrid = new List<Button>();
        public DateTime CurrentDateTime { get; set; }

        public DailyBookingsPage(DateTime currentDateTime, string day)
        {
            InitializeComponent();
            bc.br.RegisterSubscriber(this); // Subscribe to repository, to get booking updates


            CurrentDateTime = currentDateTime;

            Label_Day.Content = day;
            updateThread = new Thread(() => LoadDay());
            updateThread.Start();
        }

        // Load all the bookings for this day
        public void LoadDay(bool runOnce = false)
        {
            bool running = true;
            while (running)
            {
                ClearGrid();

                for (int i = 2; i < 7; i++) // ROw
                {

                    for (int i2 = 1; i2 < 5; i2++) // Column
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Border b = new Border();
                            b.BorderThickness = new Thickness(1);
                            b.BorderBrush = Brushes.Gray;
                            Grid.SetColumn(b, i2);
                            Grid.SetRow(b, i);
                            Grid_Main.Children.Add(b);
                        });
                        

                    }

                }


                // Load all bookings to BR
                bc.LoadAllBookingsFromDatabase();

                List<string> listBooking = bc.GetDailyBookings(CurrentDateTime);


                bool p1 = false;
                bool p2 = false;
                bool p3 = false;
                bool p4 = false;
                foreach (string item in listBooking)
                {
                    string[] split = item.Split(';');
                    string content = split[0] + ": " + split[2];
                    if (split[1] == "08:00")
                    {
                        if (p1 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(1, 2, fontSize, content);
                                p1 = true;
                            });
                        }
                        else if (p2 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(2, 2, fontSize, content);
                                p2 = true;
                            });
                        }
                        else if (p3 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(3, 2, fontSize, content);
                                p3 = true;
                            });
                        }
                        else if (p4 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(4, 2, fontSize, content);
                                p4 = true;
                            });
                        }
                    }
                }
                p1 = false;
                p2 = false;
                p3 = false;
                p4 = false;
                foreach (string item in listBooking)
                {
                    string[] split = item.Split(';');
                    string content = split[0] + ": " + split[2];
                    if (split[1] == "10:00")
                    {
                        if (p1 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(1, 3, fontSize, content);
                                p1 = true;
                            });
                        }
                        else if (p2 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(2, 3, fontSize, content);
                                p2 = true;
                            });
                        }
                        else if (p3 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(3, 3, fontSize, content);
                                p3 = true;
                            });
                        }
                        else if (p4 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(4, 3, fontSize, content);
                                p1 = true;
                            });
                        }
                    }
                }
                p1 = false;
                p2 = false;
                p3 = false;
                p4 = false;
                foreach (string item in listBooking)
                {
                    string[] split = item.Split(';');
                    string content = split[0] + ": " + split[2];
                    if (split[1] == "12:00")
                    {
                        if (p1 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(1, 4, fontSize, content);
                                p1 = true;
                            });
                        }
                        else if (p2 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(2, 4, fontSize, content);
                                p2 = true;
                            });
                        }
                        else if (p3 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(3, 4, fontSize, content);
                                p3 = true;
                            });
                        }
                        else if (p4 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(4, 4, fontSize, content);
                                p4 = true;
                            });
                        }
                    }
                }
                p1 = false;
                p2 = false;
                p3 = false;
                p4 = false;
                foreach (string item in listBooking)
                {
                    string[] split = item.Split(';');
                    string content = split[0] + ": " + split[2];
                    if (split[1] == "14:00")
                    {
                        if (p1 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(1, 5, fontSize, content);
                                p1 = true;
                            });
                        }
                        else if (p2 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(2, 5, fontSize, content);
                                p2 = true;
                            });
                        }
                        else if (p3 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(3, 5, fontSize, content);
                                p3 = true;
                            });
                        }
                        else if (p4 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(4, 5, fontSize, content);
                                p4 = true;
                            });
                        }
                    }
                }
                p1 = false;
                p2 = false;
                p3 = false;
                p4 = false;
                foreach (string item in listBooking)
                {
                    string[] split = item.Split(';');
                    string content = split[0] + ": " + split[2];
                    if (split[1] == "16:00")
                    {
                        if (p1 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(1, 6, fontSize, content);
                                p1 = true;
                            });
                        }
                        else if (p2 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(2, 6, fontSize, content);
                                p2 = true;
                            });
                        }
                        else if (p3 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(3, 6, fontSize, content);
                                p3 = true;
                            });
                        }
                        else if (p4 == false)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                Button btn = NewButton(4, 6, fontSize, content);
                                p4 = true;
                            });
                        }
                    }
                }

                if (runOnce == true)
                    running = false;

                Thread.Sleep(5000);
            }
            
        }

        private Button NewButton(int column,int row,int fontSize, string content)
        {
            Button btn = new Button();
            btn.Background = null;
            btn.Content = content;
            btn.FontSize = fontSize;
            btn.HorizontalContentAlignment = HorizontalAlignment.Center;
            btn.VerticalContentAlignment = VerticalAlignment.Center;
            Grid.SetColumn(btn, column);
            Grid.SetRow(btn, row);
            Grid_Main.Children.Add(btn);
            btn.Click += (o, e) => button_clickEvent(o, e);

            buttonsInGrid.Add(btn); // Store instances, so they can be cleared later
            return btn;
        }

        private void button_clickEvent(object sender,RoutedEventArgs e )
        {
            Button b = (Button)sender;
            string btn1 = (string)b.Content;
            string[] split = btn1.Split(':');

            Window specificBooking = new SpecificBookingWindow(split[0]); // Get the BookingId from btn.Content
            specificBooking.Height = 540;
            specificBooking.Width = 520;
            specificBooking.Show();
        }

        private void ClearGrid()
        {
            foreach (Button button in buttonsInGrid)
            {
                Dispatcher.Invoke(() => Grid_Main.Children.Remove(button));
            }
        }


        // When daily bookings windows is closed
        private void Window_Closed(object sender, EventArgs e)
        {
            updateThread.Abort();
        }



        // Observer Pattern Specific Method
        public void Update()
        {
            Thread t = new Thread(() => LoadDay(true));
            t.Start();
        }
    }
}
