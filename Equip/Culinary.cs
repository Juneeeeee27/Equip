using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Equip.Controls;

namespace Equip
{
    public partial class Culinary : Form
    {
        private Functions _functions;
                private ContextMenuStrip contextMenu; // Right-click menu

        public Culinary()
        {
            InitializeComponent();
            _functions = new Functions();
            LoadCulinaryData();  // Load all data initially
            StyleDataGridView(dgvCulinary);  // Apply DataGridView styling
            InitializeContextMenu(); // Initialize context menu
        }

        private void InitializeContextMenu()
        {
            // Create the context menu
            contextMenu = new ContextMenuStrip();
            var decommissionItem = new ToolStripMenuItem("Decommission");
            decommissionItem.Click += DecommissionItem_Click; // Event handler for decommission action
            contextMenu.Items.Add(decommissionItem);

            // Attach the context menu to the DataGridView
            dgvCulinary.ContextMenuStrip = contextMenu;
        }

        private void DecommissionItem_Click(object sender, EventArgs e)
        {
            if (dgvCulinary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to decommission.");
                return;
            }

            DataGridViewRow selectedRow = dgvCulinary.SelectedRows[0];
            int propertyNo = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);
            string equipmentName = selectedRow.Cells["EquipmentName"].Value.ToString();
            string specification = selectedRow.Cells["Specification"].Value?.ToString();
            string location = selectedRow.Cells["Location"].Value?.ToString();
            object quantity = selectedRow.Cells["Quantity"].Value ?? DBNull.Value;
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
                        string insertQuery = @"
                   INSERT INTO DecommissionedTbl
                    (PropertyNo, EquipmentName, Specification, Location, Quantity, YearDonated, DonatedBy, Amount, YearAcquired)
                    VALUES 
                    (@PropertyNo, @EquipmentName, @Specification, @Location, @Quantity, @YearDonated, @DonatedBy, @Amount, @YearAcquired)";

                        SqlCommand cmd = new SqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@PropertyNo", propertyNo);
                        cmd.Parameters.AddWithValue("@EquipmentName", equipmentName);
                        cmd.Parameters.AddWithValue("@Specification", (object)specification ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Quantity", quantity); // Set to DBNull.Value if null
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
                        string deleteQuery = "DELETE FROM CulinaryTbl WHERE PropertyNo = @PropertyNo";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                        deleteCmd.Parameters.AddWithValue("@PropertyNo", propertyNo);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Refresh the DataGridView
                    LoadCulinaryData();
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
                var hitTest = dgvCulinary.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dgvCulinary.ClearSelection();
                    dgvCulinary.Rows[hitTest.RowIndex].Selected = true;
                    contextMenu.Show(dgvCulinary, e.Location);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCulinary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            DataGridViewRow selectedRow = dgvCulinary.SelectedRows[0];
            int selectedRowId = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM CulinaryTbl WHERE PropertyNo = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", selectedRowId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record deleted successfully.");
                    LoadCulinaryData();  // Reload data after deletion
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
            AddEquipCulinary addEquipCulinary = new AddEquipCulinary();
            addEquipCulinary.RecordAdded += AddEquip_RecordAdded;
            DialogResult result = addEquipCulinary.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadCulinaryData();  // Reload data after adding new equipment
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCulinary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.");
                return;
            }

            DataGridViewRow selectedRow = dgvCulinary.SelectedRows[0];
            int selectedRowId = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);

            AddEquipCulinary addEquipCulinary = new AddEquipCulinary(selectedRowId);
            addEquipCulinary.RecordAdded += AddEquip_RecordAdded;
            addEquipCulinary.ShowDialog();
        }

        private void LoadCulinaryData(string filter = "")
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT * FROM CulinaryTbl";
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
                dgvCulinary.DataSource = table;  // Bind filtered data to DataGridView
            }
        }

        private void Culinary_Load(object sender, EventArgs e)
        {
            LoadCulinaryData();  // Load all data initially
            StyleDataGridView(dgvCulinary);  // Apply styling on load
            dgvCulinary.CellFormatting += dgvCulinary_CellFormatting;
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

        private void dgvCulinary_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCulinary.Columns[e.ColumnIndex].HeaderText == "YearDonated" ||
                dgvCulinary.Columns[e.ColumnIndex].HeaderText == "YearAcquired")
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
            LoadCulinaryData();  // Reload data after adding or editing equipment
        }

        private void dgvCulinary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks, or remove this if not necessary
        }

        // Event handler for search functionality
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text;  // Get the current search text
            LoadCulinaryData(searchQuery);  // Filter and load data based on search query
        }
    }
}
