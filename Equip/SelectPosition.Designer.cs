namespace Equip
{
    partial class SelectPosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectPosition));
            this.PicExit = new System.Windows.Forms.PictureBox();
            this.btnStudent = new Guna.UI2.WinForms.Guna2Button();
            this.btnFaculty = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicExit)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicExit
            // 
            this.PicExit.BackColor = System.Drawing.Color.White;
            this.PicExit.Image = ((System.Drawing.Image)(resources.GetObject("PicExit.Image")));
            this.PicExit.Location = new System.Drawing.Point(642, 12);
            this.PicExit.Name = "PicExit";
            this.PicExit.Size = new System.Drawing.Size(40, 34);
            this.PicExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicExit.TabIndex = 10;
            this.PicExit.TabStop = false;
            this.PicExit.Click += new System.EventHandler(this.PicExit_Click);
            // 
            // btnStudent
            // 
            this.btnStudent.AutoRoundedCorners = true;
            this.btnStudent.BorderRadius = 29;
            this.btnStudent.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStudent.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStudent.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStudent.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStudent.FillColor = System.Drawing.Color.Maroon;
            this.btnStudent.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudent.ForeColor = System.Drawing.Color.White;
            this.btnStudent.Location = new System.Drawing.Point(222, 130);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(251, 61);
            this.btnStudent.TabIndex = 11;
            this.btnStudent.Text = "STUDENT";
            this.btnStudent.Click += new System.EventHandler(this.btnStudent_Click);
            // 
            // btnFaculty
            // 
            this.btnFaculty.AutoRoundedCorners = true;
            this.btnFaculty.BorderRadius = 29;
            this.btnFaculty.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnFaculty.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnFaculty.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnFaculty.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnFaculty.FillColor = System.Drawing.Color.Maroon;
            this.btnFaculty.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaculty.ForeColor = System.Drawing.Color.White;
            this.btnFaculty.Location = new System.Drawing.Point(222, 214);
            this.btnFaculty.Name = "btnFaculty";
            this.btnFaculty.Size = new System.Drawing.Size(251, 61);
            this.btnFaculty.TabIndex = 12;
            this.btnFaculty.Text = "FACULTY";
            this.btnFaculty.Click += new System.EventHandler(this.btnFaculty_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PicExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(694, 62);
            this.panel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(200, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 38);
            this.label1.TabIndex = 11;
            this.label1.Text = "PLEASE SELECT";
            // 
            // SelectPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 365);
            this.Controls.Add(this.btnFaculty);
            this.Controls.Add(this.btnStudent);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectPosition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectionForm";
            this.Load += new System.EventHandler(this.SelectPosition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicExit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox PicExit;
        private Guna.UI2.WinForms.Guna2Button btnStudent;
        private Guna.UI2.WinForms.Guna2Button btnFaculty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}