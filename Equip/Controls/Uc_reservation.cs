using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_reservation : UserControl
    {
        private string connectionString = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";
        private Timer refreshTimer;
        private Timer selectionClearTimer; // Timer for clearing selection after 10 seconds
        private bool isRefreshing = false;  // Flag to track refresh status
        private ContextMenuStrip contextMenuReservation;

        public Uc_reservation()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeSelectionClearTimer(); // Initialize the selection clear timer
            InitializeContextMenu(); // Initialize the context menu

            dgvReservation.CellFormatting += dgvReservation_CellFormatting;
            dgvReservation.SelectionChanged += dgvReservation_SelectionChanged; // Event handler for selection change
        }

        private void InitializeContextMenu()
        {
            // Create the ContextMenuStrip
            contextMenuReservation = new ContextMenuStrip();

            // Create and add menu items
            ToolStripMenuItem menuChangeStatus = new ToolStripMenuItem("Change Status");
            menuChangeStatus.Click += MenuChangeStatus_Click;

            ToolStripMenuItem menuCancel = new ToolStripMenuItem("Cancel");
            menuCancel.Click += MenuCancel_Click;

            contextMenuReservation.Items.AddRange(new ToolStripItem[]
            {
                menuChangeStatus,
                menuCancel,
            });

            // Attach the context menu to the DataGridView
            dgvReservation.ContextMenuStrip = contextMenuReservation;
        }

        private void dgvReservation_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Show the context menu only if a row is selected
                DataGridView.HitTestInfo hit = dgvReservation.HitTest(e.X, e.Y);

                if (hit.RowIndex >= 0)
                {
                    dgvReservation.ClearSelection();
                    dgvReservation.Rows[hit.RowIndex].Selected = true;
                    contextMenuReservation.Show(dgvReservation, new Point(e.X, e.Y));
                }
            }
        }

        private void MenuChangeStatus_Click(object sender, EventArgs e)
        {
            
        }

        private void MenuCancel_Click(object sender, EventArgs e)
        {
            
        }

   

        private void Uc_reservation_Load(object sender, EventArgs e)
        {
            LoadData(); // Load all data initially
        }

        private void InitializeTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 5000; // Set the interval to 1000 milliseconds (1 second)
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void InitializeSelectionClearTimer()
        {
            selectionClearTimer = new Timer();
            selectionClearTimer.Interval = 10000; // 10 seconds
            selectionClearTimer.Tick += SelectionClearTimer_Tick;
        }

        private void dgvReservation_SelectionChanged(object sender, EventArgs e)
        {
            // Restart the timer every time the selection changes, but only if it's not refreshing
            if (!isRefreshing)
            {
                selectionClearTimer.Stop();  // Stop the existing timer
                selectionClearTimer.Start(); // Start the timer again
            }
        }

        private void SelectionClearTimer_Tick(object sender, EventArgs e)
        {
            // Clear the selection after 10 seconds, but only if it's not refreshing
            if (!isRefreshing)
            {
                dgvReservation.ClearSelection();
                selectionClearTimer.Stop(); // Stop the timer after clearing the selection
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!isRefreshing) // Only refresh if not already refreshing
            {
                isRefreshing = true;
                LoadData(); // Refresh the DataGridView periodically
                isRefreshing = false;
            }
        }

        private void LoadData(string searchQuery = "")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Build the SQL query with a condition for the search query
                    string sql = "SELECT * FROM ReservationTbl";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        sql += " WHERE Name LIKE @SearchQuery OR EquipmentName LIKE @SearchQuery OR Category LIKE @SearchQuery OR Position LIKE @SearchQuery";
                    }

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dgvReservation.DataSource = dataTable;

                    foreach (DataGridViewRow row in dgvReservation.Rows)
                    {
                        if (row.Cells["ReturnDate"].Value != null && row.Cells["ReturnTime"].Value != null)
                        {
                            DateTime returnDate;
                            DateTime returnTime;

                            bool isReturnDateValid = DateTime.TryParse(row.Cells["ReturnDate"].Value.ToString(), out returnDate);
                            bool isReturnTimeValid = DateTime.TryParse(row.Cells["ReturnTime"].Value.ToString(), out returnTime);

                            if (isReturnDateValid && isReturnTimeValid)
                            {
                                DateTime fullReturnDateTime = returnDate.Date.Add(returnTime.TimeOfDay);

                                if (fullReturnDateTime < DateTime.Now)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;
                                }
                                else
                                {
                                    row.DefaultCellStyle.BackColor = Color.White;
                                }
                            }
                        }
                    }

                    ClearSelection(); // Clear selection after deleting
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    isRefreshing = false; // Reset the flag after refreshing is complete
                }
            }
        }


        private void dgvReservation_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Ensure the DataGridView has rows and the column exists
            if (dgvReservation.Columns[e.ColumnIndex].Name == "BorrowDate" || dgvReservation.Columns[e.ColumnIndex].Name == "ReturnDate")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                {
                    e.Value = dateValue.ToString("MM/dd/yyyy"); // Format to MM/dd/yyyy
                    e.FormattingApplied = true;
                }
            }
            else if (dgvReservation.Columns[e.ColumnIndex].Name == "BorrowTime" || dgvReservation.Columns[e.ColumnIndex].Name == "ReturnTime")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime timeValue))
                {
                    e.Value = timeValue.ToString("hh:mm tt"); // Format to hh:mm tt
                    e.FormattingApplied = true;
                }
            }
            else if (dgvReservation.Columns[e.ColumnIndex].Name == "Date") // Additional logic for Date column
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                {
                    e.Value = dateValue.ToString("MM/dd/yyyy hh:mm tt"); // Desired format
                    e.FormattingApplied = true;
                }
            }
        }



        // Event handler for the search TextBox's TextChanged event
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim(); // Get the search query
            LoadData(searchQuery); // Reload data with the search query
        }

        // Button click to create a reservation
        private void btnCreate_Click(object sender, EventArgs e)
        {
            SelectPosition selectPosition = new SelectPosition();
            selectPosition.ShowDialog();
        }

        // Button click to edit a reservation
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReservation.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvReservation.SelectedRows[0];

                // Safely access the ReservationId and Position columns
                var reservationIdValue = selectedRow.Cells["ReservationId"].Value?.ToString();

                if (!string.IsNullOrEmpty(reservationIdValue) && int.TryParse(reservationIdValue, out int id))
                {
                    string position = selectedRow.Cells["Position"].Value?.ToString();

                    if (position == "Student")
                    {
                        StudentForm studentForm = new StudentForm(id);
                        studentForm.ShowDialog();
                    }
                    else if (position == "Faculty")
                    {
                        FacultyForm facultyForm = new FacultyForm(id);
                        facultyForm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid reservation ID format.");
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to edit.");
            }
        }

        // Button click to delete a reservation
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvReservation.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvReservation.SelectedRows[0];

                // Safely access the ReservationId column
                var reservationIdValue = selectedRow.Cells["ReservationId"].Value?.ToString();

                if (!string.IsNullOrEmpty(reservationIdValue) && int.TryParse(reservationIdValue, out int id))
                {
                    DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete the reservation with ID {id}?", "Confirm Deletion", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DeleteReservation(id);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid reservation ID format.");
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to delete.");
            }
        }

        // Function to delete a reservation (if needed)
        private void DeleteReservation(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM ReservationTbl WHERE ReservationId = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    LoadData();
                    ClearSelection(); // Clear the selection after deleting
                    MessageBox.Show("Reservation deleted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Method to clear the selection in the DataGridView
        private void ClearSelection()
        {
            dgvReservation.ClearSelection(); // Clears any row or cell selection in the DataGridView
        }


        // Return button click event to move reservation to history
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvReservation.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvReservation.SelectedRows[0];

                // Safely access the ReservationId column
                var reservationIdValue = selectedRow.Cells["ReservationId"].Value?.ToString();

                if (!string.IsNullOrEmpty(reservationIdValue) && int.TryParse(reservationIdValue, out int id))
                {
                    // Show a message box to confirm the condition of the item
                    DialogResult result = MessageBox.Show(
                        "Is the returned item in good condition?",
                        "Item Condition Confirmation",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Move to history with "Good Condition" status
                        MoveToHistory(id, "Good Condition");
                    }
                    else if (result == DialogResult.No)
                    {
                        // Move to history with "Damaged" status
                        MoveToHistory(id, "Damaged");
                    }
                    // If Cancel, do nothing
                }
                else
                {
                    MessageBox.Show("Invalid reservation ID format.");
                }
            }
            else
            {
                MessageBox.Show("Please select a reservation to return.");
            }
        }

        // Method to move reservation to history
        private void MoveToHistory(int reservationId, string status)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Select the reservation details including BorrowTime and ReturnTime
                    SqlCommand cmdSelect = new SqlCommand(
                        "SELECT ReservationId, Name, EquipmentName, BorrowDate, ReturnDate, BorrowTime, ReturnTime, Category, SchoolID, Position, SubjectTeacher, Quantity, YearSection, Course " +
                        "FROM ReservationTbl WHERE ReservationId = @ReservationId", con);
                    cmdSelect.Parameters.AddWithValue("@ReservationId", reservationId);

                    SqlDataReader reader = cmdSelect.ExecuteReader();

                    if (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ReservationId"]);
                        string name = reader["Name"].ToString();
                        string equipmentName = reader["EquipmentName"].ToString();
                        DateTime borrowDate = Convert.ToDateTime(reader["BorrowDate"]);
                        DateTime returnDate = Convert.ToDateTime(reader["ReturnDate"]);
                        TimeSpan borrowTime = (TimeSpan)reader["BorrowTime"];
                        TimeSpan returnTime = (TimeSpan)reader["ReturnTime"];
                        string category = reader["Category"].ToString();
                        string schoolID = reader["SchoolID"].ToString();
                        string position = reader["Position"].ToString();
                        string subjectTeacher = reader["SubjectTeacher"].ToString();
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        string yearSection = reader["YearSection"].ToString();
                        string course = reader["Course"].ToString();
                        DateTime dateMovedToHistory = DateTime.Now;

                        reader.Close();

                        // Insert the reservation details into ReservationHistoryTbl
                        SqlCommand cmdInsert = new SqlCommand(
                            "INSERT INTO ReservationHistoryTbl (ReservationId, Name, EquipmentName, BorrowDate, ReturnDate, BorrowTime, ReturnTime, Category, SchoolID, Position, SubjectTeacher, Quantity, YearSection, Course, Status, DateMovedToHistory) " +
                            "VALUES (@ReservationId, @Name, @EquipmentName, @BorrowDate, @ReturnDate, @BorrowTime, @ReturnTime, @Category, @SchoolID, @Position, @SubjectTeacher, @Quantity, @YearSection, @Course, @Status, @DateMovedToHistory)", con);
                        cmdInsert.Parameters.AddWithValue("@ReservationId", id);
                        cmdInsert.Parameters.AddWithValue("@Name", name);
                        cmdInsert.Parameters.AddWithValue("@EquipmentName", equipmentName);
                        cmdInsert.Parameters.AddWithValue("@BorrowDate", borrowDate.ToString("MM/dd/yyyy"));
                        cmdInsert.Parameters.AddWithValue("@ReturnDate", returnDate.ToString("MM/dd/yyyy"));
                        cmdInsert.Parameters.AddWithValue("@BorrowTime", borrowTime.ToString(@"hh\:mm"));
                        cmdInsert.Parameters.AddWithValue("@ReturnTime", returnTime.ToString(@"hh\:mm"));
                        cmdInsert.Parameters.AddWithValue("@Category", category);
                        cmdInsert.Parameters.AddWithValue("@SchoolID", schoolID);
                        cmdInsert.Parameters.AddWithValue("@Position", position);
                        cmdInsert.Parameters.AddWithValue("@SubjectTeacher", subjectTeacher);
                        cmdInsert.Parameters.AddWithValue("@Quantity", quantity);
                        cmdInsert.Parameters.AddWithValue("@YearSection", yearSection);
                        cmdInsert.Parameters.AddWithValue("@Course", course);
                        cmdInsert.Parameters.AddWithValue("@Status", status);
                        cmdInsert.Parameters.AddWithValue("@DateMovedToHistory", dateMovedToHistory);

                        cmdInsert.ExecuteNonQuery();

                        // Delete the reservation from the original table
                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM ReservationTbl WHERE ReservationId = @ReservationId", con);
                        cmdDelete.Parameters.AddWithValue("@ReservationId", reservationId);
                        cmdDelete.ExecuteNonQuery();

                        MessageBox.Show("Reservation moved to history successfully.");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Reservation not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


    }
}
