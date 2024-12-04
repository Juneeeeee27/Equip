using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Equip
{
    public partial class AddEquipTailoring : Form
    {
        private int? _equipmentId; // Store the ID of the equipment being edited

        public event EventHandler RecordAdded; // Added event declaration

        public AddEquipTailoring(int? equipmentId = null)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            _equipmentId = equipmentId;

            if (_equipmentId.HasValue)
            {
                LoadEquipmentDetails(_equipmentId.Value);
            }

            // Attach TextChanged events to ensure first letter is capitalized
            txtEquipmentType.TextChanged += CapitalizeFirstLetter;
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
                SqlCommand cmd = new SqlCommand("SELECT EquipmentType, EquipmentName, Specification, Location, YearDonated, DonatedBy, Amount, YearAcquired, Status FROM TailoringTbl WHERE PropertyNo = @Id", con); // Changed EquipmentNo to PropertyNo
                cmd.Parameters.AddWithValue("@Id", equipmentId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEquipment.Text = reader["EquipmentName"]?.ToString() ?? string.Empty;
                    txtSpec.Text = reader["Specification"]?.ToString() ?? string.Empty; // Added Specification
                    txtLoc.Text = reader["Location"]?.ToString() ?? string.Empty; // Added Location
                    txtAmount.Text = reader["Amount"]?.ToString() ?? string.Empty; // Added Amount
                    txtEquipmentType.Text = reader["EquipmentType"].ToString(); // Set the EquipmentType
                    cmbStatus.Text = reader["Status"].ToString();

                    // Ensure valid DateTime conversion
                    if (reader["YearDonated"] != DBNull.Value)
                        dtpYearDonated.Value = Convert.ToDateTime(reader["YearDonated"]);

                    txtDonatedBy.Text = reader["DonatedBy"]?.ToString() ?? string.Empty;

                    if (reader["YearAcquired"] != DBNull.Value)
                        dtpYearAcquired.Value = Convert.ToDateTime(reader["YearAcquired"]);
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
            if (string.IsNullOrWhiteSpace(txtEquipmentType.Text))
            {
                MessageBox.Show("Equipment Type Name cannot be empty.");
                return;
            }

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

            if (string.IsNullOrWhiteSpace(txtDonatedBy.Text))
            {
                MessageBox.Show("Donor's name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text)) // Added Amount validation
            {
                MessageBox.Show("Amount cannot be empty.");
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
                        cmd = new SqlCommand("UPDATE TailoringTbl SET EquipmentType = @EquipmentType, EquipmentName = @EquipmentName, Specification = @Specification, Location = @Location, YearDonated = @YearDonated, DonatedBy = @DonatedBy, Amount = @Amount, YearAcquired = @YearAcquired, Status = @Status WHERE PropertyNo = @Id", con); // Added Status
                        cmd.Parameters.AddWithValue("@Id", _equipmentId.Value);
                    }
                    else
                    {
                        // Insert new record
                        cmd = new SqlCommand("INSERT INTO TailoringTbl (EquipmentType, EquipmentName, Specification, Location, YearDonated, DonatedBy, Amount, YearAcquired, Status) VALUES (@EquipmentType, @EquipmentName, @Specification, @Location, @YearDonated, @DonatedBy, @Amount, @YearAcquired, @Status)", con); // Added Status
                    }

                    cmd.Parameters.AddWithValue("@EquipmentType", txtEquipmentType.Text);
                    cmd.Parameters.AddWithValue("@EquipmentName", txtEquipment.Text);
                    cmd.Parameters.AddWithValue("@Specification", txtSpec.Text); // Added Specification
                    cmd.Parameters.AddWithValue("@Location", txtLoc.Text); // Added Location
                    cmd.Parameters.AddWithValue("@YearDonated", dtpYearDonated.Value.Date);
                    cmd.Parameters.AddWithValue("@DonatedBy", txtDonatedBy.Text);
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
            txtEquipmentType.Clear(); // Clear the text box
            txtEquipment.Clear();
            txtSpec.Clear(); // Added Specification field reset
            txtLoc.Clear(); // Added Location field reset
            txtAmount.Clear(); // Added Amount field reset
            txtDonatedBy.Clear();
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

        private void AddEquipTailoring_Load(object sender, EventArgs e)
        {

        }


    }
}
