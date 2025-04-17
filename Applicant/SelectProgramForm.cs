using System;
using System.Data;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class SelectProgramForm : Form
    {
        public ProgramItem SelectedProgram { get; private set; }

        public SelectProgramForm(DataTable programs)
        {
            InitializeComponent();
            dgvPrograms.DataSource = programs;
            ApplyCustomStyle();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.SelectedRows.Count > 0)
            {
                DataRowView row = (DataRowView)dgvPrograms.SelectedRows[0].DataBoundItem;
                SelectedProgram = new ProgramItem
                {
                    Id = (int)row["id"],
                    Name = row["name"].ToString(),
                    Code = row["code"].ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите направление подготовки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void ApplyCustomStyle()
        {
            // Общий стиль формы
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            // Стилизация DataGridView
            dgvPrograms.BackgroundColor = Color.White;
            dgvPrograms.DefaultCellStyle.BackColor = Color.White;
            dgvPrograms.DefaultCellStyle.ForeColor = Color.Black;
            dgvPrograms.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dgvPrograms.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPrograms.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvPrograms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPrograms.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPrograms.EnableHeadersVisualStyles = false;
            dgvPrograms.GridColor = Color.LightGray;

            // Кнопка "Выбрать"
            btnSelect.BackColor = Color.SteelBlue;
            btnSelect.ForeColor = Color.White;
            btnSelect.FlatStyle = FlatStyle.Flat;
            btnSelect.FlatAppearance.BorderSize = 0;

            // Кнопка "Отмена"
            btnCancel.BackColor = Color.LightGray;
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
        }
    }
}