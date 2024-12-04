using System;
using System.Globalization;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_calendar : UserControl
    {
        public static int _month, _year;

        public Uc_calendar()
        {
            InitializeComponent();
        }

        private void Uc_calendar_Load(object sender, EventArgs e)
        {
            // Initialize the calendar with the current month and year
            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;
            ShowDays(_month, _year);
        }

        // Method to display days of a given month and year
        private void ShowDays(int month, int year)
        {
            daycontainer.Controls.Clear(); // Clear previous day controls
            _month = month;
            _year = year;

            // Get the name of the month and display it
            string monthName = new DateTimeFormatInfo().GetMonthName(month);
            lblMonth.Text = $"{monthName} {year}";

            // Create a DateTime object for the first day of the month
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int firstWeekday = (int)firstDayOfMonth.DayOfWeek; // 0 = Sunday, 1 = Monday, ...

            // Add empty controls to align the first day of the month correctly
            for (int i = 0; i < firstWeekday; i++)
            {
                Uc_day emptyDay = new Uc_day("", 0, 0); // Pass placeholders for empty days
                daycontainer.Controls.Add(emptyDay);
            }

            // Add controls for each day of the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                Uc_day dayControl = new Uc_day(day.ToString(), month, year);
                daycontainer.Controls.Add(dayControl);
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            _month++;
            if (_month > 12) // If month exceeds December, go to January of the next year
            {
                _month = 1;
                _year++;
            }

            ShowDays(_month, _year); // Refresh the calendar with the new month
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _month--;
            if (_month < 1) // If month is less than January, go to December of the previous year
            {
                _month = 12;
                _year--;
            }

            ShowDays(_month, _year); // Refresh the calendar with the new month
        }
    }
}
