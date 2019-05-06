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

namespace EksamensProjektDKCarWash
{
    /// <summary>
    /// Interaction logic for WeeklyBookingsPage.xaml
    /// </summary>
    public partial class WeeklyBookingsPage : Page
    {
        public WeeklyBookingsPage()
        {
            InitializeComponent();

            Label_MonthWeekYear.Content = GetMonthWeekYear();
        }

        public string GetMonthWeekYear ()
        {
            string month = MonthToString(DateTime.Now.Month);

            CultureInfo cultureInfo = new CultureInfo("da-DK");
            string week = cultureInfo.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString();

            string year = DateTime.Now.Year.ToString();

            return $"{month} - {week} - {year}";
        }
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
        }

    }


}
