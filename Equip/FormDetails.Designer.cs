namespace Equip.Controls
{
    partial class FormDetails
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetails));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tlpTimeSlot = new System.Windows.Forms.TableLayoutPanel();
            this.lblMonthDay = new System.Windows.Forms.Label();
            this.lblEquipment = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEquipmentName = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.PicExit = new System.Windows.Forms.PictureBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicExit)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.Controls.Add(this.tlpTimeSlot);
            this.mainPanel.Controls.Add(this.lblMonthDay);
            this.mainPanel.Controls.Add(this.lblEquipment);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.cmbEquipmentName);
            this.mainPanel.Controls.Add(this.cmbCategory);
            this.mainPanel.Controls.Add(this.PicExit);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(721, 914);
            this.mainPanel.TabIndex = 0;
            // 
            // tlpTimeSlot
            // 
            this.tlpTimeSlot.ColumnCount = 2;
            this.tlpTimeSlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTimeSlot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTimeSlot.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpTimeSlot.Location = new System.Drawing.Point(144, 263);
            this.tlpTimeSlot.Name = "tlpTimeSlot";
            this.tlpTimeSlot.RowCount = 10;
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlpTimeSlot.Size = new System.Drawing.Size(439, 430);
            this.tlpTimeSlot.TabIndex = 17;
            // 
            // lblMonthDay
            // 
            this.lblMonthDay.AutoSize = true;
            this.lblMonthDay.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthDay.Location = new System.Drawing.Point(249, 21);
            this.lblMonthDay.Name = "lblMonthDay";
            this.lblMonthDay.Size = new System.Drawing.Size(217, 45);
            this.lblMonthDay.TabIndex = 16;
            this.lblMonthDay.Text = "November 25";
            // 
            // lblEquipment
            // 
            this.lblEquipment.AutoSize = true;
            this.lblEquipment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEquipment.Location = new System.Drawing.Point(93, 159);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(162, 25);
            this.lblEquipment.TabIndex = 15;
            this.lblEquipment.Text = "Equipment Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Category";
            // 
            // cmbEquipmentName
            // 
            this.cmbEquipmentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEquipmentName.FormattingEnabled = true;
            this.cmbEquipmentName.Location = new System.Drawing.Point(290, 159);
            this.cmbEquipmentName.Name = "cmbEquipmentName";
            this.cmbEquipmentName.Size = new System.Drawing.Size(293, 33);
            this.cmbEquipmentName.TabIndex = 13;
            // 
            // cmbCategory
            // 
            this.cmbCategory.AutoCompleteCustomSource.AddRange(new string[] {
            "Electronics",
            "Sports",
            "Culinary",
            "Tailoring"});
            this.cmbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(290, 107);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(293, 33);
            this.cmbCategory.TabIndex = 12;
            // 
            // PicExit
            // 
            this.PicExit.BackColor = System.Drawing.Color.White;
            this.PicExit.Image = ((System.Drawing.Image)(resources.GetObject("PicExit.Image")));
            this.PicExit.Location = new System.Drawing.Point(669, 12);
            this.PicExit.Name = "PicExit";
            this.PicExit.Size = new System.Drawing.Size(40, 34);
            this.PicExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicExit.TabIndex = 11;
            this.PicExit.TabStop = false;
            this.PicExit.Click += new System.EventHandler(this.PicExit_Click);
            // 
            // FormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(721, 914);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Time";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.PictureBox PicExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEquipmentName;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblEquipment;
        private System.Windows.Forms.Label lblMonthDay;
        private System.Windows.Forms.TableLayoutPanel tlpTimeSlot;
    }
}