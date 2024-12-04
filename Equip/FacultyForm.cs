using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Equip
{
    public partial class FacultyForm : Form
    {
        private int reservationId;
        private string connectionString = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True");
        private bool isOptionalVisible = false; // Track visibility of optional section

        public FacultyForm(int id)
        {
            InitializeComponent();
            reservationId = id;
            txtSchoolID.KeyPress += TxtSchoolID_KeyPress; // Attach event handler for SchoolID input
            LoadReservationData();

        }



        private void LoadReservationData()
        {
            if (reservationId > 0) // Only load if the ID is valid
            {
                con.Open();
                string query = "SELECT * FROM ReservationTbl WHERE ReservationId = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", reservationId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cmbCategory.Text = reader["Category"].ToString();
                    cmbEquipmentName.Text = reader["EquipmentName"].ToString();
                    txtSchoolID.Text = reader["SchoolID"].ToString();
                    txtName.Text = reader["Name"].ToString();
                    cmbCourse.Text = reader["Course"].ToString();
                    numQuantity.Value = Convert.ToDecimal(reader["Quantity"]);

                    DateTime borrowDate = Convert.ToDateTime(reader["BorrowDate"]).Date;
                    DateTime returnDate = Convert.ToDateTime(reader["ReturnDate"]).Date;
                    TimeSpan borrowTime = TimeSpan.Parse(reader["BorrowTime"].ToString());
                    TimeSpan returnTime = TimeSpan.Parse(reader["ReturnTime"].ToString());

                    if (borrowDate < DateTime.Now.Date)
                    {
                        MessageBox.Show("Borrow date cannot be in the past. It will be set to today's date.");
                        borrowDate = DateTime.Now.Date;
                    }
                    if (returnDate < DateTime.Now.Date)
                    {
                        MessageBox.Show("Return date cannot be in the past. It will be set to tomorrow's date.");
                        returnDate = DateTime.Now.Date.AddDays(1);
                    }

                    dtpBorrowDate.Value = borrowDate;
                    dtpReturnDate.Value = returnDate;
                    dtpBorrowTime.Value = DateTime.Now.Date + borrowTime;
                    dtpReturnTime.Value = DateTime.Now.Date + returnTime;
                }
                reader.Close();
                con.Close();
            }
        }

        private void TxtSchoolID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(cmbCategory.Text) ||
                    string.IsNullOrWhiteSpace(cmbEquipmentName.Text) ||
                    string.IsNullOrWhiteSpace(txtSchoolID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(cmbCourse.Text) ||
                    numQuantity.Value <= 0)
                {
                    MessageBox.Show("Please fill in all required fields.");
                    return;
                }

                // Format inputs
                string formattedCategory = CapitalizeFirstLetter(cmbCategory.Text);
                string formattedEquipmentName = CapitalizeFirstLetter(cmbEquipmentName.Text);
                DateTime borrowDateTime = dtpBorrowDate.Value.Date + dtpBorrowTime.Value.TimeOfDay;
                DateTime returnDateTime = dtpReturnDate.Value.Date + dtpReturnTime.Value.TimeOfDay;

                // Check if return time is later than borrow time
                if (returnDateTime <= borrowDateTime)
                {
                    MessageBox.Show("Return time must be later than borrow time.", "Invalid Time Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ((returnDateTime - borrowDateTime).TotalHours > 2)
                {
                    MessageBox.Show("Return time cannot exceed 2 hours from borrow time.", "Invalid Time Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                con.Open();

                // Check for conflicting reservations
                string conflictQuery = @"
        SELECT COUNT(*) 
        FROM ReservationTbl 
        WHERE EquipmentName = @EquipmentName
          AND BorrowDate = @BorrowDate
          AND ReturnDate = @ReturnDate
          AND NOT (
                @ReturnTime <= BorrowTime OR 
                @BorrowTime >= ReturnTime
          )
          AND (@ReservationId = 0 OR ReservationId != @ReservationId)";

                SqlCommand conflictCmd = new SqlCommand(conflictQuery, con);
                conflictCmd.Parameters.AddWithValue("@EquipmentName", formattedEquipmentName);
                conflictCmd.Parameters.AddWithValue("@BorrowDate", borrowDateTime.Date);
                conflictCmd.Parameters.AddWithValue("@ReturnDate", returnDateTime.Date);
                conflictCmd.Parameters.AddWithValue("@BorrowTime", borrowDateTime.TimeOfDay);
                conflictCmd.Parameters.AddWithValue("@ReturnTime", returnDateTime.TimeOfDay);
                conflictCmd.Parameters.AddWithValue("@ReservationId", reservationId);

                int conflictCount = (int)conflictCmd.ExecuteScalar();
                if (conflictCount > 0)
                {
                    MessageBox.Show("Conflict detected! The equipment is already reserved for the selected date and time.",
                        "Reservation Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with insert/update if no conflict
                string query;
                if (reservationId > 0) // Editing
                {
                    query = "UPDATE ReservationTbl SET Date = @Date, Category = @Category, EquipmentName = @EquipmentName, SchoolID = @SchoolID, Name = @Name, Course = @Course, Quantity = @Quantity, BorrowDate = @BorrowDate, BorrowTime = @BorrowTime, ReturnDate = @ReturnDate, ReturnTime = @ReturnTime WHERE ReservationId = @Id";
                }
                else // New reservation
                {
                    query = "INSERT INTO ReservationTbl (Date, Category, EquipmentName, SchoolID, Name, Course, YearSection, Position, Quantity, BorrowDate, BorrowTime, ReturnDate, ReturnTime) " +
                            "VALUES (@Date, @Category, @EquipmentName, @SchoolID, @Name, @Course, @YearSection, @Position, @Quantity, @BorrowDate, @BorrowTime, @ReturnDate, @ReturnTime)";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                // Add parameters
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                if (reservationId > 0)
                {
                    cmd.Parameters.AddWithValue("@Id", reservationId);
                }
                cmd.Parameters.AddWithValue("@Category", formattedCategory);
                cmd.Parameters.AddWithValue("@EquipmentName", formattedEquipmentName);
                cmd.Parameters.AddWithValue("@SchoolID", txtSchoolID.Text);
                cmd.Parameters.AddWithValue("@Name", CapitalizeFirstLetter(txtName.Text));
                cmd.Parameters.AddWithValue("@Course", cmbCourse.Text.ToUpper());
                cmd.Parameters.AddWithValue("@YearSection", DBNull.Value);
                cmd.Parameters.AddWithValue("@Position", "Faculty");
                cmd.Parameters.AddWithValue("@Quantity", (int)numQuantity.Value);
                cmd.Parameters.AddWithValue("@BorrowDate", borrowDateTime.Date);
                cmd.Parameters.AddWithValue("@BorrowTime", borrowDateTime.TimeOfDay);
                cmd.Parameters.AddWithValue("@ReturnDate", returnDateTime.Date);
                cmd.Parameters.AddWithValue("@ReturnTime", returnDateTime.TimeOfDay);

                cmd.ExecuteNonQuery();
                MessageBox.Show(reservationId > 0 ? "Reservation edited successfully." : "Reservation created successfully.");
                this.DialogResult = DialogResult.OK; // Indicate successful submission
                this.Close(); // Close the form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the equipment ComboBox before populating
            cmbEquipmentName.Items.Clear();

            // Dictionary to map categories to table names
            var categoryTableMap = new Dictionary<string, string>
    {
        { "Electronics", "ElectronicTbl" },
        { "Sports", "SportsTbl" },
        { "Culinary", "CulinaryTbl" },
        { "Tailoring", "TailoringTbl" }
    };

            // Get the selected category
            string selectedCategory = cmbCategory.SelectedItem?.ToString();

            // Check if the category has a corresponding table
            if (!categoryTableMap.TryGetValue(selectedCategory, out string tableName))
            {
                // Exit if no valid table is associated
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch available equipment
                    string query = $"SELECT EquipmentName FROM {tableName} WHERE Status = 'Available'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Add each available equipment name to the ComboBox
                        cmbEquipmentName.Items.Add(reader["EquipmentName"].ToString());
                    }

                    reader.Close();
                }

                // Show a message if no equipment is available
                if (cmbEquipmentName.Items.Count == 0)
                {
                    MessageBox.Show("No available equipment found for the selected category.", "No Equipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Optionally, set the first item as selected
                    cmbEquipmentName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching equipment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = -1;
            cmbEquipmentName.SelectedIndex = -1;
            txtSchoolID.Clear();
            txtName.Clear();
            cmbCourse.SelectedIndex = -1;
            numQuantity.Value = 0;
            dtpBorrowDate.Value = DateTime.Now.AddDays(1);
            dtpReturnDate.Value = DateTime.Now.AddDays(1);
            dtpBorrowTime.Value = DateTime.Now;
            dtpReturnTime.Value = DateTime.Now.AddHours(1);
        }


        private void DtpReturnTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime borrowDateTime = dtpBorrowDate.Value.Date + dtpBorrowTime.Value.TimeOfDay;
            DateTime returnDateTime = dtpReturnDate.Value.Date + dtpReturnTime.Value.TimeOfDay;

            if (returnDateTime <= borrowDateTime)
            {
                MessageBox.Show("Return time must be later than borrow time.", "Invalid Time Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpReturnTime.Value = borrowDateTime.AddHours(1); // Default to 1 hour later
            }
            else if ((returnDateTime - borrowDateTime).TotalHours > 2)
            {
                MessageBox.Show("Return time cannot exceed 2 hours from borrow time.", "Invalid Time Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpReturnTime.Value = borrowDateTime.AddHours(2); // Restrict to 2 hours later
            }
        }


        private void DtpBorrowDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpBorrowDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Borrow date cannot be in the past. Please select a valid date.", "Invalid Borrow Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBorrowDate.Value = DateTime.Now.Date; // Reset to today's date
            }

            // Ensure the return date matches the borrow date
            dtpReturnDate.Value = dtpBorrowDate.Value;
        }

        private void DtpReturnDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpReturnDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Return date cannot be in the past. Please select a valid date.", "Invalid Return Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpReturnDate.Value = DateTime.Now.Date; // Reset to today's date
            }
            else if (dtpReturnDate.Value.Date != dtpBorrowDate.Value.Date)
            {
                MessageBox.Show("The return date must be the same as the borrow date. Please select a valid date.", "Invalid Return Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpReturnDate.Value = dtpBorrowDate.Value; // Reset to borrow date
            }
        }

        private void DtpTime_ValueChanged(object sender, EventArgs e)
        {
            // Restrict the selected time range for both Borrow and Return times
            if (sender is DateTimePicker dtp && dtp.Value < DateTime.Today.AddHours(8))
            {
                dtp.Value = DateTime.Today.AddHours(8); // Set to 8 AM if before
            }
            else if (sender is DateTimePicker dtp2 && dtp2.Value > DateTime.Today.AddHours(22))
            {
                dtp2.Value = DateTime.Today.AddHours(22); // Set to 10 PM if after
            }
        }

        private void FacultyForm_Load(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;

            lblIsUrgent.Visible = false;
            rdoYes.Visible = false;
            rdoNo.Visible = false;

            // Adjust "Clear" and "Submit" buttons to initial positions
            btnClear.Top = lblOptional.Top + lblOptional.Height + 10;
            btnSubmit.Top = btnClear.Top;

            dtpBorrowDate.ValueChanged += DtpBorrowDate_ValueChanged;
            dtpReturnDate.ValueChanged += DtpReturnDate_ValueChanged;
            dtpReturnDate.Value = dtpBorrowDate.Value; // Initialize return date to borrow date

            // Initialize time pickers to restrict times between 8 AM and 10 PM
            dtpBorrowTime.Format = DateTimePickerFormat.Custom;
            dtpBorrowTime.CustomFormat = "hh:mm tt";
            dtpBorrowTime.ShowUpDown = true;
            dtpBorrowTime.MinDate = DateTime.Today.AddHours(8); // 8 AM
            dtpBorrowTime.MaxDate = DateTime.Today.AddHours(22); // 10 PM

            dtpReturnTime.Format = DateTimePickerFormat.Custom;
            dtpReturnTime.CustomFormat = "hh:mm tt";
            dtpReturnTime.ShowUpDown = true;
            dtpReturnTime.MinDate = DateTime.Today.AddHours(8); // 8 AM
            dtpReturnTime.MaxDate = DateTime.Today.AddHours(22); // 10 PM

            // Attach event handlers for value changes
            dtpBorrowTime.ValueChanged += DtpTime_ValueChanged;
            dtpReturnTime.ValueChanged += DtpTime_ValueChanged;

            // Set the initial values to be within the range
            dtpBorrowTime.Value = DateTime.Today.AddHours(8); // Default to 8 AM
            dtpReturnTime.Value = DateTime.Today.AddHours(9); // Default to 9 AM
        }

        private void lblOptional_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the "Is this urgent?" section
            isOptionalVisible = !isOptionalVisible;

            lblIsUrgent.Visible = isOptionalVisible;
            rdoYes.Visible = isOptionalVisible;
            rdoNo.Visible = isOptionalVisible;

            // Adjust the form height and button positions based on visibility
            if (isOptionalVisible)
            {
                // Calculate the additional height needed for the "Is this urgent?" section
                int additionalSpace = lblIsUrgent.Height + 20; // Include some padding
                this.Height += additionalSpace; // Increase form height

                // Adjust button positions below the "Is this urgent?" section
                btnClear.Top = lblIsUrgent.Top + lblIsUrgent.Height + 10;
                btnSubmit.Top = btnClear.Top;
            }
            else
            {
                // Calculate the height to reduce when collapsing the "Is this urgent?" section
                int additionalSpace = lblIsUrgent.Height + 20; // Include padding
                this.Height -= additionalSpace; // Decrease form height

                // Reset button positions below the "Optional" label
                btnClear.Top = lblOptional.Top + lblOptional.Height + 10;
                btnSubmit.Top = btnClear.Top;
            }
        }
    }
}
