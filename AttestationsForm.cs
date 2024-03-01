using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class AttestationsForm : Form
    {
        data_base dataBase = new data_base();
        
        public AttestationsForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; 
            dataGridView1.ReadOnly = true; // Установка свойства ReadOnly для DataGridView 
            dataGridView1.AllowUserToDeleteRows = false; // Запрещаем удаление строк                                             
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана
            // Определение столбцов в DataGridView
            dataGridView1.ColumnCount = 3; // Устанавливаем количество столбцов
            dataGridView1.Columns[0].Name = "Порядковий номер"; // Задаем имя первого столбца
            dataGridView1.Columns[1].Name = "Найменування"; // Задаем имя второго столбца
            dataGridView1.Columns[2].Name = "Час проходження"; // Задаем имя третьего столбца
            // Установка режима автоматического изменения размера столбцов для отображения всего содержимого
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Установка свойства AutoSizeMode для каждого столбца
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void LoadAttestationsFromDatabase()
        {

            try
            {
                dataBase.openConnection();
                string query = "SELECT Attestations.Id, Attestations.Name, Time.Meaning AS Time FROM Attestations INNER JOIN Time ON Attestations.TimeId = Time.Id";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    // Очищаем DataGridView перед загрузкой данных
                    dataGridView1.Rows.Clear();

                    // Заполняем DataGridView данными из таблицы
                    foreach (DataRow row in table.Rows)
                    {
                        int attestationId = Convert.ToInt32(row["Id"]);
                        string attestationName = row["Name"].ToString();
                        string time = row["Time"].ToString();

                        // Добавляем новую строку в DataGridView
                        dataGridView1.Rows.Add(attestationId, attestationName, time);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void AttestationsForm_Load(object sender, EventArgs e)
        {
            LoadAttestationsFromDatabase();
            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения

        }

        private void Add_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления новой аттестации
            AddAttestationsForm form = new AddAttestationsForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // При успешном добавлении обновляем список аттестаций
                LoadAttestationsFromDatabase();
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем выбранную аттестацию
                int attestationId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Порядковий номер"].Value);
                string attestationName = dataGridView1.SelectedRows[0].Cells["Найменування"].Value.ToString();

                // Создаем новый экземпляр формы редактирования и отображаем его
                using (EditAttestationsForm editForm = new EditAttestationsForm(attestationId, attestationName))
                {
                    // Подписываемся на событие DataUpdated
                    editForm.DataUpdated += EditForm_DataUpdated;

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Обновляем отображение после редактирования аттестации
                        LoadAttestationsFromDatabase();
                    }

                    // Отписываемся от события DataUpdated
                    editForm.DataUpdated -= EditForm_DataUpdated;
                }
            }
            else
            {
                MessageBox.Show("Виберіть атестацію для редагування.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Обработчик события DataUpdated
        private void EditForm_DataUpdated(object sender, EventArgs e)
        {
            LoadAttestationsFromDatabase();
        }


        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int attestationId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Порядковий номер"].Value);
                string attestationName = dataGridView1.SelectedRows[0].Cells["Найменування"].Value.ToString();

                // Запрос подтверждения удаления аттестации
                DialogResult result = MessageBox.Show($"Ви впевнені, що бажаєте видалити атестацію '{attestationName}'?", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataBase.openConnection();
                        string query = "DELETE FROM Attestations WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                        command.Parameters.AddWithValue("@Id", attestationId);
                        command.ExecuteNonQuery();

                        // При успешном удалении обновляем список аттестаций
                        LoadAttestationsFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при видаленні атестації: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        dataBase.closeConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть атестацію для видалення.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Back_Click_1(object sender, EventArgs e)
        {
            AdminForm admform = new AdminForm();
            admform.Show();
            this.Hide();
        }

        private void AttestationsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Завершаем работу программы
                Application.Exit();
            }
        }
    }
}