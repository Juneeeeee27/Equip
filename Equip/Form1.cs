using System;
using System.Windows.Forms;

namespace Equip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Subscribe to the KeyPress event for txtUser and txtPass
            txtUser.KeyPress += new KeyPressEventHandler(txtUser_KeyPress);
            txtPass.KeyPress += new KeyPressEventHandler(txtPass_KeyPress);
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformLogin();
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the Enter key was pressed
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            if (txtUser != null && txtPass != null && labelError != null)
            {
                if (txtUser.Text == "admin" && txtPass.Text == "admin")
                {
                    Dashboard dash = new Dashboard();
                    dash.ShowDialog();
                    labelError.Visible = false;
                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();
                }
                else
                {
                    labelError.Visible = true;
                    txtUser.Clear();
                    txtPass.Clear();
                    txtUser.Focus();
                }
            }
            else
            {
                MessageBox.Show("Controls are not initialized properly.");
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
