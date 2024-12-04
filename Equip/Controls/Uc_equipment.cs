using System;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_equipment : UserControl
    {
        Controls.Uc_electronics electronics = new Controls.Uc_electronics();

        public Uc_equipment()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnElectronics_Click(object sender, EventArgs e)
        {
            Electronics electronics = new Electronics();
            electronics.Show();
        }

        private void Uc_equipment_Load(object sender, EventArgs e)
        {

        }

        private void btnSports_Click(object sender, EventArgs e)
        {
            Sports sports = new Sports();
            sports.Show();
        }

        private void btnCulinary_Click(object sender, EventArgs e)
        {
            Culinary culinary = new Culinary();
            culinary.Show();
        }

        private void btnTailoring_Click(object sender, EventArgs e)
        {
            Tailoring tailoring = new Tailoring();
            tailoring.Show();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            AddCategory addCategory = new AddCategory();
            addCategory.Show();
        }
    }
}
