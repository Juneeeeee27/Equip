namespace Equip.Controls
{
    partial class Uc_day
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
            this.panelDay = new System.Windows.Forms.Panel();
            this.lblDay = new System.Windows.Forms.Label();
            this.panelDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDay
            // 
            this.panelDay.BackColor = System.Drawing.Color.White;
            this.panelDay.Controls.Add(this.lblDay);
            this.panelDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDay.Location = new System.Drawing.Point(0, 0);
            this.panelDay.Name = "panelDay";
            this.panelDay.Size = new System.Drawing.Size(132, 100);
            this.panelDay.TabIndex = 0;
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.Location = new System.Drawing.Point(51, 34);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(30, 32);
            this.lblDay.TabIndex = 0;
            this.lblDay.Text = "1";
            // 
            // Uc_day
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.panelDay);
            this.Name = "Uc_day";
            this.Size = new System.Drawing.Size(132, 100);
            this.panelDay.ResumeLayout(false);
            this.panelDay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panelDay;
        public System.Windows.Forms.Label lblDay;
    }
}
