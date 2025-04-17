using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class ApplicantMainForm : Form
    {
        private readonly int _userId;
        private readonly string _fullName;
        private readonly IApplicationRepository _applicationRepository;

        public ApplicantMainForm(int userId, string fullName)
            : this(userId, fullName, new ApplicationRepository())
        {
        }

        // Конструктор для тестирования, позволяющий внедрить зависимость
        internal ApplicantMainForm(int userId, string fullName, IApplicationRepository applicationRepository)
        {
            InitializeComponent();
            _userId = userId;
            _fullName = fullName;
            _applicationRepository = applicationRepository;
        }

        private void ApplicantMainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Добро пожаловать, {_fullName}!";
            UpdateApplicationStatus();
            ApplyModernStyle();
        }

        private void UpdateApplicationStatus()
        {
            try
            {
                var status = _applicationRepository.GetLatestApplicationStatus(_userId);
                lblStatus.Text = status == null
                    ? "У вас нет поданных заявлений"
                    : $"Статус заявления: {TranslateStatus(status)}";
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при получении статуса заявления: {ex.Message}");
            }
        }

        private string TranslateStatus(string status)
        {
            return status.ToLower() switch
            {
                "submitted" => "Подано",
                "ready" => "Готово",
                "revision" => "Требуется доработка",
                "rejected" => "Отклонено",
                "accepted" => "Принято",
                _ => status
            };
        }

        private void податьЗаявлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new ApplicationForm(_userId))
            {
                form.ShowDialog();
            }
            UpdateApplicationStatus();
        }

        private void просмотретьЗаявлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var applicationId = _applicationRepository.GetLatestApplicationId(_userId);

                if (applicationId.HasValue)
                {
                    using (var form = new ViewApplicationForm(applicationId.Value))
                    {
                        form.ShowDialog();
                    }
                }
                else
                {
                    ShowInfo("У вас нет поданных заявлений");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при просмотре заявления: {ex.Message}");
            }
        }

        private void распечататьЗаявлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var applicationInfo = _applicationRepository.GetApplicationInfoForPdf(_userId);

                if (applicationInfo != null)
                {
                    GenerateApplicationPdf(
                        applicationInfo.ApplicantName,
                        applicationInfo.Programs,
                        applicationInfo.ApplicationDate);
                }
                else
                {
                    ShowInfo("У вас нет поданных заявлений");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при генерации PDF: {ex.Message}");
            }
        }

        private void GenerateApplicationPdf(string applicantName, List<string> programs, DateTime applicationDate)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
                saveFileDialog.Title = "Сохранить заявление как PDF";
                saveFileDialog.FileName = $"Заявление_{applicantName}_{applicationDate:yyyyMMdd}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Здесь должна быть логика генерации PDF
                        ShowInfo($"PDF файл успешно сохранен: {saveFileDialog.FileName}");
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Ошибка при сохранении PDF: {ex.Message}");
                    }
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyModernStyle()
        {
            // Общие стили формы
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // lblWelcome стили
            lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(0, 120, 215); // Windows-синий
            lblWelcome.Text = "Добро пожаловать в систему подачи заявлений!";
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point((this.ClientSize.Width - lblWelcome.Width) / 2, 80);

            // MenuStrip стиль
            menuStrip1.BackColor = Color.WhiteSmoke;
            menuStrip1.RenderMode = ToolStripRenderMode.System;
            menuStrip1.Font = new Font("Segoe UI", 9F);

            foreach (ToolStripMenuItem item in menuStrip1.Items)
                item.ForeColor = Color.Black;

            foreach (ToolStripMenuItem item in заявлениеToolStripMenuItem.DropDownItems)
            {
                item.BackColor = Color.White;
                item.ForeColor = Color.Black;
                item.Font = new Font("Segoe UI", 9F);
            }

            // StatusStrip стиль
            statusStrip1.BackColor = Color.WhiteSmoke;
            lblStatus.ForeColor = Color.Gray;
            lblStatus.Text = "Готово";
        }
    }

    // Интерфейс и реализация для работы с заявлениями
    public interface IApplicationRepository
    {
        string GetLatestApplicationStatus(int userId);
        int? GetLatestApplicationId(int userId);
        ApplicationInfo GetApplicationInfoForPdf(int userId);
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public string GetLatestApplicationStatus(int userId)
        {
            using (var conn = DBManager.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT status FROM applications 
                                   WHERE applicant_id = @userId 
                                   ORDER BY application_date DESC 
                                   LIMIT 1";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    return cmd.ExecuteScalar()?.ToString();
                }
            }
        }

        public int? GetLatestApplicationId(int userId)
        {
            using (var conn = DBManager.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT id FROM applications 
                                  WHERE applicant_id = @userId 
                                  ORDER BY application_date DESC 
                                  LIMIT 1";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    return cmd.ExecuteScalar() as int?;
                }
            }
        }

        public ApplicationInfo GetApplicationInfoForPdf(int userId)
        {
            using (var conn = DBManager.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT a.id, u.full_name, a.application_date
                                  FROM applications a
                                  JOIN users u ON a.applicant_id = u.id
                                  WHERE a.applicant_id = @userId
                                  ORDER BY a.application_date DESC
                                  LIMIT 1";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var applicationId = reader.GetInt32(0);
                            var applicantName = reader.GetString(1);
                            var applicationDate = reader.GetDateTime(2);

                            var programs = GetApplicationPrograms(conn, applicationId);
                            return new ApplicationInfo(applicantName, programs, applicationDate);
                        }
                    }
                }
            }
            return null;
        }

        private List<string> GetApplicationPrograms(NpgsqlConnection conn, int applicationId)
        {
            var programs = new List<string>();
            const string programsSql = @"SELECT sp.name 
                                      FROM application_programs ap
                                      JOIN study_programs sp ON ap.program_id = sp.id
                                      WHERE ap.application_id = @appId
                                      ORDER BY ap.priority";

            using (var programsCmd = new NpgsqlCommand(programsSql, conn))
            {
                programsCmd.Parameters.AddWithValue("@appId", applicationId);
                using (var programsReader = programsCmd.ExecuteReader())
                {
                    while (programsReader.Read())
                    {
                        programs.Add(programsReader.GetString(0));
                    }
                }
            }
            return programs;
        }

    }

    public class ApplicationInfo
    {
        public string ApplicantName { get; }
        public List<string> Programs { get; }
        public DateTime ApplicationDate { get; }

        public ApplicationInfo(string applicantName, List<string> programs, DateTime applicationDate)
        {
            ApplicantName = applicantName;
            Programs = programs;
            ApplicationDate = applicationDate;
        }
    }

}