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

            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.заявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.податьЗаявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотретьЗаявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.распечататьЗаявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.заявлениеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // заявлениеToolStripMenuItem
            // 
            this.заявлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.податьЗаявлениеToolStripMenuItem,
            this.просмотретьЗаявлениеToolStripMenuItem,
            this.распечататьЗаявлениеToolStripMenuItem});
            this.заявлениеToolStripMenuItem.Name = "заявлениеToolStripMenuItem";
            this.заявлениеToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.заявлениеToolStripMenuItem.Text = "Заявление";
            // 
            // податьЗаявлениеToolStripMenuItem
            // 
            this.податьЗаявлениеToolStripMenuItem.Name = "податьЗаявлениеToolStripMenuItem";
            this.податьЗаявлениеToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.податьЗаявлениеToolStripMenuItem.Text = "Подать заявление";
            this.податьЗаявлениеToolStripMenuItem.Click += new System.EventHandler(this.податьЗаявлениеToolStripMenuItem_Click);
            // 
            // просмотретьЗаявлениеToolStripMenuItem
            // 
            this.просмотретьЗаявлениеToolStripMenuItem.Name = "просмотретьЗаявлениеToolStripMenuItem";
            this.просмотретьЗаявлениеToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.просмотретьЗаявлениеToolStripMenuItem.Text = "Просмотреть заявление";
            this.просмотретьЗаявлениеToolStripMenuItem.Click += new System.EventHandler(this.просмотретьЗаявлениеToolStripMenuItem_Click);
            // 
            // распечататьЗаявлениеToolStripMenuItem
            // 
            this.распечататьЗаявлениеToolStripMenuItem.Name = "распечататьЗаявлениеToolStripMenuItem";
            this.распечататьЗаявлениеToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.распечататьЗаявлениеToolStripMenuItem.Text = "Распечатать заявление";
            this.распечататьЗаявлениеToolStripMenuItem.Click += new System.EventHandler(this.распечататьЗаявлениеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWelcome.Location = new System.Drawing.Point(50, 50);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(60, 24);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "label1";
            // 
            // ApplicantMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ApplicantMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Учет заявок на поступление - Абитуриент";
            this.Load += new System.EventHandler(this.ApplicantMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
    }
}