using System;
using System.Windows.Forms;

namespace Equip
{
    public partial class Dashboard : Form
    {
        Controls.Uc_dashboard home = new Controls.Uc_dashboard();
        Controls.Uc_inventory inventory = new Controls.Uc_inventory();
        Controls.Uc_history history = new Controls.Uc_history();
        Controls.Uc_reservation reservation = new Controls.Uc_reservation();
        Controls.Uc_user calendar = new Controls.Uc_user();


        public Dashboard()
        {
            InitializeComponent();

        }





        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure to logout?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            panels.Controls.Clear();
            panels.Controls.Add(home);
            home.Dock = DockStyle.Fill;
        }

        private void btnInven_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnInven.Height;
            sidePanel.Top = btnInven.Top;
            panels.Controls.Clear();
            panels.Controls.Add(inventory);
            inventory.Dock = DockStyle.Fill;
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnRes.Height;
            sidePanel.Top = btnRes.Top;
            panels.Controls.Clear();
            panels.Controls.Add(reservation);
            reservation.Dock = DockStyle.Fill;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnHome.Height;
            sidePanel.Top = btnHome.Top;
            panels.Controls.Clear();
            panels.Controls.Add(home);
            home.Dock = DockStyle.Fill;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {

        }

        private void btnCalen_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnCalen.Height;
            sidePanel.Top = btnCalen.Top;
            panels.Controls.Clear();
            panels.Controls.Add(calendar);
            calendar.Dock = DockStyle.Fill;
        }

        private void btnHis_Click(object sender, EventArgs e)
        {
            sidePanel.Height = btnHis.Height;
            sidePanel.Top = btnHis.Top;
            panels.Controls.Clear();
            panels.Controls.Add(history);
            history.Dock = DockStyle.Fill;
        }

        private void uc_home1_Load(object sender, EventArgs e)
        {

        }
    }
}
