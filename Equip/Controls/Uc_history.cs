using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_history : UserControl
    {
        private string connectionString = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";
        private Timer refreshTimer; // Timer for automatic refresh

        public Uc_history()
        {
            InitializeComponent();
            InitializeTimer(); // Initialize and start the refresh timer

            // Subscribe to the CellFormatting event
            dgvHistory.CellFormatting += DgvHistory_CellFormatting;
        }

        private void Uc_history_Load(object sender, EventArgs e)
        {
            LoadHistoryData(); // Load initial data when the user control loads
        }

        // Initialize the refresh timer
        private void InitializeTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 5000; // Set the interval to 5000 milliseconds (5 seconds)
            refreshTimer.Tick += RefreshTimer_Tick; // Handle the timer tick event
            refreshTimer.Start(); // Start the timer
        }

        // Timer tick event to refresh the data
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadHistoryData(); // Refresh the history data
        }

        // Load history data from the database
        private void LoadHistoryData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // Order data by the 'DateMovedToHistory' column in descending order
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ReservationHistoryTbl ORDER BY DateMovedToHistory DESC", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvHistory.DataSource = dt;

                    // Clear any row selection
                    dgvHistory.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // CellFormatting event to format date and time columns
        private void DgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format the BorrowDate and ReturnDate columns
            if (dgvHistory.Columns[e.ColumnIndex].Name == "BorrowDate" || dgvHistory.Columns[e.ColumnIndex].Name == "ReturnDate")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (e.Value is DateTime dateValue)
                    {
                        e.Value = dateValue.ToString("MM/dd/yyyy");
                        e.FormattingApplied = true; // Indicate formatting is applied
                    }
                }
            }

            // Format the BorrowTime and ReturnTime columns
            if (dgvHistory.Columns[e.ColumnIndex].Name == "BorrowTime" || dgvHistory.Columns[e.ColumnIndex].Name == "ReturnTime")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (e.Value is TimeSpan timeSpanValue)
                    {
                        // Convert TimeSpan to a formatted string (hh:mm tt)
                        DateTime dateTime = DateTime.Today.Add(timeSpanValue); // Convert TimeSpan to DateTime for formatting
                        e.Value = dateTime.ToString("hh:mm tt");
                        e.FormattingApplied = true; // Indicate formatting is applied
                    }
                }
            }
        }

    }
}
