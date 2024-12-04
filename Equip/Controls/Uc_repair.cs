using System;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_repair : UserControl
    {
        public Uc_repair()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            createSched createSched = new createSched();
            createSched.ShowDialog();

        }
    }
}
