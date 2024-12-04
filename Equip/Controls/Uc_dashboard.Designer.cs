namespace Equip.Controls
{
    partial class Uc_dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uc_dashboard));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelToday = new System.Windows.Forms.Panel();
            this.lblTodayReservations = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelWeek = new System.Windows.Forms.Panel();
            this.lblThisWeekReservations = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblMonth = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.uc_calendar1 = new Equip.Controls.Uc_calendar();
            this.panelToday.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelWeek.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 95);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1602, 5);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(237, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1018, 70);
            this.label1.TabIndex = 3;
            this.label1.Text = "EQUIPMENT MANAGEMENT SYSTEM";
            // 
            // panelToday
            // 
            this.panelToday.BackColor = System.Drawing.Color.Goldenrod;
            this.panelToday.Controls.Add(this.lblTodayReservations);
            this.panelToday.Controls.Add(this.label39);
            this.panelToday.Controls.Add(this.pictureBox1);
            this.panelToday.Location = new System.Drawing.Point(40, 273);
            this.panelToday.Name = "panelToday";
            this.panelToday.Size = new System.Drawing.Size(195, 139);
            this.panelToday.TabIndex = 6;
            // 
            // lblTodayReservations
            // 
            this.lblTodayReservations.AutoSize = true;
            this.lblTodayReservations.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodayReservations.Location = new System.Drawing.Point(112, 62);
            this.lblTodayReservations.Name = "lblTodayReservations";
            this.lblTodayReservations.Size = new System.Drawing.Size(35, 40);
            this.lblTodayReservations.TabIndex = 2;
            this.lblTodayReservations.Text = "1";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(33, 14);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(145, 16);
            this.label39.TabIndex = 1;
            this.label39.Text = "Total Reserved Today";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 58);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelWeek
            // 
            this.panelWeek.BackColor = System.Drawing.Color.Goldenrod;
            this.panelWeek.Controls.Add(this.lblThisWeekReservations);
            this.panelWeek.Controls.Add(this.label40);
            this.panelWeek.Controls.Add(this.pictureBox2);
            this.panelWeek.Location = new System.Drawing.Point(37, 572);
            this.panelWeek.Name = "panelWeek";
            this.panelWeek.Size = new System.Drawing.Size(195, 139);
            this.panelWeek.TabIndex = 7;
            // 
            // lblThisWeekReservations
            // 
            this.lblThisWeekReservations.AutoSize = true;
            this.lblThisWeekReservations.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThisWeekReservations.Location = new System.Drawing.Point(112, 62);
            this.lblThisWeekReservations.Name = "lblThisWeekReservations";
            this.lblThisWeekReservations.Size = new System.Drawing.Size(35, 40);
            this.lblThisWeekReservations.TabIndex = 3;
            this.lblThisWeekReservations.Text = "4";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(25, 17);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(169, 16);
            this.label40.TabIndex = 1;
            this.label40.Text = "Total Reserved This Week";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(3, 53);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 58);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // lblMonth
            // 
            this.lblMonth.BackColor = System.Drawing.Color.Transparent;
            this.lblMonth.Font = new System.Drawing.Font("Century Gothic", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(365, 23);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(294, 46);
            this.lblMonth.TabIndex = 47;
            this.lblMonth.Text = "September 2024";
            // 
            // uc_calendar1
            // 
            this.uc_calendar1.Location = new System.Drawing.Point(292, 123);
            this.uc_calendar1.Name = "uc_calendar1";
            this.uc_calendar1.Size = new System.Drawing.Size(1055, 2921);
            this.uc_calendar1.TabIndex = 8;
            // 
            // Uc_dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uc_calendar1);
            this.Controls.Add(this.panelWeek);
            this.Controls.Add(this.panelToday);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Uc_dashboard";
            this.Size = new System.Drawing.Size(1602, 1033);
            this.panelToday.ResumeLayout(false);
            this.panelToday.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelWeek.ResumeLayout(false);
            this.panelWeek.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelToday;
        private System.Windows.Forms.Label lblTodayReservations;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelWeek;
        private System.Windows.Forms.Label lblThisWeekReservations;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonth;
        private Uc_calendar uc_calendar1;
    }
}
