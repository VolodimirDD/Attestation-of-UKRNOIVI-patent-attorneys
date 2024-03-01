using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class EditAttestationsForm : Form
    {
        private int attestationId;
        private string attestationName;
        private data_base dataBase = new data_base();

        // Добавляем событие для уведомления об обновлении данных
        public event EventHandler DataUpdated;

        public EditAttestationsForm(int id, string name)
        {
            InitializeComponent();
            attestationId = id;
            attestationName = name;
            textBox1.Text = attestationId.ToString();
            textBox1.ReadOnly = true;
            textBox1.TextAlign = HorizontalAlignment.Center;
            LoadAttestationFromDatabase();
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
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["Meaning"].HeaderText = "Значення";

                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void LoadAttestationFromDatabase()
        {
            try
            {
                dataBase.openConnection();
                string query = "SELECT * FROM Attestations WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.Parameters.AddWithValue("@Id", attestationId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string attestationValue = reader["Name"].ToString();
                    textBox2.Text = attestationValue;
                }

                reader.Close();
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Метод для генерации события DataUpdated
        protected virtual void OnDataUpdated()
        {
            EventHandler handler = DataUpdated;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Проверка на заполненную информацию в textBox2 и textBoxTime
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBoxTime.Text))
            {
                MessageBox.Show("Заповність всі значення.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка на существующий ID времени при нажатии на кнопку btnSave
            int timeId;
            if (int.TryParse(textBoxTime.Text, out timeId))
            {
                if (CheckTimeIdExists(timeId))
                {
                    try
                    {
                        dataBase.openConnection();
                        string query = "UPDATE Attestations SET Name = @Name, TimeId = @TimeId WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                        command.Parameters.AddWithValue("@Name", textBox2.Text);
                        command.Parameters.AddWithValue("@TimeId", timeId);
                        command.Parameters.AddWithValue("@Id", attestationId);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Зміни успішно збережено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dataBase.closeConnection();

                        // Генерируем событие DataUpdated для уведомления об обновлении данных
                        OnDataUpdated();

                        // Закрываем форму
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при збереженні змін: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Введений ідентифікатор часу не існує.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Введіть дійсний ідентифікатор часу.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
       
        private void EditAttestationsForm_Load(object sender, EventArgs e)
        {
            LoadTimeDatabase();
            LoadAttestationFromDatabase();
        }

        
    }
}