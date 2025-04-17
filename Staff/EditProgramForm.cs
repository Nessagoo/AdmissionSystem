using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class EditProgramForm : Form
    {
        private int? programId;

        public EditProgramForm()
        {
            InitializeComponent();
        }

        public EditProgramForm(int programId) : this()
        {
            this.programId = programId;
        }

        private void EditProgramForm_Load(object sender, EventArgs e)
        {
            LoadEducationLevels();

            if (programId.HasValue)
            {
                LoadProgramData();
            }
        }

        private void LoadEducationLevels()
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT id, name FROM education_levels ORDER BY id";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            cbEducationLevel.DisplayMember = "name";
                            cbEducationLevel.ValueMember = "id";
                            cbEducationLevel.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке уровней образования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProgramData()
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT education_level_id, name, code 
                                FROM study_programs 
                                WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", programId.Value);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cbEducationLevel.SelectedValue = reader.GetInt32(0);
                                txtName.Text = reader.GetString(1);
                                txtCode.Text = reader.GetString(2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных направления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();

                    if (programId.HasValue)
                    {
                        // Обновление существующего направления
                        string updateSql = @"UPDATE study_programs 
                                          SET education_level_id = @eduLevelId, 
                                              name = @name, 
                                              code = @code
                                          WHERE id = @id";

                        using (var cmd = new NpgsqlCommand(updateSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@eduLevelId", (int)cbEducationLevel.SelectedValue);
                            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                            cmd.Parameters.AddWithValue("@code", txtCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@id", programId.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Добавление нового направления
                        string insertSql = @"INSERT INTO study_programs 
                                          (education_level_id, name, code) 
                                          VALUES (@eduLevelId, @name, @code)";

                        using (var cmd = new NpgsqlCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@eduLevelId", (int)cbEducationLevel.SelectedValue);
                            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                            cmd.Parameters.AddWithValue("@code", txtCode.Text.Trim());

                            cmd.ExecuteNonQuery();
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении направления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cbEducationLevel.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите уровень образования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название направления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Введите код направления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}