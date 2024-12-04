namespace Equip.Controls
{
    partial class Uc_Decommissioned
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
            if (disposing)
            {
                components?.Dispose(); // This line is auto-generated for component cleanup
                refreshTimer?.Dispose(); // Add this to clean up the timer
            }
            base.Dispose(disposing); // Call the base class Dispose method
        }


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDecommissioned = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDecommissioned)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDecommissioned
            // 
            this.dgvDecommissioned.AllowUserToAddRows = false;
            this.dgvDecommissioned.AllowUserToDeleteRows = false;
            this.dgvDecommissioned.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDecommissioned.Location = new System.Drawing.Point(0, 0);
            this.dgvDecommissioned.Name = "dgvDecommissioned";
            this.dgvDecommissioned.ReadOnly = true;
            this.dgvDecommissioned.RowHeadersWidth = 51;
            this.dgvDecommissioned.RowTemplate.Height = 24;
            this.dgvDecommissioned.Size = new System.Drawing.Size(1296, 855);
            this.dgvDecommissioned.TabIndex = 4;
            // 
            // Uc_Decommissioned
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDecommissioned);
            this.Name = "Uc_Decommissioned";
            this.Size = new System.Drawing.Size(1296, 855);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDecommissioned)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDecommissioned;
    }
}
