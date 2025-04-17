using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class ApplicationForm : Form
    {
        private readonly int _userId;
        private int? _educationLevelId;
        private readonly List<ProgramItem> _selectedPrograms = new List<ProgramItem>();
        private readonly List<ExamResult> _examResults = new List<ExamResult>();
        private string _documentPath;
        private readonly IApplicationService _applicationService;

        public ApplicationForm(int userId) : this(userId, new ApplicationService())
        {
        }

        // Конструктор для тестирования с внедрением зависимости
        internal ApplicationForm(int userId, IApplicationService applicationService)
        {
            InitializeComponent();
            _userId = userId;
            _applicationService = applicationService;
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            LoadEducationLevels();
            LoadSubjects();
            ApplyModernStyle();
        }

        private void LoadEducationLevels()
        {
            try
            {
                var educationLevels = _applicationService.GetEducationLevels();
                cbEducationLevel.DisplayMember = "name";
                cbEducationLevel.ValueMember = "id";
                cbEducationLevel.DataSource = educationLevels;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке уровней образования: {ex.Message}");
            }
        }

        private void LoadSubjects()
        {
            try
            {
                cbSubject.Items.Clear();
                cbSubject.Items.AddRange(_applicationService.GetSubjects());
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке предметов: {ex.Message}");
            }
        }

        private void cbEducationLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEducationLevel.SelectedValue != null && int.TryParse(cbEducationLevel.SelectedValue.ToString(), out int levelId))
            {
                _educationLevelId = levelId;
            }
        }

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            if (!_educationLevelId.HasValue)
            {
                ShowWarning("Сначала выберите уровень образования");
                return;
            }

            try
            {
                var programs = _applicationService.GetStudyPrograms(_educationLevelId.Value);
                using (var form = new SelectProgramForm(programs))
                {
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedProgram != null)
                    {
                        if (!_selectedPrograms.Exists(p => p.Id == form.SelectedProgram.Id))
                        {
                            _selectedPrograms.Add(form.SelectedProgram);
                            RefreshProgramsGrid();
                        }
                        else
                        {
                            ShowInfo("Это направление уже добавлено");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при загрузке направлений подготовки: {ex.Message}");
            }
        }

        private void RefreshProgramsGrid()
        {
            dgvPrograms.Rows.Clear();
            for (int i = 0; i < _selectedPrograms.Count; i++)
            {
                dgvPrograms.Rows.Add(i + 1, _selectedPrograms[i].Name);
            }
        }

        private void btnRemoveProgram_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.SelectedRows.Count > 0)
            {
                int index = dgvPrograms.SelectedRows[0].Index;
                if (index >= 0 && index < _selectedPrograms.Count)
                {
                    _selectedPrograms.RemoveAt(index);
                    RefreshProgramsGrid();
                }
            }
            else
            {
                ShowWarning("Выберите направление для удаления");
            }
        }

        private void btnAddScore_Click(object sender, EventArgs e)
        {
            if (cbSubject.SelectedItem == null)
            {
                ShowWarning("Выберите предмет");
                return;
            }

            if (!int.TryParse(txtScore.Text, out int score) || score < 0 || score > 100)
            {
                ShowWarning("Введите корректное количество баллов (0-100)");
                return;
            }

            string subject = cbSubject.SelectedItem.ToString();
            if (!_examResults.Exists(r => r.Subject == subject))
            {
                _examResults.Add(new ExamResult { Subject = subject, Score = score });
                RefreshScoresGrid();
            }
            else
            {
                ShowInfo("Результаты по этому предмету уже добавлены");
            }
        }

        private void RefreshScoresGrid()
        {
            dgvScores.Rows.Clear();
            foreach (var result in _examResults)
            {
                dgvScores.Rows.Add(result.Subject, result.Score);
            }
        }

        private void btnRemoveScore_Click(object sender, EventArgs e)
        {
            if (dgvScores.SelectedRows.Count > 0)
            {
                int index = dgvScores.SelectedRows[0].Index;
                if (index >= 0 && index < _examResults.Count)
                {
                    _examResults.RemoveAt(index);
                    RefreshScoresGrid();
                }
            }
            else
            {
                ShowWarning("Выберите результат для удаления");
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _documentPath = openFileDialog1.FileName;
                txtDocumentPath.Text = Path.GetFileName(_documentPath);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateApplication())
                return;

            try
            {
                var applicationData = new ApplicationData
                {
                    UserId = _userId,
                    EducationLevelId = _educationLevelId.Value,
                    DocumentPath = _documentPath,
                    Programs = _selectedPrograms,
                    ExamResults = _examResults
                };

                _applicationService.SubmitApplication(applicationData);

                ShowInfo("Заявление успешно подано!");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка при подаче заявления: {ex.Message}");
            }
        }

        private bool ValidateApplication()
        {
            if (!_educationLevelId.HasValue)
            {
                ShowWarning("Выберите уровень образования");
                return false;
            }

            if (_selectedPrograms.Count == 0)
            {
                ShowWarning("Добавьте хотя бы одно направление подготовки");
                return false;
            }

            if (_examResults.Count == 0)
            {
                ShowWarning("Добавьте результаты экзаменов или средний балл");
                return false;
            }

            if (string.IsNullOrEmpty(_documentPath))
            {
                ShowWarning("Загрузите документ об образовании");
                return false;
            }

            // Валидация в зависимости от уровня образования
            var validator = ApplicationValidatorFactory.GetValidator(_educationLevelId.Value);
            if (!validator.Validate(_examResults, out string errorMessage))
            {
                ShowWarning(errorMessage);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ApplyModernStyle()
        {
            // Общие стили формы
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            // Стили TabControl и TabPages
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.Font = new Font("Segoe UI", 9F);

            foreach (TabPage page in tabControl1.TabPages)
            {
                page.BackColor = Color.White;
            }

            // Стили Label
            foreach (Control tab in tabControl1.Controls)
            {
                foreach (Control control in tab.Controls)
                {
                    if (control is Label label)
                    {
                        label.Font = new Font("Segoe UI", 9F);
                        label.ForeColor = Color.Black;
                    }
                }
            }

            // Стили ComboBox
            cbEducationLevel.Font = cbSubject.Font = new Font("Segoe UI", 9F);

            // Стили TextBox
            txtScore.Font = txtDocumentPath.Font = new Font("Segoe UI", 9F);

            // GroupBox
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Кнопки
            Action<Button> styleButton = (btn) =>
            {
                btn.BackColor = Color.WhiteSmoke;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 9F);
                btn.FlatAppearance.BorderColor = Color.Silver;
                btn.FlatAppearance.BorderSize = 1;
            };

            var allButtons = new Button[]
            {
        btnAddProgram, btnRemoveProgram,
        btnAddScore, btnRemoveScore,
        btnUpload, btnSubmit, btnCancel
            };

            foreach (var btn in allButtons)
                styleButton(btn);

            // DataGridView стиль
            Action<DataGridView> styleGrid = (grid) =>
            {
                grid.BackgroundColor = Color.White;
                grid.GridColor = Color.LightGray;
                grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
                grid.EnableHeadersVisualStyles = false;
            };

            styleGrid(dgvPrograms);
            styleGrid(dgvScores);
        }
    }

    // Классы для работы с данными
    public class ProgramItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ExamResult
    {
        public string Subject { get; set; }
        public int Score { get; set; }
    }

    public class ApplicationData
    {
        public int UserId { get; set; }
        public int EducationLevelId { get; set; }
        public string DocumentPath { get; set; }
        public List<ProgramItem> Programs { get; set; }
        public List<ExamResult> ExamResults { get; set; }
    }

    // Интерфейсы и реализации сервисов
    public interface IApplicationService
    {
        DataTable GetEducationLevels();
        object[] GetSubjects();
        DataTable GetStudyPrograms(int educationLevelId);
        void SubmitApplication(ApplicationData applicationData);
    }

    public class ApplicationService : IApplicationService
    {
        public DataTable GetEducationLevels()
        {
            using (var conn = DBManager.GetConnection())
            {
                conn.Open();
                const string sql = "SELECT id, name FROM education_levels ORDER BY id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    new NpgsqlDataAdapter(cmd).Fill(dt);
                    return dt;
                }
            }
        }

        public object[] GetSubjects()
        {
            return new object[] {
                "Русский язык",
                "Математика",
                "Физика",
                "Химия",
                "Биология",
                "История",
                "Обществознание",
                "Информатика",
                "Иностранный язык",
                "Литература",
                "География"
            };
        }

        public DataTable GetStudyPrograms(int educationLevelId)
        {
            using (var conn = DBManager.GetConnection())
            {
                conn.Open();
                const string sql = "SELECT id, name, code FROM study_programs WHERE education_level_id = @levelId ORDER BY name";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@levelId", educationLevelId);
                    DataTable dt = new DataTable();
                    new NpgsqlDataAdapter(cmd).Fill(dt);
                    return dt;
                }
            }
        }

        public void SubmitApplication(ApplicationData applicationData)
        {
            using (var conn = DBManager.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Создаем заявление
                        const string applicationSql = @"INSERT INTO applications 
                                                    (applicant_id, education_level_id, status, document_path) 
                                                    VALUES (@applicantId, @eduLevelId, 'submitted', @docPath)
                                                    RETURNING id";

                        int applicationId;
                        using (var cmd = new NpgsqlCommand(applicationSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@applicantId", applicationData.UserId);
                            cmd.Parameters.AddWithValue("@eduLevelId", applicationData.EducationLevelId);
                            cmd.Parameters.AddWithValue("@docPath", applicationData.DocumentPath);
                            applicationId = (int)cmd.ExecuteScalar();
                        }

                        // 2. Добавляем выбранные направления
                        for (int i = 0; i < applicationData.Programs.Count; i++)
                        {
                            const string programSql = @"INSERT INTO application_programs 
                                                     (application_id, program_id, priority) 
                                                     VALUES (@appId, @progId, @priority)";

                            using (var cmd = new NpgsqlCommand(programSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@appId", applicationId);
                                cmd.Parameters.AddWithValue("@progId", applicationData.Programs[i].Id);
                                cmd.Parameters.AddWithValue("@priority", i + 1);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 3. Добавляем результаты экзаменов
                        foreach (var result in applicationData.ExamResults)
                        {
                            const string examSql = @"INSERT INTO exam_results 
                                                   (application_id, subject, score) 
                                                   VALUES (@appId, @subject, @score)";

                            using (var cmd = new NpgsqlCommand(examSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@appId", applicationId);
                                cmd.Parameters.AddWithValue("@subject", result.Subject);
                                cmd.Parameters.AddWithValue("@score", result.Score);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Копируем документ в папку приложения
                        string appDocumentsPath = Path.Combine(Application.StartupPath, "Documents");
                        if (!Directory.Exists(appDocumentsPath))
                            Directory.CreateDirectory(appDocumentsPath);

                        string destPath = Path.Combine(appDocumentsPath, $"{applicationId}_{Path.GetFileName(applicationData.DocumentPath)}");
                        File.Copy(applicationData.DocumentPath, destPath, true);

                        // Обновляем путь в базе данных
                        const string updateSql = "UPDATE applications SET document_path = @docPath WHERE id = @appId";
                        using (var cmd = new NpgsqlCommand(updateSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@docPath", destPath);
                            cmd.Parameters.AddWithValue("@appId", applicationId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }

    // Фабрика валидаторов
    public static class ApplicationValidatorFactory
    {
        public static IApplicationValidator GetValidator(int educationLevelId)
        {
            return educationLevelId switch
            {
                1 => new SecondaryEducationValidator(), // СПО
                4 => new MasterDegreeValidator(),      // Магистратура
                _ => new BachelorDegreeValidator()    // Бакалавриат/Специалитет
            };
        }
    }

    public interface IApplicationValidator
    {
        bool Validate(List<ExamResult> examResults, out string errorMessage);
    }

    public class SecondaryEducationValidator : IApplicationValidator
    {
        public bool Validate(List<ExamResult> examResults, out string errorMessage)
        {
            if (examResults.Count != 1 || examResults[0].Subject != "Средний балл аттестата")
            {
                errorMessage = "Для СПО укажите средний балл аттестата";
                return false;
            }

            if (examResults[0].Score < 0 || examResults[0].Score > 5)
            {
                errorMessage = "Средний балл аттестата должен быть от 0 до 5";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }

    public class MasterDegreeValidator : IApplicationValidator
    {
        public bool Validate(List<ExamResult> examResults, out string errorMessage)
        {
            if (examResults.Count != 1 || examResults[0].Subject != "Средний балл диплома")
            {
                errorMessage = "Для магистратуры укажите средний балл диплома";
                return false;
            }

            if (examResults[0].Score < 0 || examResults[0].Score > 100)
            {
                errorMessage = "Средний балл диплома должен быть от 0 до 100";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }

    public class BachelorDegreeValidator : IApplicationValidator
    {
        public bool Validate(List<ExamResult> examResults, out string errorMessage)
        {
            if (examResults.Count < 3)
            {
                errorMessage = "Для бакалавриата/специалитета укажите минимум 3 предмета ЕГЭ";
                return false;
            }

            foreach (var result in examResults)
            {
                if (result.Score < 0 || result.Score > 100)
                {
                    errorMessage = "Баллы ЕГЭ должны быть от 0 до 100";
                    return false;
                }
            }

            errorMessage = null;
            return true;
        }
    }
}