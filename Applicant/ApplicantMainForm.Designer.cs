namespace AdmissionSystem
{
    partial class ApplicantMainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicantMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.заявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.податьЗаявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотретьЗаявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.распечататьЗаявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заявлениеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(784, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // заявлениеToolStripMenuItem
            // 
            this.заявлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.податьЗаявлениеToolStripMenuItem,
            this.просмотретьЗаявлениеToolStripMenuItem,
            this.распечататьЗаявлениеToolStripMenuItem});
            this.заявлениеToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.заявлениеToolStripMenuItem.Name = "заявлениеToolStripMenuItem";
            this.заявлениеToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.заявлениеToolStripMenuItem.Text = "Заявление";
            // 
            // податьЗаявлениеToolStripMenuItem
            // 
            this.податьЗаявлениеToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.податьЗаявлениеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("податьЗаявлениеToolStripMenuItem.Image")));
            this.податьЗаявлениеToolStripMenuItem.Name = "податьЗаявлениеToolStripMenuItem";
            this.податьЗаявлениеToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.податьЗаявлениеToolStripMenuItem.Text = "Подать заявление";
            this.податьЗаявлениеToolStripMenuItem.Click += new System.EventHandler(this.податьЗаявлениеToolStripMenuItem_Click);
            // 
            // просмотретьЗаявлениеToolStripMenuItem
            // 
            this.просмотретьЗаявлениеToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.просмотретьЗаявлениеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("просмотретьЗаявлениеToolStripMenuItem.Image")));
            this.просмотретьЗаявлениеToolStripMenuItem.Name = "просмотретьЗаявлениеToolStripMenuItem";
            this.просмотретьЗаявлениеToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.просмотретьЗаявлениеToolStripMenuItem.Text = "Просмотреть заявление";
            this.просмотретьЗаявлениеToolStripMenuItem.Click += new System.EventHandler(this.просмотретьЗаявлениеToolStripMenuItem_Click);
            // 
            // распечататьЗаявлениеToolStripMenuItem
            // 
            this.распечататьЗаявлениеToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.распечататьЗаявлениеToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("распечататьЗаявлениеToolStripMenuItem.Image")));
            this.распечататьЗаявлениеToolStripMenuItem.Name = "распечататьЗаявлениеToolStripMenuItem";
            this.распечататьЗаявлениеToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.распечататьЗаявлениеToolStripMenuItem.Text = "Распечатать заявление";
            this.распечататьЗаявлениеToolStripMenuItem.Click += new System.EventHandler(this.распечататьЗаявлениеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.выходToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWelcome.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblWelcome.Location = new System.Drawing.Point(12, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(78, 32);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Location = new System.Drawing.Point(200, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 200);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(650, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ApplicantMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ApplicantMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Учет заявок на поступление - Абитуриент";
            this.Load += new System.EventHandler(this.ApplicantMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem заявлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem податьЗаявлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотретьЗаявлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem распечататьЗаявлениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
