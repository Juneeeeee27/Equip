namespace Equip.Controls
{
    partial class Uc_inventory
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_inventory));
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEquipment = new System.Windows.Forms.Button();
            this.btnRepair = new System.Windows.Forms.Button();
            this.btnDecommission = new System.Windows.Forms.Button();
            this.panelInventory = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(443, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(355, 70);
            this.label2.TabIndex = 6;
            this.label2.Text = "INVENTORY";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(-9, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1634, 5);
            this.panel2.TabIndex = 24;
            // 
            // btnEquipment
            // 
            this.btnEquipment.BackColor = System.Drawing.Color.Goldenrod;
            this.btnEquipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEquipment.Image = ((System.Drawing.Image)(resources.GetObject("btnEquipment.Image")));
            this.btnEquipment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEquipment.Location = new System.Drawing.Point(19, 330);
            this.btnEquipment.Name = "btnEquipment";
            this.btnEquipment.Size = new System.Drawing.Size(232, 80);
            this.btnEquipment.TabIndex = 25;
            this.btnEquipment.Text = "   Equipment";
            this.btnEquipment.UseVisualStyleBackColor = false;
            this.btnEquipment.Click += new System.EventHandler(this.btnEquipment_Click);
            // 
            // btnRepair
            // 
            this.btnRepair.BackColor = System.Drawing.Color.Red;
            this.btnRepair.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepair.Image = ((System.Drawing.Image)(resources.GetObject("btnRepair.Image")));
            this.btnRepair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepair.Location = new System.Drawing.Point(19, 425);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(232, 80);
            this.btnRepair.TabIndex = 26;
            this.btnRepair.Text = "Repair";
            this.btnRepair.UseVisualStyleBackColor = false;
            this.btnRepair.Click += new System.EventHandler(this.btnRepair_Click);
            // 
            // btnDecommission
            // 
            this.btnDecommission.BackColor = System.Drawing.Color.Gray;
            this.btnDecommission.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecommission.Image = ((System.Drawing.Image)(resources.GetObject("btnDecommission.Image")));
            this.btnDecommission.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDecommission.Location = new System.Drawing.Point(19, 521);
            this.btnDecommission.Name = "btnDecommission";
            this.btnDecommission.Size = new System.Drawing.Size(232, 80);
            this.btnDecommission.TabIndex = 27;
            this.btnDecommission.Text = "      Decommissioned";
            this.btnDecommission.UseVisualStyleBackColor = false;
            this.btnDecommission.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // panelInventory
            // 
            this.panelInventory.BackColor = System.Drawing.Color.Transparent;
            this.panelInventory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInventory.Location = new System.Drawing.Point(257, 133);
            this.panelInventory.Name = "panelInventory";
            this.panelInventory.Size = new System.Drawing.Size(972, 691);
            this.panelInventory.TabIndex = 28;
            this.panelInventory.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInventory_Paint);
            // 
            // Uc_inventory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelInventory);
            this.Controls.Add(this.btnDecommission);
            this.Controls.Add(this.btnRepair);
            this.Controls.Add(this.btnEquipment);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Name = "Uc_inventory";
            this.Size = new System.Drawing.Size(1602, 1033);
            this.Load += new System.EventHandler(this.Uc_inventory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEquipment;
        private System.Windows.Forms.Button btnRepair;
        private System.Windows.Forms.Button btnDecommission;
        public System.Windows.Forms.Panel panelInventory;
    }
}
