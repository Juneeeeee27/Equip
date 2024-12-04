using System;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_inventory : UserControl
    {
        Controls.Uc_equipment equip = new Controls.Uc_equipment();
        Controls.Uc_repair repair = new Controls.Uc_repair();
        Controls.Uc_Decommissioned archive = new Controls.Uc_Decommissioned();
        Controls.Uc_electronics electronics = new Controls.Uc_electronics();
        public Uc_inventory()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void Uc_inventory_Load(object sender, EventArgs e)
        {
            panelInventory.Controls.Clear();
            panelInventory.Controls.Add(equip);
            equip.Dock = DockStyle.Fill;
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            panelInventory.Controls.Clear();
            panelInventory.Controls.Add(repair);
            repair.Dock = DockStyle.Fill;
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            panelInventory.Controls.Clear();
            panelInventory.Controls.Add(equip);
            equip.Dock = DockStyle.Fill;
            panelInventory.Controls.Add(electronics);
            electronics.Dock = DockStyle.Fill;
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            panelInventory.Controls.Clear();
            panelInventory.Controls.Add(archive);
            archive.Dock = DockStyle.Fill;
        }

        private void panelInventory_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
