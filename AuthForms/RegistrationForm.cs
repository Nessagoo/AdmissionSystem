using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            ApplyCustomStyle();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            LoadEducationLevels();
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();

                    // Начало транзакции
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Создаем пользователя
                            string userSql = @"INSERT INTO users (login, password, role, email, full_name, phone) 
                                            VALUES (@login, @password, 'applicant', @email, @fullName, @phone)
                                            RETURNING id";

                            int userId;
                            using (var cmd = new NpgsqlCommand(userSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text.Trim());
                                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());

                                userId = (int)cmd.ExecuteScalar();
                            }

                            // 2. Создаем запись абитуриента
                            string applicantSql = @"INSERT INTO applicants (user_id, passport_data, snils, parent_name, previous_education)
                                                 VALUES (@userId, @passport, @snils, @parentName, @prevEducation)";

                            using (var cmd = new NpgsqlCommand(applicantSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.Parameters.AddWithValue("@passport", txtPassport.Text.Trim());
                                cmd.Parameters.AddWithValue("@snils", txtSnils.Text.Trim());
                                cmd.Parameters.AddWithValue("@parentName", txtParentName.Text.Trim());
                                cmd.Parameters.AddWithValue("@prevEducation", txtPreviousEducation.Text.Trim());

                                cmd.ExecuteNonQuery();
                            }

                            // 3. Создаем заявление
                            string applicationSql = @"INSERT INTO applications (applicant_id, education_level_id, status)
                                                   VALUES (@applicantId, @eduLevelId, 'submitted')
                                                   RETURNING id";

                            int applicationId;
                            using (var cmd = new NpgsqlCommand(applicationSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@applicantId", userId);
                                cmd.Parameters.AddWithValue("@eduLevelId", (int)cbEducationLevel.SelectedValue);

                                applicationId = (int)cmd.ExecuteScalar();
                            }

                            // Фиксация транзакции
                            transaction.Commit();

                            MessageBox.Show("Регистрация успешно завершена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Введите корректный email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Введите паспортные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSnils.Text))
            {
                MessageBox.Show("Введите СНИЛС", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtParentName.Text))
            {
                MessageBox.Show("Введите ФИО родителя/опекуна", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPreviousEducation.Text))
            {
                MessageBox.Show("Введите оконченное учебное заведение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void ApplyCustomStyle()
        {
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            // Общий стиль всех меток
            foreach (var control in this.Controls)
            {
                if (control is Label lbl)
                {
                    lbl.ForeColor = Color.DimGray;
                }

                if (control is TextBox txt)
                {
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                }

                if (control is ComboBox cb)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                    cb.BackColor = Color.White;
                    cb.FlatStyle = FlatStyle.Flat;
                }

                if (control is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }

            // Кнопки
            btnRegister.BackColor = Color.SeaGreen;
            btnRegister.ForeColor = Color.White;

            btnCancel.BackColor = Color.LightGray;
            btnCancel.ForeColor = Color.Black;
        }
    }
}