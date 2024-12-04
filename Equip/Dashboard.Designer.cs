namespace Equip
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.sideContainer = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnInven = new System.Windows.Forms.Button();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnRes = new System.Windows.Forms.Button();
            this.btnlogout = new System.Windows.Forms.Button();
            this.btnHis = new System.Windows.Forms.Button();
            this.btnCalen = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panels = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.sideContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panels.SuspendLayout();
            this.SuspendLayout();
            // 
            // sideContainer
            // 
            this.sideContainer.BackColor = System.Drawing.Color.Silver;
            this.sideContainer.Controls.Add(this.label1);
            this.sideContainer.Controls.Add(this.pictureBox2);
            this.sideContainer.Controls.Add(this.btnInven);
            this.sideContainer.Controls.Add(this.sidePanel);
            this.sideContainer.Controls.Add(this.pictureBox1);
            this.sideContainer.Controls.Add(this.btnHome);
            this.sideContainer.Controls.Add(this.btnRes);
            this.sideContainer.Controls.Add(this.btnlogout);
            this.sideContainer.Controls.Add(this.btnHis);
            this.sideContainer.Controls.Add(this.btnCalen);
            this.sideContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideContainer.Location = new System.Drawing.Point(0, 0);
            this.sideContainer.Margin = new System.Windows.Forms.Padding(4);
            this.sideContainer.Name = "sideContainer";
            this.sideContainer.Size = new System.Drawing.Size(286, 922);
            this.sideContainer.TabIndex = 0;
            this.sideContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Admin";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Location = new System.Drawing.Point(78, 274);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 44);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // btnInven
            // 
            this.btnInven.FlatAppearance.BorderSize = 0;
            this.btnInven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInven.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInven.ForeColor = System.Drawing.Color.Black;
            this.btnInven.Image = ((System.Drawing.Image)(resources.GetObject("btnInven.Image")));
            this.btnInven.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInven.Location = new System.Drawing.Point(6, 486);
            this.btnInven.Margin = new System.Windows.Forms.Padding(4);
            this.btnInven.Name = "btnInven";
            this.btnInven.Size = new System.Drawing.Size(294, 52);
            this.btnInven.TabIndex = 2;
            this.btnInven.Tag = "";
            this.btnInven.Text = "     Inventory";
            this.btnInven.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInven.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInven.UseVisualStyleBackColor = true;
            this.btnInven.Click += new System.EventHandler(this.btnInven_Click);
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.sidePanel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.sidePanel.Location = new System.Drawing.Point(0, 370);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(8, 50);
            this.sidePanel.TabIndex = 3;
            this.sidePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(30, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 196);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnHome
            // 
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.Black;
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(9, 366);
            this.btnHome.Margin = new System.Windows.Forms.Padding(4);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(294, 52);
            this.btnHome.TabIndex = 7;
            this.btnHome.Tag = "";
            this.btnHome.Text = "     Dashboard";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnRes
            // 
            this.btnRes.FlatAppearance.BorderSize = 0;
            this.btnRes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRes.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRes.ForeColor = System.Drawing.Color.Black;
            this.btnRes.Image = ((System.Drawing.Image)(resources.GetObject("btnRes.Image")));
            this.btnRes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRes.Location = new System.Drawing.Point(6, 426);
            this.btnRes.Margin = new System.Windows.Forms.Padding(4);
            this.btnRes.Name = "btnRes";
            this.btnRes.Size = new System.Drawing.Size(294, 52);
            this.btnRes.TabIndex = 4;
            this.btnRes.Tag = "     ";
            this.btnRes.Text = "     Reservation";
            this.btnRes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRes.UseVisualStyleBackColor = true;
            this.btnRes.Click += new System.EventHandler(this.btnRes_Click);
            // 
            // btnlogout
            // 
            this.btnlogout.FlatAppearance.BorderSize = 0;
            this.btnlogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogout.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogout.ForeColor = System.Drawing.Color.Black;
            this.btnlogout.Image = ((System.Drawing.Image)(resources.GetObject("btnlogout.Image")));
            this.btnlogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlogout.Location = new System.Drawing.Point(9, 666);
            this.btnlogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnlogout.Name = "btnlogout";
            this.btnlogout.Size = new System.Drawing.Size(294, 52);
            this.btnlogout.TabIndex = 3;
            this.btnlogout.Tag = "";
            this.btnlogout.Text = "     Logout";
            this.btnlogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnlogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlogout.UseVisualStyleBackColor = true;
            this.btnlogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnHis
            // 
            this.btnHis.FlatAppearance.BorderSize = 0;
            this.btnHis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHis.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHis.ForeColor = System.Drawing.Color.Black;
            this.btnHis.Image = ((System.Drawing.Image)(resources.GetObject("btnHis.Image")));
            this.btnHis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHis.Location = new System.Drawing.Point(6, 546);
            this.btnHis.Margin = new System.Windows.Forms.Padding(4);
            this.btnHis.Name = "btnHis";
            this.btnHis.Size = new System.Drawing.Size(294, 52);
            this.btnHis.TabIndex = 6;
            this.btnHis.Tag = "";
            this.btnHis.Text = "     History";
            this.btnHis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHis.UseVisualStyleBackColor = true;
            this.btnHis.Click += new System.EventHandler(this.btnHis_Click);
            // 
            // btnCalen
            // 
            this.btnCalen.FlatAppearance.BorderSize = 0;
            this.btnCalen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalen.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalen.ForeColor = System.Drawing.Color.Black;
            this.btnCalen.Image = ((System.Drawing.Image)(resources.GetObject("btnCalen.Image")));
            this.btnCalen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalen.Location = new System.Drawing.Point(6, 606);
            this.btnCalen.Margin = new System.Windows.Forms.Padding(4);
            this.btnCalen.Name = "btnCalen";
            this.btnCalen.Size = new System.Drawing.Size(294, 52);
            this.btnCalen.TabIndex = 5;
            this.btnCalen.Tag = "";
            this.btnCalen.Text = "     In-Charge";
            this.btnCalen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCalen.UseVisualStyleBackColor = true;
            this.btnCalen.Click += new System.EventHandler(this.btnCalen_Click);
            // 
            // panels
            // 
            this.panels.Controls.Add(this.panelMain);
            this.panels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panels.Location = new System.Drawing.Point(286, 0);
            this.panels.Name = "panels";
            this.panels.Size = new System.Drawing.Size(1336, 922);
            this.panels.TabIndex = 3;
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1240, 942);
            this.panelMain.TabIndex = 0;
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1622, 922);
            this.Controls.Add(this.panels);
            this.Controls.Add(this.sideContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.sideContainer.ResumeLayout(false);
            this.sideContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panels.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sideContainer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnHis;
        private System.Windows.Forms.Button btnCalen;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.Button btnlogout;
        private System.Windows.Forms.Button btnInven;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel sidePanel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panels;
        public System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}