using System;
using System.Windows.Forms;

namespace Equip
{
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
            cmbPosition.SelectedIndexChanged += cmbPosition_SelectedIndexChanged;

            // Set panel properties to adjust automatically based on control visibility
            panelContent.AutoSize = true;
            panelContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPosition.SelectedItem.ToString() == "Faculty")
            {
                txtYearSection.Visible = false;
                lblYearSection.Visible = false;
                txtSubjectAdviser.Visible = false;
                lblSubjectAdviser.Visible = false;
            }
            else if (cmbPosition.SelectedItem.ToString() == "Student")
            {
                txtYearSection.Visible = true;
                lblYearSection.Visible = true;
                txtSubjectAdviser.Visible = true;
                lblSubjectAdviser.Visible = true;
            }

            // Adjust the layout of panelContent after changing visibility of controls
            AdjustLayout();
        }

        private void AdjustLayout()
        {
            // Refresh panel layout to apply the visibility changes
            panelContent.PerformLayout();
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
