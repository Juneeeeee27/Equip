using System;
using System.Drawing;
using System.Windows.Forms;

namespace Equip
{
    public partial class AddUserForm : Form
    {
        public string UserName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        public string EquipmentInCharge
        {
            get => cmbEquipmentInCharge.SelectedItem?.ToString();
            set => cmbEquipmentInCharge.SelectedItem = value;
        }

        public Image UserPhoto
        {
            get => pictureBoxPhoto.Image;
            set => pictureBoxPhoto.Image = value;
        }

        public AddUserForm()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ensure name and equipment are filled
            if (string.IsNullOrEmpty(txtName.Text) || cmbEquipmentInCharge.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            DialogResult = DialogResult.OK; // Close form and return OK
            Close();
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxPhoto.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
