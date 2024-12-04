using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Equip
{
    public partial class AddEquipElectronics : Form
    {
        public event EventHandler RecordAdded;
        private int? _equipmentId; // Store the ID of the equipment being edited

        public AddEquipElectronics(int? equipmentId = null)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            _equipmentId = equipmentId;

            if (_equipmentId.HasValue)
            {
                LoadEquipmentDetails(_equipmentId.Value);
            }

            txtEquipment.TextChanged += FormatTextToCapitalizeFirstLetter;
            txtSpec.TextChanged += FormatTextToCapitalizeFirstLetter;
            txtLoc.TextChanged += FormatTextToCapitalizeFirstLetter;
            txtDonatedBy.TextChanged += FormatTextToCapitalizeFirstLetter;
            txtAmount.TextChanged += FormatTextToCapitalizeFirstLetter;
            txtEquipmentType.TextChanged += FormatTextToCapitalizeFirstLetter;
            txtAmount.KeyPress += txtAmount_KeyPress; // Allow only numbers
        }

        private void FormatTextToCapitalizeFirstLetter(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("SELECT EquipmentType, EquipmentName, Specification, Location, YearDonated, DonatedBy, Amount, YearAcquired, Status FROM ElectronicTbl WHERE PropertyNo = @Id", con);
                cmd.Parameters.AddWithValue("@Id", equipmentId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEquipment.Text = reader["EquipmentName"].ToString();
                    txtSpec.Text = reader["Specification"].ToString();
                    txtLoc.Text = reader["Location"].ToString();
                    dtpYearDonated.Value = Convert.ToDateTime(reader["YearDonated"]);
                    txtDonatedBy.Text = reader["DonatedBy"].ToString();
                    txtAmount.Text = reader["Amount"].ToString();
                    dtpYearAcquired.Value = Convert.ToDateTime(reader["YearAcquired"]);
                    txtEquipmentType.Text = reader["EquipmentType"].ToString(); // Set the EquipmentType
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
            if (string.IsNullOrWhiteSpace(txtEquipmentType.Text))
            {
                MessageBox.Show("Equipment Type cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEquipment.Text))
            {
                MessageBox.Show("Equipment Name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSpec.Text))
            {
                MessageBox.Show("Specification cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLoc.Text))
            {
                MessageBox.Show("Location cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDonatedBy.Text))
            {
                MessageBox.Show("Donor's name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
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

            // Check if Status is selected
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
                        cmd = new SqlCommand("UPDATE ElectronicTbl SET EquipmentName = @EquipmentName, Specification = @Specification, Location = @Location, YearDonated = @YearDonated, DonatedBy = @DonatedBy, Amount = @Amount, YearAcquired = @YearAcquired, EquipmentType = @EquipmentType, Status = @Status WHERE PropertyNo = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", _equipmentId.Value);
                    }
                    else
                    {
                        // Insert new record
                        cmd = new SqlCommand("INSERT INTO ElectronicTbl (EquipmentName, Specification, Location, YearDonated, DonatedBy, Amount, YearAcquired, EquipmentType, Status) VALUES (@EquipmentName, @Specification, @Location, @YearDonated, @DonatedBy, @Amount, @YearAcquired, @EquipmentType, @Status)", con);
                    }

                    cmd.Parameters.AddWithValue("@EquipmentType", txtEquipmentType.Text);
                    cmd.Parameters.AddWithValue("@EquipmentName", txtEquipment.Text);
                    cmd.Parameters.AddWithValue("@Specification", txtSpec.Text);
                    cmd.Parameters.AddWithValue("@Location", txtLoc.Text);
                    cmd.Parameters.AddWithValue("@YearDonated", dtpYearDonated.Value.Date);
                    cmd.Parameters.AddWithValue("@DonatedBy", txtDonatedBy.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@YearAcquired", dtpYearAcquired.Value.Date);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show(_equipmentId.HasValue ? "Successfully updated." : "Successfully added.");

                    OnRecordAdded(); // Raise the event to notify the parent form

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
        }


        private void PicExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set to Cancel to indicate the form was closed without saving
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear text fields
            txtEquipmentType.Clear(); // Clear the text box
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

        // Allow only numbers in the Amount field
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is not a digit and not a control character (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent the key from being entered into the text box
            }
        }

        
    }
}
