using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class ApplicationsListForm : Form
    {
        public ApplicationsListForm()
        {
            InitializeComponent();
        }

        private void ApplicationsListForm_Load(object sender, EventArgs e)
        {
            LoadApplications();
            cbStatusFilter.SelectedIndex = 0;
            dtpDateFilter.Value = DateTime.Today;
        }

        private void LoadApplications(string statusFilter = null, DateTime? dateFilter = null)
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT a.id, u.full_name AS applicant_name, el.name AS education_level, 
                                 a.application_date, a.status, a.document_path
                                 FROM applications a
                                 JOIN users u ON a.applicant_id = u.id
                                 JOIN education_levels el ON a.education_level_id = el.id
                                 WHERE 1=1";

                    if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "Все")
                    {
                        sql += " AND a.status = @status";
                    }

                    if (dateFilter.HasValue)
                    {
                        sql += " AND DATE(a.application_date) = @appDate";
                    }

                    sql += " ORDER BY a.application_date DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "Все")
                        {
                            cmd.Parameters.AddWithValue("@status", TranslateStatusToEnglish(statusFilter));
                        }

                        if (dateFilter.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@appDate", dateFilter.Value.Date);
                        }

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Переводим статусы на русский
                            foreach (DataRow row in dt.Rows)
                            {
                                row["status"] = TranslateStatus(row["status"].ToString());
                            }

                            dgvApplications.DataSource = dt;

                            // Настраиваем заголовки столбцов
                            if (dgvApplications.Columns.Count > 0)
                            {
                                dgvApplications.Columns["id"].HeaderText = "№";
                                dgvApplications.Columns["applicant_name"].HeaderText = "Абитуриент";
                                dgvApplications.Columns["education_level"].HeaderText = "Уровень образования";
                                dgvApplications.Columns["application_date"].HeaderText = "Дата подачи";
                                dgvApplications.Columns["status"].HeaderText = "Статус";
                                dgvApplications.Columns["document_path"].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заявлений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TranslateStatus(string status)
        {
            switch (status.ToLower())
            {
                case "submitted": return "Подано";
                case "ready": return "Готово";
                case "revision": return "Требуется доработка";
                case "rejected": return "Отклонено";
                case "accepted": return "Принято";
                default: return status;
            }
        }

        private string TranslateStatusToEnglish(string status)
        {
            switch (status.ToLower())
            {
                case "подано": return "submitted";
                case "готово": return "ready";
                case "требуется доработка": return "revision";
                case "отклонено": return "rejected";
                case "принято": return "accepted";
                default: return status;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvApplications.SelectedRows.Count > 0)
            {
                int applicationId = (int)dgvApplications.SelectedRows[0].Cells["id"].Value;
                new ViewApplicationForm(applicationId).ShowDialog();
                LoadApplications(); // Обновляем список после просмотра
            }
            else
            {
                MessageBox.Show("Выберите заявление для просмотра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvApplications.SelectedRows.Count > 0)
            {
                int applicationId = (int)dgvApplications.SelectedRows[0].Cells["id"].Value;
                string currentStatus = dgvApplications.SelectedRows[0].Cells["status"].Value.ToString();
                new EditStatusForm(applicationId, currentStatus).ShowDialog();
                LoadApplications(); // Обновляем список после изменения статуса
            }
            else
            {
                MessageBox.Show("Выберите заявление для изменения статуса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string statusFilter = cbStatusFilter.SelectedItem.ToString();
            DateTime? dateFilter = dtpDateFilter.Checked ? (DateTime?)dtpDateFilter.Value.Date : null;
            LoadApplications(statusFilter, dateFilter);
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            cbStatusFilter.SelectedIndex = 0;
            dtpDateFilter.Value = DateTime.Today;
            dtpDateFilter.Checked = false;
            LoadApplications();
        }
    }
}