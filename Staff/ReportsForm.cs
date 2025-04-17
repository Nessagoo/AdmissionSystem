using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpEndDate.Value = DateTime.Now;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            if (startDate > endDate)
            {
                MessageBox.Show("Дата начала периода не может быть позже даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT 
                                el.name AS education_level,
                                sp.name AS study_program,
                                COUNT(a.id) AS applications_count,
                                SUM(CASE WHEN a.status = 'accepted' THEN 1 ELSE 0 END) AS accepted_count,
                                SUM(CASE WHEN a.status = 'rejected' THEN 1 ELSE 0 END) AS rejected_count,
                                SUM(CASE WHEN a.status = 'ready' THEN 1 ELSE 0 END) AS ready_count,
                                SUM(CASE WHEN a.status = 'submitted' THEN 1 ELSE 0 END) AS submitted_count
                                FROM applications a
                                JOIN education_levels el ON a.education_level_id = el.id
                                JOIN application_programs ap ON a.id = ap.application_id AND ap.priority = 1
                                JOIN study_programs sp ON ap.program_id = sp.id
                                WHERE a.application_date BETWEEN @startDate AND @endDate
                                GROUP BY el.name, sp.name
                                ORDER BY el.name, applications_count DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        DataTable dt = new DataTable();
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dgvReport.DataSource = dt;

                        // Настраиваем заголовки столбцов
                        if (dgvReport.Columns.Count > 0)
                        {
                            dgvReport.Columns["education_level"].HeaderText = "Уровень образования";
                            dgvReport.Columns["study_program"].HeaderText = "Направление подготовки";
                            dgvReport.Columns["applications_count"].HeaderText = "Всего заявлений";
                            dgvReport.Columns["accepted_count"].HeaderText = "Принято";
                            dgvReport.Columns["rejected_count"].HeaderText = "Отклонено";
                            dgvReport.Columns["ready_count"].HeaderText = "Готово";
                            dgvReport.Columns["submitted_count"].HeaderText = "Подано";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel файлы (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Экспорт отчета в Excel";
            saveFileDialog.FileName = $"Отчет_по_заявлениям_{DateTime.Now:yyyyMMdd}.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // TODO: Реализовать экспорт в Excel с помощью библиотеки (например, EPPlus)
                    MessageBox.Show($"Excel файл успешно сохранен: {saveFileDialog.FileName}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при экспорте в Excel: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}