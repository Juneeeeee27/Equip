using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Equip
{
    public partial class AddEquipSports : Form
    {
        // Declare an event to notify when a record is added
        public event EventHandler RecordAdded;

        // Store the equipment ID if editing, null if adding a new record
        private int? _equipmentId;

        // Constructor - correct class name
        public AddEquipSports(int? equipmentId = null)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            _equipmentId = equipmentId;

            if (_equipmentId.HasValue)
            {
                LoadSportsDetails(_equipmentId.Value);
            }

            // Attach events to ensure first letter is capitalized
            txtEquipment.TextChanged += CapitalizeFirstLetter;
            txtSpec.TextChanged += CapitalizeFirstLetter;
            txtLoc.TextChanged += CapitalizeFirstLetter;
            txtDonatedBy.TextChanged += CapitalizeFirstLetter;
            txtAmount.TextChanged += CapitalizeFirstLetter;

            // Allow only numbers in txtAmount
            txtAmount.KeyPress += txtAmount_KeyPress;
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


        private void PicExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Set to Cancel to indicate the form was closed without saving
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validate if any field is empty
            if (string.IsNullOrWhiteSpace(txtEquipment.Text))
            {
                MessageBox.Show("Equipment Name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDonatedBy.Text))  // Changed from WhoDonated to DonatedBy
            {
                MessageBox.Show("Donor's name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSpec.Text))  // Added Specification check
            {
                MessageBox.Show("Specification cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLoc.Text))  // Added Location check
            {
                MessageBox.Show("Location cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbStatus.Text)) // Added Amount validation
            {
                MessageBox.Show("Quantity cannot be empty.");
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
                        cmd = new SqlCommand("UPDATE SportsTbl SET EquipmentName = @EquipmentName, Specification = @Specification, Location = @Location, Quantity = @Quantity, YearDonated = @YearDonated, DonatedBy = @DonatedBy, Amount = @Amount, YearAcquired = @YearAcquired, Status = @Status WHERE PropertyNo = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", _equipmentId.Value);
                    }
                    else
                    {
                        // Insert new record
                        cmd = new SqlCommand("INSERT INTO SportsTbl (EquipmentName, Specification, Location, Quantity, YearDonated, DonatedBy, Amount, YearAcquired, Status) VALUES (@EquipmentName, @Specification, @Location, @Quantity, @YearDonated, @DonatedBy, @Amount, @YearAcquired, @Status)", con);
                    }

                    // Adding the parameters to the command
                    cmd.Parameters.AddWithValue("@EquipmentName", txtEquipment.Text);
                    cmd.Parameters.AddWithValue("@Specification", txtSpec.Text);
                    cmd.Parameters.AddWithValue("@Location", txtLoc.Text);
                    cmd.Parameters.AddWithValue("@Quantity", (int)numQuantity.Value);
                    cmd.Parameters.AddWithValue("@YearDonated", dtpYearDonated.Value.Date);
                    cmd.Parameters.AddWithValue("@DonatedBy", txtDonatedBy.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@YearAcquired", dtpYearAcquired.Value.Date);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString()); // Assuming the ComboBox holds strings


                    // Execute the command
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear text fields
            txtEquipment.Clear();
            txtSpec.Clear();  // Clear Specification field
            txtLoc.Clear();  // Clear Location field
            txtDonatedBy.Clear();
            cmbStatus.SelectedIndex = -1;

            // Reset date pickers to their default values
            dtpYearDonated.Value = DateTime.Now;
            dtpYearAcquired.Value = DateTime.Now;
            txtAmount.Clear();  // Clear Amount field
        }

        ///////////// DATABASE

        private void OnRecordAdded()
        {
            RecordAdded?.Invoke(this, EventArgs.Empty);
        }

        private void LoadSportsDetails(int equipmentId)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EquipmentName, Specification, Location, YearDonated, Quantity, DonatedBy, Amount, YearAcquired, Status FROM SportsTbl WHERE PropertyNo = @Id", con);  // Changed EquipmentNo to PropertyNo
                cmd.Parameters.AddWithValue("@Id", equipmentId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtEquipment.Text = reader["EquipmentName"].ToString();
                    txtSpec.Text = reader["Specification"].ToString();  // Load Specification
                    txtLoc.Text = reader["Location"].ToString();  // Load Location
                    numQuantity.Value = Convert.ToDecimal(reader["Quantity"]);
                    dtpYearDonated.Value = Convert.ToDateTime(reader["YearDonated"]);
                    txtDonatedBy.Text = reader["DonatedBy"].ToString();
                    txtAmount.Text = reader["Amount"].ToString();  // Load Amount
                    dtpYearAcquired.Value = Convert.ToDateTime(reader["YearAcquired"]);
                    cmbStatus.Text = reader["Status"].ToString();
                }
                reader.Close();
            }
        }

        // Allow only numbers in the Amount field (if applicable)
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is not a digit and not a control character (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent the key from being entered into the text box
            }
        }

        private void AddEquipSports_Load(object sender, EventArgs e)
        {

        }
    }
}
