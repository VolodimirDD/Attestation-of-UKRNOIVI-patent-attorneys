using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class EditTimeForm : Form
    {
        private int timeId;
        private string timeName;
        private data_base dataBase = new data_base();

        // Добавляем событие для уведомления об обновлении данных
        public event EventHandler DataUpdated;
        public EditTimeForm(int id, string name)
        {
            InitializeComponent();
            timeId = id;
            timeName = name;
            textBox1.Text = timeId.ToString();
            textBox1.ReadOnly = true;
            textBox1.TextAlign = HorizontalAlignment.Center;
            LoadTimeFromDatabase();
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана

        }

        private void LoadTimeFromDatabase()
        {
            try
            {
                dataBase.openConnection();
                string query = "SELECT * FROM Time WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.Parameters.AddWithValue("@Id", timeId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string attestationValue = reader["Meaning"].ToString();
                    textBox2.Text = attestationValue;
                }

                reader.Close();
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні записів: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Метод для генерации события DataUpdated
        protected virtual void OnDataUpdated()
        {
            EventHandler handler = DataUpdated;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newValue = textBox2.Text;

            // Проверяем, является ли поле пустым
            if (string.IsNullOrEmpty(newValue))
            {
                MessageBox.Show("Будь ласка, заповність всі поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Прерываем выполнение метода
            }

            try
            {
                dataBase.openConnection();
                string query = "UPDATE Time SET Meaning = @Name WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.Parameters.AddWithValue("@Name", newValue);
                command.Parameters.AddWithValue("@Id", timeId);
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

        private void EditTimeForm_Load(object sender, EventArgs e)
        {
            LoadTimeFromDatabase();
        }
    }
}