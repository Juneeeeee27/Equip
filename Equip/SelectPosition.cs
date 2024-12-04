using System;
using System.Windows.Forms;

namespace Equip
{
    public partial class SelectPosition : Form
    {
        private int id; // Store the ID to pass to the forms
        private bool isFormSubmitted = false; // Flag to track if a form was submitted successfully

        public SelectPosition()
        {
            InitializeComponent();
        }

        // Method to handle the close button click
        private void PicExit_Click(object sender, EventArgs e)
        {
            if (!isFormSubmitted) // Only close if no form was submitted
            {
                this.Close(); // Close the SelectPosition form
            }
        }

        // Opens the StudentForm when the button is clicked
        private void btnStudent_Click(object sender, EventArgs e)
        {
            OpenStudentForm();
        }

        // Opens the FacultyForm when the button is clicked
        private void btnFaculty_Click(object sender, EventArgs e)
        {
            OpenFacultyForm();
        }

        // Method to open the StudentForm
        private void OpenStudentForm()
        {
            try
            {
                // Create the StudentForm and pass the ID
                using (StudentForm studentForm = new StudentForm(id))
                {
                    if (studentForm.ShowDialog() == DialogResult.OK) // Check if form submission was successful
                    {
                        isFormSubmitted = true;
                        this.Close(); // Close the SelectPosition form
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Student Form: " + ex.Message);
            }
        }

        // Method to open the FacultyForm
        private void OpenFacultyForm()
        {
            try
            {
                // Create the FacultyForm and pass the ID
                using (FacultyForm facultyForm = new FacultyForm(id))
                {
                    if (facultyForm.ShowDialog() == DialogResult.OK) // Check if form submission was successful
                    {
                        isFormSubmitted = true;
                        this.Close(); // Close the SelectPosition form
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Faculty Form: " + ex.Message);
            }
        }

        // Any initialization code for SelectPosition, if needed
        private void SelectPosition_Load(object sender, EventArgs e)
        {
            // Initialization code can be placed here, if required
        }
    }
}
