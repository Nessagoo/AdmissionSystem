using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class ViewApplicationForm : Form
    {
        private int applicationId;

        public ViewApplicationForm(int applicationId)
        {
            InitializeComponent();
            this.applicationId = applicationId;
        }

        private void ViewApplicationForm_Load(object sender, EventArgs e)
        {
            LoadApplicationData();
            LoadPrograms();
            LoadScores();
        }

        private void LoadApplicationData()
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT u.full_name, el.name AS education_level, 
                                 a.application_date, a.status, a.document_path
                                 FROM applications a
                                 JOIN users u ON a.applicant_id = u.id
                                 JOIN education_levels el ON a.education_level_id = el.id
                                 WHERE a.id = @appId";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@appId", applicationId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtApplicantName.Text = reader.GetString(0);
                                txtEducationLevel.Text = reader.GetString(1);
                                txtApplicationDate.Text = reader.GetDateTime(2).ToString("dd.MM.yyyy HH:mm");
                                txtStatus.Text = TranslateStatus(reader.GetString(3));

                                if (!reader.IsDBNull(4))
                                {
                                    txtDocumentPath.Text = reader.GetString(4);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных заявления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LoadPrograms()
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT ap.priority, sp.name
                                 FROM application_programs ap
                                 JOIN study_programs sp ON ap.program_id = sp.id
                                 WHERE ap.application_id = @appId
                                 ORDER BY ap.priority";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@appId", applicationId);

                        DataTable dt = new DataTable();
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dgvPrograms.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке направлений подготовки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadScores()
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT subject, score
                                 FROM exam_results
                                 WHERE application_id = @appId
                                 ORDER BY subject";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@appId", applicationId);

                        DataTable dt = new DataTable();
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dgvScores.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке результатов экзаменов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewDocument_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocumentPath.Text))
            {
                try
                {
                    Process.Start(txtDocumentPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось открыть документ: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Документ не загружен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}