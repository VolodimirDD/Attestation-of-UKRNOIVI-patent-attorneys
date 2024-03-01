using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class TimeForm : Form
    {
        data_base dataBase = new data_base();

        public TimeForm()
        {
            InitializeComponent();
            

            // Установка свойства ReadOnly для DataGridView
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToDeleteRows = false; // Запрещаем удаление строк
            this.FormBorderStyle = FormBorderStyle.Fixed3D;// Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen;// Устанавливаем форму по центру экрана
            // Установка режима автоматического изменения размера столбцов для отображения всего содержимого
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Установка свойства AutoSizeMode для каждого столбца
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void LoadTimeFromDatabase()
        {

            try
            {
                dataBase.openConnection();
                string query = "SELECT * FROM Time";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Очищаем DataGridView перед загрузкой данных
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                if (table.Rows.Count > 0)
                {
                    // Загружаем данные в DataGridView
                    dataGridView1.DataSource = table;

                    // Изменяем названия столбцов
                    dataGridView1.Columns["Id"].HeaderText = "Порядковий номер";
                    dataGridView1.Columns["Meaning"].HeaderText = "Час";
                }

                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка :" + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Обработчик события DataUpdated
        private void EditForm_DataUpdated(object sender, EventArgs e)
        {
            LoadTimeFromDatabase();
        }

        private void TimeForm_Load(object sender, EventArgs e)
        {
            LoadTimeFromDatabase();
            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем выбранную аттестацию
                int timeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                string timeName = dataGridView1.SelectedRows[0].Cells["Meaning"].Value.ToString();

                // Создаем новый экземпляр формы редактирования и отображаем его
                using (EditTimeForm editForm = new EditTimeForm(timeId, timeName))
                {
                    // Подписываемся на событие DataUpdated
                    editForm.DataUpdated += EditForm_DataUpdated;

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Обновляем отображение после редактирования аттестации
                        LoadTimeFromDatabase();
                    }

                    // Отписываемся от события DataUpdated
                    editForm.DataUpdated -= EditForm_DataUpdated;
                }
            }
            else
            {
                MessageBox.Show("Виберіть запис для редагування.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int timeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                string timeName = dataGridView1.SelectedRows[0].Cells["Meaning"].Value.ToString();

                // Запрос подтверждения удаления аттестации
                DialogResult result = MessageBox.Show($"Ви впевнені, що бажаєте видалити запис '{timeName}'?", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataBase.openConnection();
                        string query = "DELETE FROM Time WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                        command.Parameters.AddWithValue("@Id", timeId);
                        command.ExecuteNonQuery();

                        // При успешном удалении обновляем список аттестаций
                        LoadTimeFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при видаленні запису: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        dataBase.closeConnection();
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть запис для видалення.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления новой аттестации
            AddTimeForm form = new AddTimeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // При успешном добавлении обновляем список аттестаций
                LoadTimeFromDatabase();
            }
        }

        private void TimeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Завершаем работу программы
                Application.Exit();
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            AdminForm admform = new AdminForm();
            admform.Show();
            this.Hide();
        }
    }
}