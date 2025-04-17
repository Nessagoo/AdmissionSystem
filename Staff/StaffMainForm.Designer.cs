namespace AdmissionSystem
{
    partial class StaffMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaffMainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.applicationsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewApplicationsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.generateReportItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.manageProgramsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // mainMenu - современное меню
            // 
            this.mainMenu.BackColor = System.Drawing.Color.SteelBlue;
            this.mainMenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationsMenu,
            this.reportsMenu,
            this.programsMenu,
            this.exitMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1000, 32);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";

            // 
            // applicationsMenu - меню заявлений
            // 
            this.applicationsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewApplicationsItem});
            this.applicationsMenu.ForeColor = System.Drawing.Color.White;
            this.applicationsMenu.Name = "applicationsMenu";
            this.applicationsMenu.Size = new System.Drawing.Size(102, 28);
            this.applicationsMenu.Text = "Заявления";

            // 
            // viewApplicationsItem
            // 
            this.viewApplicationsItem.Image = ((System.Drawing.Image)(resources.GetObject("viewApplicationsItem.Image")));
            this.viewApplicationsItem.Name = "viewApplicationsItem";
            this.viewApplicationsItem.Size = new System.Drawing.Size(280, 28);
            this.viewApplicationsItem.Text = "Просмотреть заявления";
            this.viewApplicationsItem.Click += new System.EventHandler(this.просмотретьЗаявленияToolStripMenuItem_Click);

            // 
            // reportsMenu - меню отчетов
            // 
            this.reportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateReportItem});
            this.reportsMenu.ForeColor = System.Drawing.Color.White;
            this.reportsMenu.Name = "reportsMenu";
            this.reportsMenu.Size = new System.Drawing.Size(76, 28);
            this.reportsMenu.Text = "Отчеты";

            // 
            // generateReportItem
            // 
            this.generateReportItem.Image = ((System.Drawing.Image)(resources.GetObject("generateReportItem.Image")));
            this.generateReportItem.Name = "generateReportItem";
            this.generateReportItem.Size = new System.Drawing.Size(252, 28);
            this.generateReportItem.Text = "Сформировать отчет";
            this.generateReportItem.Click += new System.EventHandler(this.сформироватьОтчетToolStripMenuItem_Click);

            // 
            // programsMenu - меню направлений
            // 
            this.programsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageProgramsItem});
            this.programsMenu.ForeColor = System.Drawing.Color.White;
            this.programsMenu.Name = "programsMenu";
            this.programsMenu.Size = new System.Drawing.Size(211, 28);
            this.programsMenu.Text = "Направления подготовки";

            // 
            // manageProgramsItem
            // 
            this.manageProgramsItem.Image = ((System.Drawing.Image)(resources.GetObject("manageProgramsItem.Image")));
            this.manageProgramsItem.Name = "manageProgramsItem";
            this.manageProgramsItem.Size = new System.Drawing.Size(303, 28);
            this.manageProgramsItem.Text = "Управление направлениями";
            this.manageProgramsItem.Click += new System.EventHandler(this.управлениеНаправлениямиToolStripMenuItem_Click);

            // 
            // exitMenu - меню выхода
            // 
            this.exitMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exitMenu.ForeColor = System.Drawing.Color.White;
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(70, 28);
            this.exitMenu.Text = "Выход";
            this.exitMenu.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);

            // 
            // lblWelcome - приветственная надпись
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblWelcome.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblWelcome.Location = new System.Drawing.Point(300, 200);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(400, 37);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Добро пожаловать в систему!";

            // 
            // pictureBox1 - логотип
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(400, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;

            // 
            // statusStrip - строка состояния
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 650);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1000, 26);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(151, 20);
            this.statusLabel.Text = "Готов к работе";

            // 
            // StaffMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 676);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "StaffMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Система учета заявок - Приемная комиссия";
            this.Load += new System.EventHandler(this.StaffMainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem applicationsMenu;
        private System.Windows.Forms.ToolStripMenuItem viewApplicationsItem;
        private System.Windows.Forms.ToolStripMenuItem reportsMenu;
        private System.Windows.Forms.ToolStripMenuItem generateReportItem;
        private System.Windows.Forms.ToolStripMenuItem programsMenu;
        private System.Windows.Forms.ToolStripMenuItem manageProgramsItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}