using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Equip.Controls
{
    public partial class Uc_dashboard : UserControl
    {
        private string connectionString = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";
        private Timer refreshTimer;

        public Uc_dashboard()
        {
            InitializeComponent();
            LoadReservationCounts();

            // Initialize and start the timer to refresh every 60 seconds
            refreshTimer = new Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        // Event handler for the timer's tick event
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadReservationCounts();
        }

        private void LoadReservationCounts()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Today's reservations
                SqlCommand cmdToday = new SqlCommand("SELECT COUNT(*) FROM ReservationTbl WHERE CAST(BorrowDate AS DATE) = CAST(GETDATE() AS DATE)", con);
                int todayReservations = (int)cmdToday.ExecuteScalar();
                lblTodayReservations.Text = todayReservations.ToString();

                // This week's reservations
                SqlCommand cmdThisWeek = new SqlCommand("SELECT COUNT(*) FROM ReservationTbl WHERE BorrowDate >= DATEADD(day, -DATEDIFF(day, 0, GETDATE()) % 7, GETDATE()) AND BorrowDate < DATEADD(day, 1, GETDATE())", con);
                int thisWeekReservations = (int)cmdThisWeek.ExecuteScalar();
                lblThisWeekReservations.Text = thisWeekReservations.ToString();
            }
        }
    }
}
