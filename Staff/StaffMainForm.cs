using Npgsql;
using System;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class StaffMainForm : Form
    {
        private int userId;
        private string fullName;

        public StaffMainForm(int userId, string fullName)
        {
            InitializeComponent();
            this.userId = userId;
            this.fullName = fullName;
        }

        private void StaffMainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Добро пожаловать, {fullName}!";
        }

        private void просмотретьЗаявленияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ApplicationsListForm().ShowDialog();
        }

        private void сформироватьОтчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ReportsForm().ShowDialog();
        }

        private void управлениеНаправлениямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ProgramsManagementForm().ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}