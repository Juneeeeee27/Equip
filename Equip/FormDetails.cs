using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class FormDetails : Form
    {
        private string connectionString = @"Data Source=LAPTOP-VUEU29CI\SQLEXPRESS;Initial Catalog=CtuEquipDB;Integrated Security=True";

        public FormDetails(string selectedDate)
        {
            InitializeComponent();

            // Format and display the current date
            if (DateTime.TryParse(selectedDate, out DateTime parsedDate))
            {
                lblMonthDay.Text = parsedDate.ToString("MMMM d"); // Example: "December 1"

                // Get the next day
                DateTime nextDay = parsedDate.AddDays(1);
                string nextDayFormatted = nextDay.ToString("MMMM d"); // Example: "December 2"

                // You can display this nextDayFormatted somewhere or update other elements accordingly
                // Example: MessageBox.Show($"The next day is: {nextDayFormatted}");

                PopulateTimeSlots(); // Populate Time Slots
                LoadFilteredCategoriesAndEquipment(parsedDate); // Load filtered data
            }
            else
            {
                MessageBox.Show("Invalid date format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cmbCategory.SelectedIndexChanged += OnEquipmentChanged;
            cmbEquipmentName.SelectedIndexChanged += OnEquipmentChanged;
        }

        private void LoadFilteredCategoriesAndEquipment(DateTime selectedDate)
        {
            try
            {
                string query = @"
            SELECT DISTINCT Category, EquipmentName 
            FROM reservationtbl
            WHERE CAST(BorrowDate AS DATE) = @BorrowDate";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BorrowDate", selectedDate.Date);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var categories = new HashSet<string>();
                            var equipmentByCategory = new Dictionary<string, HashSet<string>>();

                            bool hasReservation = false; // Flag to track if there's any reservation for the day

                            // Group equipment by category
                            while (reader.Read())
                            {
                                string category = reader.GetString(0);
                                string equipmentName = reader.GetString(1);

                                if (!equipmentByCategory.ContainsKey(category))
                                {
                                    equipmentByCategory[category] = new HashSet<string>();
                                }

                                equipmentByCategory[category].Add(equipmentName);
                                categories.Add(category); // Add category to the list

                                hasReservation = true; // Mark that there's at least one reservation
                            }

                            // Populate categories in ComboBox
                            PopulateComboBox(cmbCategory, categories);

                            // Handle category change event
                            cmbCategory.SelectedIndexChanged += (sender, e) =>
                            {
                                // Clear the equipment ComboBox when a new category is selected
                                cmbEquipmentName.Items.Clear();
                                cmbEquipmentName.SelectedIndex = -1; // Clear selected equipment

                                // If "No Reserve" is selected, we do not need to update the equipment list
                                if (cmbCategory.SelectedItem?.ToString() == "No Reserve")
                                {
                                    return;
                                }

                                string selectedCategory = cmbCategory.SelectedItem?.ToString();
                                if (!string.IsNullOrEmpty(selectedCategory) && equipmentByCategory.ContainsKey(selectedCategory))
                                {
                                    // Populate the equipment ComboBox with items for the selected category
                                    PopulateComboBox(cmbEquipmentName, equipmentByCategory[selectedCategory]);
                                }
                            };

                            // If no reservations exist for this date, add "No Reserve" to both ComboBoxes
                            if (!hasReservation)
                            {
                                cmbCategory.Items.Add("No Reserve");
                                cmbCategory.SelectedIndex = 0; // Select "No Reserve" by default

                                cmbEquipmentName.Items.Add("No Reserve");
                                cmbEquipmentName.SelectedIndex = 0; // Select "No Reserve" by default
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void PopulateComboBox(ComboBox comboBox, IEnumerable<string> items)
        {
            comboBox.Items.Clear();
            foreach (var item in items)
            {
                comboBox.Items.Add(item);
            }
            comboBox.SelectedIndex = -1; // Prevent auto-selection of an item
        }


        private void OnEquipmentChanged(object sender, EventArgs e)
        {
            HighlightReservedSlots(); // Highlight reserved slots based on the selected equipment
        }

        private void PopulateTimeSlots()
        {
            tlpTimeSlot.ColumnCount = 2; // Morning and afternoon slots
            tlpTimeSlot.RowCount = 10;   // Ten rows for time slots
            tlpTimeSlot.ColumnStyles.Clear();
            tlpTimeSlot.RowStyles.Clear();

            tlpTimeSlot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Morning column
            tlpTimeSlot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Afternoon column

            for (int i = 1; i < tlpTimeSlot.RowCount; i++)
            {
                tlpTimeSlot.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            }

            string[] morningTimes = { "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM" };
            for (int i = 0; i < morningTimes.Length; i++)
            {
                var label = new Label()
                {
                    Text = morningTimes[i],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray
                };

                tlpTimeSlot.Controls.Add(label, 0, i); // First column
            }

            string[] afternoonTimes = { "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM",
                                        "6:00 PM", "7:00 PM", "8:00 PM", "9:00 PM", "10:00 PM" };
            for (int i = 0; i < afternoonTimes.Length; i++)
            {
                var label = new Label()
                {
                    Text = afternoonTimes[i],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray
                };

                tlpTimeSlot.Controls.Add(label, 1, i); // Second column
            }
        }

        private void HighlightReservedSlots()
        {
            if (cmbCategory.SelectedItem == null || cmbEquipmentName.SelectedItem == null)
                return;

            try
            {
                string category = cmbCategory.SelectedItem.ToString();
                string equipmentName = cmbEquipmentName.SelectedItem.ToString();

                string reservationQuery = @"
            SELECT BorrowTime, ReturnTime, CONCAT(Course, ' ', YearSection) AS Identifier
            FROM reservationtbl
            WHERE Category = @Category AND EquipmentName = @EquipmentName";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(reservationQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Category", category);
                        command.Parameters.AddWithValue("@EquipmentName", equipmentName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var reservedIntervals = new List<(TimeSpan borrow, TimeSpan returnTime, string identifier)>();

                            while (reader.Read())
                            {
                                var borrowTime = TimeSpan.FromMinutes(Math.Floor(reader.GetTimeSpan(0).TotalMinutes));
                                var returnTime = TimeSpan.FromMinutes(Math.Floor(reader.GetTimeSpan(1).TotalMinutes));
                                string identifier = reader.GetString(2); // Retrieve the identifier
                                reservedIntervals.Add((borrowTime, returnTime, identifier));
                            }

                            MarkReservedSlots(reservedIntervals);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reservations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void MarkReservedSlots(List<(TimeSpan borrow, TimeSpan returnTime, string identifier)> reservedIntervals)
        {
            var slotTimes = new List<(TimeSpan start, int column, int row, string timeLabel)>
    {
        (new TimeSpan(8, 0, 0), 0, 0, "8:00 AM"), (new TimeSpan(9, 0, 0), 0, 1, "9:00 AM"),
        (new TimeSpan(10, 0, 0), 0, 2, "10:00 AM"), (new TimeSpan(11, 0, 0), 0, 3, "11:00 AM"),
        (new TimeSpan(12, 0, 0), 0, 4, "12:00 PM"), (new TimeSpan(13, 0, 0), 1, 0, "1:00 PM"),
        (new TimeSpan(14, 0, 0), 1, 1, "2:00 PM"), (new TimeSpan(15, 0, 0), 1, 2, "3:00 PM"),
        (new TimeSpan(16, 0, 0), 1, 3, "4:00 PM"), (new TimeSpan(17, 0, 0), 1, 4, "5:00 PM"),
        (new TimeSpan(18, 0, 0), 1, 5, "6:00 PM"), (new TimeSpan(19, 0, 0), 1, 6, "7:00 PM"),
        (new TimeSpan(20, 0, 0), 1, 7, "8:00 PM"), (new TimeSpan(21, 0, 0), 1, 8, "9:00 PM"),
        (new TimeSpan(22, 0, 0), 1, 9, "10:00 PM")
    };

            // Initialize all labels with time and ensure they're visible
            foreach (Control control in tlpTimeSlot.Controls)
            {
                if (control is Label label)
                {
                    label.BackColor = Color.LightGray;
                    label.Text = ""; // Clear the text initially
                }
            }

            // Add all the time labels to the slots
            foreach (var (slotStart, column, row, timeLabel) in slotTimes)
            {
                var label = tlpTimeSlot.GetControlFromPosition(column, row) as Label;
                if (label != null)
                {
                    label.Text = timeLabel; // Ensure time is always displayed
                }
            }

            // Now mark reserved slots with the identifier
            foreach (var interval in reservedIntervals)
            {
                foreach (var (slotStart, column, row, timeLabel) in slotTimes)
                {
                    if (slotStart >= interval.borrow && slotStart <= interval.returnTime)
                    {
                        var label = tlpTimeSlot.GetControlFromPosition(column, row) as Label;
                        if (label != null)
                        {
                            label.BackColor = Color.Red; // Highlight reserved slot
                            label.Text = $"{timeLabel} - {interval.identifier}"; // Show time and identifier
                        }
                    }
                }
            }
        }



        private void PicExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
