using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class AddAttestationsForm : Form
    {
        private data_base dataBase = new data_base();

        public AddAttestationsForm()
        {
            InitializeComponent();
            LoadTimeDatabase();
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана
            // Установка режима автоматического изменения размера столбцов для отображения всего содержимого
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Установка свойства AutoSizeMode для каждого столбца
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void LoadTimeDatabase()
        {
            try
            {
                dataBase.openConnection();
                string query = "SELECT Id, Meaning FROM Time";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
                dataGridView1.Columns["Id"].HeaderText = "ID времени";
                dataGridView1.Columns["Meaning"].HeaderText = "Время";

                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckTimeIdExists(int timeId)
        {
            try
            {
                dataBase.openConnection();
                string query = "SELECT COUNT(*) FROM Time WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.Parameters.AddWithValue("@Id", timeId);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при перевірці ID часу: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string attestationName = textBox1.Text;
            string timeIdString = textBoxTime.Text;

            if (!string.IsNullOrEmpty(attestationName) && !string.IsNullOrEmpty(timeIdString))
            {
                try
                {
                    int timeId = Convert.ToInt32(timeIdString);

                    if (CheckTimeIdExists(timeId))
                    {
                        dataBase.openConnection();

                        // Сохранение аттестации с указанным ID времени
                        string insertQuery = "INSERT INTO Attestations (Name, TimeId) VALUES (@Name, @TimeId)";
                        SqlCommand insertCommand = new SqlCommand(insertQuery, dataBase.getConnection());
                        insertCommand.Parameters.AddWithValue("@Name", attestationName);
                        insertCommand.Parameters.AddWithValue("@TimeId", timeId);
                        insertCommand.ExecuteNonQuery();

                        MessageBox.Show("Запис успішно додано.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Вказаний ID часу не існує.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Не вірний формат часу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Помилка при збереженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dataBase.closeConnection();
                }
            }
            else
            {
                MessageBox.Show("Заповність всі поля.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}