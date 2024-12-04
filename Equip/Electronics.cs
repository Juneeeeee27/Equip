using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Equip.Controls;

namespace Equip
{
    public partial class Electronics : Form
    {
        private Functions _functions;
        private ContextMenuStrip contextMenu; // Right-click menu

        public Electronics()
        {
            InitializeComponent();
            _functions = new Functions();
            LoadElectronicData();
            StyleDataGridView(dgvElectronic);  // Apply consistent styling
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
            dgvElectronic.ContextMenuStrip = contextMenu;
        }

        private void DecommissionItem_Click(object sender, EventArgs e)
        {
            if (dgvElectronic.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to decommission.");
                return;
            }

            DataGridViewRow selectedRow = dgvElectronic.SelectedRows[0];
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
                        string deleteQuery = "DELETE FROM ElectronicTbl WHERE PropertyNo = @PropertyNo";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                        deleteCmd.Parameters.AddWithValue("@PropertyNo", propertyNo);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Refresh the DataGridView
                    LoadElectronicData();
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
                var hitTest = dgvElectronic.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dgvElectronic.ClearSelection();
                    dgvElectronic.Rows[hitTest.RowIndex].Selected = true;
                    contextMenu.Show(dgvElectronic, e.Location);
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvElectronic.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            DataGridViewRow selectedRow = dgvElectronic.SelectedRows[0];
            int selectedRowId = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
                    {
                        con.Open();
                        int currentId = selectedRowId;

                        SqlCommand cmd = new SqlCommand("DELETE FROM ElectronicTbl WHERE PropertyNo = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", selectedRowId);
                        cmd.ExecuteNonQuery();

                        SqlCommand reseedCmd = new SqlCommand("DBCC CHECKIDENT('ElectronicTbl', RESEED, @Id)", con);
                        reseedCmd.Parameters.AddWithValue("@Id", currentId);
                        reseedCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Record deleted successfully.");
                    LoadElectronicData();
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
            AddEquipElectronics addEquip = new AddEquipElectronics();
            addEquip.RecordAdded += AddEquip_RecordAdded;

            DialogResult result = addEquip.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadElectronicData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvElectronic.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.");
                return;
            }

            DataGridViewRow selectedRow = dgvElectronic.SelectedRows[0];
            int selectedRowId = Convert.ToInt32(selectedRow.Cells["PropertyNo"].Value);

            AddEquipElectronics addEquip = new AddEquipElectronics(selectedRowId);
            addEquip.RecordAdded += AddEquip_RecordAdded;

            addEquip.ShowDialog();
        }

        private void LoadElectronicData(string searchQuery = null)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd;

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    cmd = new SqlCommand("SELECT * FROM ElectronicTbl WHERE PropertyNo LIKE @Filter OR EquipmentName LIKE @Filter OR Specification LIKE @Filter OR Location LIKE @Filter", con);
                    cmd.Parameters.AddWithValue("@Filter", "%" + searchQuery + "%");
                }
                else
                {
                    cmd = new SqlCommand("SELECT * FROM ElectronicTbl", con);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dgvElectronic.DataSource = table;
            }
        }

        private void Electronics_Load(object sender, EventArgs e)
        {
            LoadElectronicData();
            StyleDataGridView(dgvElectronic);
            dgvElectronic.CellFormatting += dgvElectronic_CellFormatting;
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

        private void dgvElectronic_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvElectronic.Columns[e.ColumnIndex].HeaderText == "YearDonated" ||
                dgvElectronic.Columns[e.ColumnIndex].HeaderText == "YearAcquired")
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
            LoadElectronicData();
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadElectronicData(txtSearch.Text);
        }
    }
}
