namespace WindowsFormsApp1
{
    partial class AnaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnKat1 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnKat2 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnKat3 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMusteri = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAdmin = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnKat1,
            this.tsbtnKat2,
            this.tsbtnKat3,
            this.tsbtnMusteri,
            this.tsbtnAdmin});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1389, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnKat1
            // 
            this.tsbtnKat1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnKat1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsbtnKat1.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnKat1.Image")));
            this.tsbtnKat1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnKat1.Name = "tsbtnKat1";
            this.tsbtnKat1.Size = new System.Drawing.Size(67, 29);
            this.tsbtnKat1.Text = "KAT 1";
            this.tsbtnKat1.Click += new System.EventHandler(this.tsbtnKat1_Click);
            // 
            // tsbtnKat2
            // 
            this.tsbtnKat2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnKat2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsbtnKat2.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnKat2.Image")));
            this.tsbtnKat2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnKat2.Name = "tsbtnKat2";
            this.tsbtnKat2.Size = new System.Drawing.Size(67, 29);
            this.tsbtnKat2.Text = "KAT 2";
            this.tsbtnKat2.Click += new System.EventHandler(this.tsbtnKat2_Click);
            // 
            // tsbtnKat3
            // 
            this.tsbtnKat3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnKat3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsbtnKat3.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnKat3.Image")));
            this.tsbtnKat3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnKat3.Name = "tsbtnKat3";
            this.tsbtnKat3.Size = new System.Drawing.Size(67, 29);
            this.tsbtnKat3.Text = "KAT 3";
            this.tsbtnKat3.Click += new System.EventHandler(this.tsbtnKat3_Click);
            // 
            // tsbtnMusteri
            // 
            this.tsbtnMusteri.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnMusteri.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsbtnMusteri.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMusteri.Image")));
            this.tsbtnMusteri.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMusteri.Name = "tsbtnMusteri";
            this.tsbtnMusteri.Size = new System.Drawing.Size(115, 29);
            this.tsbtnMusteri.Text = "Müşteri Kayıt";
            this.tsbtnMusteri.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnAdmin
            // 
            this.tsbtnAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnAdmin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tsbtnAdmin.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdmin.Image")));
            this.tsbtnAdmin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdmin.Name = "tsbtnAdmin";
            this.tsbtnAdmin.Size = new System.Drawing.Size(70, 29);
            this.tsbtnAdmin.Text = "ADMİN";
            this.tsbtnAdmin.Click += new System.EventHandler(this.tsbtnAdmin_Click);
            // 
            // AnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 881);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "AnaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KATLI OTOPARK";
            this.Load += new System.EventHandler(this.AnaForm_Load);
            this.DoubleClick += new System.EventHandler(this.AnaForm_DoubleClick);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton tsbtnKat1;
        public System.Windows.Forms.ToolStripButton tsbtnKat2;
        public System.Windows.Forms.ToolStripButton tsbtnKat3;
        public System.Windows.Forms.ToolStripButton tsbtnMusteri;
        public System.Windows.Forms.ToolStripButton tsbtnAdmin;
    }
}