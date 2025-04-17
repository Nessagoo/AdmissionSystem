using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class EditStatusForm : Form
    {
        private int applicationId;
        private string currentStatus;

        public EditStatusForm(int applicationId, string currentStatus)
        {
            InitializeComponent();
            this.applicationId = applicationId;
            this.currentStatus = currentStatus;
        }

        private string TranslateStatusToEnglish(string status)
        {
            switch (status.ToLower())
            {
                case "готово": return "ready";
                case "требуется доработка": return "revision";
                case "отклонено": return "rejected";
                case "принято": return "accepted";
                default: return status;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbNewStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите новый статус", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newStatus = TranslateStatusToEnglish(cbNewStatus.SelectedItem.ToString());
            string comments = txtComments.Text.Trim();

            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE applications SET status = @status, comments = @comments WHERE id = @appId";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@comments", string.IsNullOrEmpty(comments) ? (object)DBNull.Value : comments);
                        cmd.Parameters.AddWithValue("@appId", applicationId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Статус заявления успешно обновлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}