using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Equip.Controls
{
    public partial class Uc_user : UserControl
    {
        private List<User> users = new List<User>();
        private const string UserFilePath = "users.dat"; // File path to store user data

        public Uc_user()
        {
            InitializeComponent();
            LoadUsers(); // Load users on initialization
        }

        private void DisplayUser(User user)
        {
            // Create a new panel for the user's information
            Panel userPanel = new Panel();
            userPanel.Size = new Size(180, 240);
            userPanel.BorderStyle = BorderStyle.FixedSingle;

            // Add PictureBox for the user's photo and set SizeMode
            PictureBox userPhoto = new PictureBox();
            userPhoto.Size = new Size(100, 100);
            userPhoto.Image = user.Photo;
            userPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            userPhoto.Location = new Point(40, 10);
            userPanel.Controls.Add(userPhoto);

            // Calculate the center position for labels
            int centerX = (userPanel.Width - 100) / 2; // 100 is the width of the label area

            // Add labels for name and equipment
            Label lblName = new Label();
            lblName.Text = user.Name;
            lblName.AutoSize = true;
            // Set font to bold
            lblName.Font = new Font(lblName.Font, FontStyle.Bold);
            // Center the label horizontally within the userPanel
            lblName.Location = new Point(centerX, 120);
            userPanel.Controls.Add(lblName);

            Label lblEquipment = new Label();
            lblEquipment.Text = user.EquipmentInCharge;
            lblEquipment.AutoSize = true;
            // Set font to bold
            lblEquipment.Font = new Font(lblEquipment.Font, FontStyle.Bold);
            // Center the label horizontally within the userPanel, under lblName
            lblEquipment.Location = new Point(centerX, lblName.Bottom + 5); // 5 pixels below lblName
            userPanel.Controls.Add(lblEquipment);

            // Add an Edit button
            Button btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Size = new Size(70, 30);
            btnEdit.Location = new Point(10, 180);
            btnEdit.Click += (s, e) => EditUser(user, userPanel, lblName, lblEquipment, userPhoto);
            userPanel.Controls.Add(btnEdit);

            // Add a Delete button
            Button btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Size = new Size(70, 30);
            btnDelete.Location = new Point(100, 180);
            btnDelete.Click += (s, e) => DeleteUser(user, userPanel);
            userPanel.Controls.Add(btnDelete);

            // Add the userPanel to the main display panel
            mainFlowLayoutPanel.Controls.Add(userPanel);
        }


        private void EditUser(User user, Panel userPanel, Label lblName, Label lblEquipment, PictureBox userPhoto)
        {
            AddUserForm addUserForm = new AddUserForm();
            addUserForm.UserName = user.Name;
            addUserForm.EquipmentInCharge = user.EquipmentInCharge;
            addUserForm.UserPhoto = user.Photo;

            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                // Update the user object
                user.Name = addUserForm.UserName;
                user.EquipmentInCharge = addUserForm.EquipmentInCharge;
                user.Photo = addUserForm.UserPhoto;

                // Update the user panel
                lblName.Text = user.Name;
                lblEquipment.Text = user.EquipmentInCharge;
                userPhoto.Image = user.Photo;

                SaveUsers(); // Save users after editing
            }
        }

        private void DeleteUser(User user, Panel userPanel)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Remove the user from the list and the display
                users.Remove(user);
                mainFlowLayoutPanel.Controls.Remove(userPanel);
                userPanel.Dispose();

                SaveUsers(); // Save users after deletion
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm();

            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                // Create a new User object with data from the form
                User newUser = new User
                {
                    Name = addUserForm.UserName,
                    EquipmentInCharge = GetUniqueEquipmentName(addUserForm.EquipmentInCharge),
                    Photo = addUserForm.UserPhoto
                };

                // Add the new user to the list
                users.Add(newUser);

                // Display the new user
                DisplayUser(newUser);

                SaveUsers(); // Save users after addition
                MessageBox.Show("User added successfully!");
            }
        }

        private string GetUniqueEquipmentName(string equipmentName)
        {
            int count = 1;
            string uniqueName = equipmentName;

            // Check for existing users with the same equipment name
            foreach (var user in users)
            {
                if (user.EquipmentInCharge.Equals(equipmentName, StringComparison.OrdinalIgnoreCase))
                {
                    uniqueName = $"{equipmentName} {count}";
                    count++;
                }
            }

            return uniqueName;
        }

        private void SaveUsers()
        {
            using (FileStream stream = new FileStream(UserFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, users);
            }
        }

        private void LoadUsers()
        {
            if (File.Exists(UserFilePath))
            {
                using (FileStream stream = new FileStream(UserFilePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    users = (List<User>)formatter.Deserialize(stream);
                }

                foreach (User user in users)
                {
                    DisplayUser(user); // Display each user loaded from the file
                }
            }
        }

        private void Uc_user_Load(object sender, EventArgs e)
        {

        }
    }
}
