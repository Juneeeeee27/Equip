using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Equip
{
    public partial class AddEquipCulinary : Form
    {
        public event EventHandler RecordAdded;
        private int? _equipmentId; // Store the ID of the equipment being edited

        public AddEquipCulinary(int? equipmentId = null)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            _equipmentId = equipmentId;

            if (_equipmentId.HasValue)
            {
                LoadEquipmentDetails(_equipmentId.Value);
            }

            // Attach events to ensure first letter is capitalized
            txtEquipment.TextChanged += CapitalizeFirstLetter;
            txtSpec.TextChanged += CapitalizeFirstLetter;
            txtLoc.TextChanged += CapitalizeFirstLetter;
            txtDonatedBy.TextChanged += CapitalizeFirstLetter;
        }

        private void CapitalizeFirstLetter(object sender, EventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                string text = textBox.Text;
                textBox.Text = char.ToUpper(text[0]) + text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length; // Keep the cursor at the end
            }
        }



        private void LoadEquipmentDetails(int equipmentId)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EquipmentName, Specification, Location, Quantity, YearDonated, DonatedBy, Amount, YearAcquired, Status FROM CulinaryTbl WHERE PropertyNo = @Id", con); // Changed EquipmentNo to PropertyNo
                cmd.Parameters.AddWithValue("@Id", equipmentId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEquipment.Text = reader["EquipmentName"].ToString();
                    txtSpec.Text = reader["Specification"].ToString(); // Added Specification
                    txtLoc.Text = reader["Location"].ToString(); // Added Location
                    numQuantity.Value = Convert.ToDecimal(reader["Quantity"]);
                    dtpYearDonated.Value = Convert.ToDateTime(reader["YearDonated"]);
                    txtDonatedBy.Text = reader["DonatedBy"].ToString(); // Updated to DonatedBy
                    txtAmount.Text = reader["Amount"].ToString(); // Added Amount
                    dtpYearAcquired.Value = Convert.ToDateTime(reader["YearAcquired"]);
                    cmbStatus.Text = reader["Status"].ToString();
                }
                reader.Close();
            }
        }

        private void OnRecordAdded()
        {
            RecordAdded?.Invoke(this, EventArgs.Empty);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate if any field is empty
            if (string.IsNullOrWhiteSpace(txtEquipment.Text))
            {
                MessageBox.Show("Equipment Name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSpec.Text)) // Added Specification validation
            {
                MessageBox.Show("Specification cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLoc.Text)) // Added Location validation
            {
                MessageBox.Show("Location cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDonatedBy.Text)) // Updated to DonatedBy
            {
                MessageBox.Show("Donor's name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text)) // Added Amount validation
            {
                MessageBox.Show("Amount cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbStatus.Text)) // Added Amount validation
            {
                MessageBox.Show("Status cannot be empty.");
                return;
            }

            // Validate year inputs
            if (dtpYearDonated.Value > DateTime.Now)
            {
                MessageBox.Show("The donation year cannot be in the future.");
                return;
            }

            if (dtpYearAcquired.Value > DateTime.Now)
            {
                MessageBox.Show("The acquisition year cannot be in the future.");
                return;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Status cannot be empty.");
                return;
            }

            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd;

                    // Determine whether to insert a new record or update an existing one
                    if (_equipmentId.HasValue)
                    {
                        // Update existing record
                        cmd = new SqlCommand("UPDATE CulinaryTbl SET EquipmentName = @EquipmentName, Specification = @Specification, Location = @Location, Quantity = @Quantity, YearDonated = @YearDonated, DonatedBy = @DonatedBy, Amount = @Amount, YearAcquired = @YearAcquired, Status = @Status WHERE PropertyNo = @Id", con); // Added Status
                        cmd.Parameters.AddWithValue("@Id", _equipmentId.Value);
                    }
                    else
                    {
                        // Insert new record
                        cmd = new SqlCommand("INSERT INTO CulinaryTbl (EquipmentName, Specification, Location, Quantity, YearDonated, DonatedBy, Amount, YearAcquired, Status) VALUES (@EquipmentName, @Specification, @Location, @Quantity, @YearDonated, @DonatedBy, @Amount, @YearAcquired, @Status)", con); // Added Status
                    }

                    cmd.Parameters.AddWithValue("@EquipmentName", txtEquipment.Text);
                    cmd.Parameters.AddWithValue("@Specification", txtSpec.Text); // Added Specification
                    cmd.Parameters.AddWithValue("@Location", txtLoc.Text); // Added Location
                    cmd.Parameters.AddWithValue("@Quantity", (int)numQuantity.Value); // Ensure Quantity is cast to an int
                    cmd.Parameters.AddWithValue("@YearDonated", dtpYearDonated.Value.Date);
                    cmd.Parameters.AddWithValue("@DonatedBy", txtDonatedBy.Text); // Updated to DonatedBy
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text); // Added Amount
                    cmd.Parameters.AddWithValue("@YearAcquired", dtpYearAcquired.Value.Date);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString()); // Added Status parameter

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(_equipmentId.HasValue ? "Successfully updated." : "Successfully added.");

                    OnRecordAdded(); // Raise the event to notify the parent form

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear text fields
            txtEquipment.Clear();
            txtSpec.Clear();
            txtLoc.Clear();
            txtDonatedBy.Clear();
            txtAmount.Clear();
            cmbStatus.SelectedIndex = -1;

            // Reset date pickers to their default values
            dtpYearDonated.Value = DateTime.Now;
            dtpYearAcquired.Value = DateTime.Now;
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set to Cancel to indicate the form was closed without saving
            this.Close();
        }

        private void AddEquipCulinary_Load(object sender, EventArgs e)
        {

        }
    }
}
