using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_Decommissioned : UserControl
    {
        private Timer refreshTimer;

        public Uc_Decommissioned()
        {
            InitializeComponent();
            InitializeRefreshTimer();
            LoadDecommissionedData();
        }

        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer
            {
                Interval = 1000 // Set the interval to 60 seconds (60000 ms) or adjust as needed
            };
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadDecommissionedData();
        }

        private void LoadDecommissionedData()
        {
            string connectionString = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";
            string query = "SELECT * FROM DecommissionedTbl"; // Update table name

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dgvDecommissioned.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
