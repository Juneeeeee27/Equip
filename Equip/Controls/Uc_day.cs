using System;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_day : UserControl
    {
        private string _day;
        private string _date;
        private string _weekday;
        private int _month; // Add _month field
        private int _year;  // Add _year field

        public Uc_day(string day, int month, int year)
        {
            InitializeComponent();
            _day = day;
            _month = month; // Assign the passed month
            _year = year;   // Assign the passed year

            lblDay.Text = _day;

            // Subscribe to Click event for the UserControl and its children
            this.Click += Uc_day_Click;
            panelDay.Click += Uc_day_Click;
            lblDay.Click += Uc_day_Click;
        }

        // Property to set/get the day
        public string Day
        {
            get { return _day; }
            set
            {
                _day = value;
                lblDay.Text = _day; // Update label when day is set
            }
        }

        // Property to set/get the date
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        // Property to set/get the weekday
        public string Weekday
        {
            get { return _weekday; }
            set { _weekday = value; }
        }

        // Event handler for click events
        // Event handler for click events in Uc_day
        private void Uc_day_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_day))
            {
                DateTime selectedDateTime = new DateTime(_year, _month, int.Parse(_day));
                string selectedDate = selectedDateTime.ToString("yyyy-MM-dd");

                // Open the form and pass the selected date
                FormDetails formDetails = new FormDetails(selectedDate);
                formDetails.ShowDialog();
            }
        }


    }
}
