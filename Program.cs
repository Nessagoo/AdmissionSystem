using AdmissionSystem.Database;
using System;
using System.Windows.Forms;

namespace AdmissionSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Проверка подключения к базе данных
            try
            {
                DBManager.TestConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось подключиться к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new LoginForm());
        }
    }
}