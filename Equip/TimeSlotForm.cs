using System;
using System.Drawing;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class TimeSlotForm : Form
    {
        public TimeSlotForm()
        {
            InitializeComponent(); // Ensure the designer file has this method
            CreateTimeSlots();
        }

        private void CreateTimeSlots()
        {
            // Create a TableLayoutPanel with 12 rows and 2 columns
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                RowCount = 12,  // 12 rows for both columns
                ColumnCount = 2,  // 2 columns (one for each time period)
                AutoSize = true,
                Padding = new Padding(5)
            };

            // Define the row heights and column widths
            for (int i = 0; i < 12; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 12));  // Evenly distribute rows
            }

            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f)); // First column width (50%)
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f)); // Second column width (50%)

            // First column (8 AM to 12 PM) - 5 slots, fill 7 empty slots to make 12
            for (int i = 8; i <= 12; i++)
            {
                var panel = new Panel
                {
                    BackColor = Color.LightGray, // Background color for the box
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5)
                };

                var label = new Label
                {
                    Text = $"{i}:00 AM",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    ForeColor = Color.Black
                };

                panel.Controls.Add(label);
                tableLayout.Controls.Add(panel, 0, i - 8); // Column 0, Rows 0 to 4
            }

            // Fill the remaining rows (7 empty rows) in the first column
            for (int i = 5; i < 12; i++)
            {
                var panel = new Panel
                {
                    BackColor = Color.Transparent, // Empty rows, no background
                    BorderStyle = BorderStyle.FixedSingle
                };

                tableLayout.Controls.Add(panel, 0, i); // First column, Rows 5 to 11
            }

            // Second column (1 PM to 10 PM) - 10 slots, fill 2 empty slots to make 12
            for (int i = 1; i <= 10; i++)
            {
                var panel = new Panel
                {
                    BackColor = Color.LightGray, // Background color for the box
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(5)
                };

                var label = new Label
                {
                    Text = $"{i}:00 PM",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    ForeColor = Color.Black
                };

                panel.Controls.Add(label);
                tableLayout.Controls.Add(panel, 1, i - 1); // Second column, Rows 0 to 9
            }

            // Fill the remaining empty rows (2 empty rows) in the second column
            for (int i = 10; i < 12; i++)
            {
                var panel = new Panel
                {
                    BackColor = Color.Transparent, // Empty rows, no background
                    BorderStyle = BorderStyle.FixedSingle
                };

                tableLayout.Controls.Add(panel, 1, i); // Second column, Rows 10 to 11
            }

            // Add the TableLayoutPanel to the form
            Controls.Add(tableLayout);
        }
    }
}
