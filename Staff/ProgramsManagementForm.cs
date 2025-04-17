using AdmissionSystem.Database;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace AdmissionSystem
{
    public partial class ProgramsManagementForm : Form
    {
        public ProgramsManagementForm()
        {
            InitializeComponent();
        }

        private void ProgramsManagementForm_Load(object sender, EventArgs e)
        {
            LoadPrograms();
        }

        private void LoadPrograms()
        {
            try
            {
                using (var conn = DBManager.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT sp.id, el.name AS education_level, sp.name, sp.code
                                FROM study_programs sp
                                JOIN education_levels el ON sp.education_level_id = el.id
                                ORDER BY el.id, sp.name";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new EditProgramForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPrograms();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.SelectedRows.Count > 0)
            {
                int programId = (int)dgvPrograms.SelectedRows[0].Cells["Id"].Value;
                var form = new EditProgramForm(programId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPrograms();
                }
            }
            else
            {
                MessageBox.Show("Выберите направление для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPrograms.SelectedRows.Count > 0)
            {
                int programId = (int)dgvPrograms.SelectedRows[0].Cells["Id"].Value;
                string programName = dgvPrograms.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show($"Вы уверены, что хотите удалить направление \"{programName}\"?",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        using (var conn = DBManager.GetConnection())
                        {
                            conn.Open();
                            string sql = "DELETE FROM study_programs WHERE id = @id";

                            using (var cmd = new NpgsqlCommand(sql, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", programId);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Направление успешно удалено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadPrograms();
                                }
                            }
                        }
                    }
                    catch (Npgsql.PostgresException ex) when (ex.SqlState == "23503")
                    {
                        MessageBox.Show("Невозможно удалить направление, так как оно используется в заявлениях", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении направления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите направление для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}