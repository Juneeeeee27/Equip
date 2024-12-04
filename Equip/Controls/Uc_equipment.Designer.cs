namespace Equip.Controls
{
    partial class Uc_equipment
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnElectronics = new System.Windows.Forms.Button();
            this.btnSports = new System.Windows.Forms.Button();
            this.btnCulinary = new System.Windows.Forms.Button();
            this.btnTailoring = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(496, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Equipment";
            // 
            // btnElectronics
            // 
            this.btnElectronics.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnElectronics.Location = new System.Drawing.Point(311, 250);
            this.btnElectronics.Name = "btnElectronics";
            this.btnElectronics.Size = new System.Drawing.Size(660, 63);
            this.btnElectronics.TabIndex = 1;
            this.btnElectronics.Text = "Electronics";
            this.btnElectronics.UseVisualStyleBackColor = true;
            this.btnElectronics.Click += new System.EventHandler(this.btnElectronics_Click);
            // 
            // btnSports
            // 
            this.btnSports.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSports.Location = new System.Drawing.Point(311, 332);
            this.btnSports.Name = "btnSports";
            this.btnSports.Size = new System.Drawing.Size(660, 63);
            this.btnSports.TabIndex = 2;
            this.btnSports.Text = "Sports";
            this.btnSports.UseVisualStyleBackColor = true;
            this.btnSports.Click += new System.EventHandler(this.btnSports_Click);
            // 
            // btnCulinary
            // 
            this.btnCulinary.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCulinary.Location = new System.Drawing.Point(311, 412);
            this.btnCulinary.Name = "btnCulinary";
            this.btnCulinary.Size = new System.Drawing.Size(660, 63);
            this.btnCulinary.TabIndex = 3;
            this.btnCulinary.Text = "Culinary";
            this.btnCulinary.UseVisualStyleBackColor = true;
            this.btnCulinary.Click += new System.EventHandler(this.btnCulinary_Click);
            // 
            // btnTailoring
            // 
            this.btnTailoring.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTailoring.Location = new System.Drawing.Point(311, 498);
            this.btnTailoring.Name = "btnTailoring";
            this.btnTailoring.Size = new System.Drawing.Size(660, 63);
            this.btnTailoring.TabIndex = 4;
            this.btnTailoring.Text = "Tailoring";
            this.btnTailoring.UseVisualStyleBackColor = true;
            this.btnTailoring.Click += new System.EventHandler(this.btnTailoring_Click);
            // 
            // Uc_equipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTailoring);
            this.Controls.Add(this.btnCulinary);
            this.Controls.Add(this.btnSports);
            this.Controls.Add(this.btnElectronics);
            this.Controls.Add(this.label1);
            this.Name = "Uc_equipment";
            this.Size = new System.Drawing.Size(1227, 691);
            this.Load += new System.EventHandler(this.Uc_equipment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnElectronics;
        private System.Windows.Forms.Button btnSports;
        private System.Windows.Forms.Button btnCulinary;
        private System.Windows.Forms.Button btnTailoring;
    }
}
