using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ApplyCustomStyle();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT id, role, full_name FROM users WHERE login = @login AND password = @password";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string role = reader.GetString(1);
                                string fullName = reader.GetString(2);

                                this.Hide();

                                if (role == "applicant")
                                {
                                    new ApplicantMainForm(userId, fullName).Show();
                                }
                                else if (role == "staff")
                                {
                                    new StaffMainForm(userId, fullName).Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            new RegistrationForm().ShowDialog();
        }
        private void ApplyCustomStyle()
        {
            // Общий стиль формы
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            // Стилизация меток
            label1.ForeColor = Color.DimGray;
            label2.ForeColor = Color.DimGray;

            // Стилизация текстбоксов
            txtLogin.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtLogin.BackColor = Color.White;
            txtPassword.BackColor = Color.White;

            // Кнопка "Войти"
            btnLogin.BackColor = Color.SteelBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;

            // Кнопка "Регистрация"
            btnRegister.BackColor = Color.LightGray;
            btnRegister.ForeColor = Color.Black;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
        }
    }
}