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
using ApplicationLayer;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for DailyBookingsPage.xaml
    /// </summary>
    public partial class DailyBookingsPage : Page
    {
        private BookingController bc = new BookingController();
        public DateTime CurrentDateTime { get; set; }
        private int fontSize = 20;
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
            List<string> listBooking = bc.GetDailyBookings(CurrentDateTime);



            for (int i = 2; i < 7; i++) // ROw
            {

                for (int i2 = 1; i2 < 5; i2++) // Column
                {
                    Border b = new Border();
                    b.BorderThickness = new Thickness(1);
                    b.BorderBrush = Brushes.Gray;
                    Grid.SetColumn(b, i2);
                    Grid.SetRow(b, i);
                    Grid_Main.Children.Add(b);

                }

            }
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
                        Button btn = NewButton(1, 2, fontSize, content);
                        p1 = true;
                    }
                    else if(p2 == false)
                    {
                        Button btn = NewButton(2, 2, fontSize, content);
                        p2 = true;
                    }
                    else if ( p3 == false)
                    {
                        Button btn = NewButton(3, 2, fontSize, content);
                        p3 = true;
                    }
                    else if (p4 == false)
                    {
                        Button btn = NewButton(4, 2, fontSize, content);
                        p4 = true;
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
                        Button btn = NewButton(1, 3, fontSize, content);
                        p1 = true;
                    }
                    else if (p2 == false)
                    {
                        Button btn = NewButton(2, 3, fontSize, content);
                        p2 = true;
                    }
                    else if (p3 == false)
                    {
                        Button btn = NewButton(3, 3, fontSize, content);
                        p3 = true;
                    }
                    else if (p4 == false)
                    {
                        Button btn = NewButton(4, 3, fontSize, content);
                        p4 = true;
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
                        Button btn = NewButton(1, 4, fontSize, content);
                        p1 = true;
                    }
                    else if (p2 == false)
                    {
                        Button btn = NewButton(2, 4, fontSize, content);
                        p2 = true;
                    }
                    else if (p3 == false)
                    {
                        Button btn = NewButton(3, 4, fontSize, content);
                        p3 = true;
                    }
                    else if (p4 == false)
                    {
                        Button btn = NewButton(4, 4, fontSize, content);
                        p4 = true;
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
                        Button btn = NewButton(1, 5, fontSize, content);
                        p1 = true;
                    }
                    else if (p2 == false)
                    {
                        Button btn = NewButton(2, 5, fontSize, content);
                        p2 = true;
                    }
                    else if (p3 == false)
                    {
                        Button btn = NewButton(3, 5, fontSize, content);
                        p3 = true;
                    }
                    else if (p4 == false)
                    {
                        Button btn = NewButton(4, 5, fontSize, content);
                        p4 = true;
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
                        Button btn = NewButton(1, 6, fontSize, content);
                        p1 = true;
                    }
                    else if (p2 == false)
                    {
                        Button btn = NewButton(2, 6, fontSize, content);
                        p2 = true;
                    }
                    else if (p3 == false)
                    {
                        Button btn = NewButton(3, 6, fontSize, content);
                        p3 = true;
                    }
                    else if (p4 == false)
                    {
                        Button btn = NewButton(4, 6, fontSize, content);
                        p4 = true;
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
                        Button btn = NewButton(1, 7, fontSize, content);
                        p1 = true;
                    }
                    else if (p2 == false)
                    {
                        Button btn = NewButton(2, 7, fontSize, content);
                        p2 = true;
                    }
                    else if (p3 == false)
                    {
                        Button btn = NewButton(3, 7, fontSize, content);
                        p3 = true;
                    }
                    else if (p4 == false)
                    {
                        Button btn = NewButton(4, 7, fontSize, content);
                        p4 = true;
                    }
                }
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

            return btn;
        }
        private void button_clickEvent(object sender,RoutedEventArgs e )
        {
            MessageBox.Show("Hello RIcky");
        }
    }
}
