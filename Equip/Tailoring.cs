using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Equip.Controls;

namespace Equip
{
    public partial class Tailoring : Form
    {
        private Functions _functions;
        private ContextMenuStrip contextMenu; // Right-click menu
        private Timer refreshTimer; // Timer for automatic refresh

        public Tailoring()
        {
            InitializeComponent();
            _functions = new Functions();
            LoadTailoringData();  // Load all data initially
            StyleDataGridView(dgvTailoring);  // Apply DataGridView styling
            InitializeContextMenu();
            InitializeTimer(); // Initialize and start the refresh timer
        }

        private void InitializeTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 5000; // Set the interval to 5000 milliseconds (5 seconds)
            refreshTimer.Tick += RefreshTimer_Tick; // Handle the timer tick event
            refreshTimer.Start(); // Start the timer
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadTailoringData(); // Refresh the history data
        }

        private void InitializeContextMenu()
        {
            // Create the context menu
            contextMenu = new ContextMenuStrip();
            var decommissionItem = new ToolStripMenuItem("Decommission");
            decommissionItem.Click += DecommissionItem_Click; // Event handler for decommission action
            contextMenu.Items.Add(decommissionItem);

            // Attach the context menu to the DataGridView
            dgvTailoring.ContextMenuStrip = contextMenu;
        }

        private void DecommissionItem_Click(object sender, EventArgs e)
        {
            if (dgvTailoring.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to decommission.");
                return;
            }

            DataGridViewRow selectedRow = dgvTailoring.SelectedRows[0];
            int propertyNo = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);
            string equipmentType = selectedRow.Cells["EquipmentType"].Value?.ToString();
            string equipmentName = selectedRow.Cells["EquipmentName"].Value.ToString();
            string specification = selectedRow.Cells["Specification"].Value.ToString();
            string location = selectedRow.Cells["Location"].Value.ToString();
            DateTime? yearDonated = selectedRow.Cells["YearDonated"].Value as DateTime?;
            string donatedBy = selectedRow.Cells["DonatedBy"].Value?.ToString();
            decimal? amount = selectedRow.Cells["Amount"].Value as decimal?;
            DateTime? yearAcquired = selectedRow.Cells["YearAcquired"].Value as DateTime?;

            // Confirm decommission action
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to decommission this equipment?",
                                                        "Confirm Decommission", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
                    {
                        con.Open();

                        // Insert into DecommissionedTbl
                        string decommissionQuery = @"
                    INSERT INTO DecommissionedTbl
                    (PropertyNo, EquipmentType, EquipmentName, Specification, Location, Quantity, YearDonated, DonatedBy, Amount, YearAcquired)
                    VALUES 
                    (@PropertyNo, @EquipmentType, @EquipmentName, @Specification, @Location, @Quantity, @YearDonated, @DonatedBy, @Amount, @YearAcquired)";

                        SqlCommand cmd = new SqlCommand(decommissionQuery, con);
                        cmd.Parameters.AddWithValue("@PropertyNo", propertyNo);
                        cmd.Parameters.AddWithValue("@EquipmentType", (object)equipmentType ?? DBNull.Value); // Null if EquipmentType is missing
                        cmd.Parameters.AddWithValue("@EquipmentName", equipmentName);
                        cmd.Parameters.AddWithValue("@Specification", specification);
                        cmd.Parameters.AddWithValue("@Location", location);
                        cmd.Parameters.AddWithValue("@Quantity", DBNull.Value); // Always set Quantity to NULL for ElectronicsTbl
                        cmd.Parameters.AddWithValue("@YearDonated", yearDonated.HasValue ? (object)yearDonated.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@DonatedBy", (object)donatedBy ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Amount", amount.HasValue ? (object)amount.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@YearAcquired", yearAcquired.HasValue ? (object)yearAcquired.Value : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    // Remove the record from ElectronicTbl after successful insertion
                    using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
                    {
                        con.Open();
                        string deleteQuery = "DELETE FROM TailoringTbl WHERE PropertyNo = @PropertyNo";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                        deleteCmd.Parameters.AddWithValue("@PropertyNo", propertyNo);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Refresh the DataGridView
                    LoadTailoringData();
                    MessageBox.Show("The equipment has been successfully decommissioned.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during decommission: " + ex.Message);
                }
            }
        }

        private void dgvElectronic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvTailoring.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dgvTailoring.ClearSelection();
                    dgvTailoring.Rows[hitTest.RowIndex].Selected = true;
                    contextMenu.Show(dgvTailoring, e.Location);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTailoring.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            DataGridViewRow selectedRow = dgvTailoring.SelectedRows[0];
            int selectedRowId = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM TailoringTbl WHERE PropertyNo = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", selectedRowId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record deleted successfully.");
                    LoadTailoringData();  // Reload data after deletion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Uc_equipment back = new Uc_equipment();
            back.Show();
        }

        private void btnAddequip_Click(object sender, EventArgs e)
        {
            AddEquipTailoring addEquipTailoring = new AddEquipTailoring();
            addEquipTailoring.RecordAdded += AddEquip_RecordAdded;

            DialogResult result = addEquipTailoring.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadTailoringData();  // Reload data after adding new equipment
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTailoring.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.");
                return;
            }

            DataGridViewRow selectedRow = dgvTailoring.SelectedRows[0];
            int selectedRowId = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);

            AddEquipTailoring addEquipTailoring = new AddEquipTailoring(selectedRowId);
            addEquipTailoring.RecordAdded += AddEquip_RecordAdded;

            addEquipTailoring.ShowDialog();
        }

        // Modified LoadTailoringData method to include a filter
        private void LoadTailoringData(string filter = "")
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT * FROM TailoringTbl";
                if (!string.IsNullOrEmpty(filter))
                {
                    // Filter data based on the input search term
                    query += " WHERE PropertyNo LIKE @Filter OR EquipmentName LIKE @Filter OR Specification LIKE @Filter OR Location LIKE @Filter";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Filter", "%" + filter + "%");  // Search for any matching records
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dgvTailoring.DataSource = table;  // Bind filtered data to DataGridView
            }
        }

        private void Tailoring_Load(object sender, EventArgs e)
        {
            LoadTailoringData();  // Load all data initially
            StyleDataGridView(dgvTailoring);  // Apply styling on load
            dgvTailoring.CellFormatting += dgvTailoring_CellFormatting;
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(220, 220, 220);

            dgv.RowTemplate.Height = 30;
        }

        private void dgvTailoring_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTailoring.Columns[e.ColumnIndex].HeaderText == "YearDonated" ||
                dgvTailoring.Columns[e.ColumnIndex].HeaderText == "YearAcquired")
            {
                if (e.Value != null && e.Value is DateTime dateValue)
                {
                    e.Value = dateValue.ToString("MM/dd/yyyy");
                    e.FormattingApplied = true;
                }
            }
        }

        private void AddEquip_RecordAdded(object sender, EventArgs e)
        {
            LoadTailoringData();  // Reload data after adding or editing equipment
        }

        private void dgvTailoring_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks, or remove this if not necessary
        }

        // Event handler for search functionality
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;  // Get the current search text
            LoadTailoringData(searchQuery);  // Filter and load data based on search query
        }
    }
}
