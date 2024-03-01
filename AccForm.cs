using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Атестація
{
    public partial class AccForm : Form
    {
        data_base dataBase = new data_base();

        public AccForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана
            dataGridView1.ReadOnly = true; // Установка свойства ReadOnly для DataGridView
            dataGridView1.AllowUserToDeleteRows = false; // Запрещаем удаление строк
            
            // Установка режима автоматического изменения размера столбцов для отображения всего содержимого
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Установка свойства AutoSizeMode для каждого столбца
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void AccForm_Load(object sender, EventArgs e)
        {
            LoadUserAccounts();
            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения
        }

        private void LoadUserAccounts()
        {
            try
            {
                dataBase.openConnection();
                string query = "SELECT * FROM Users_db";
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
                    dataGridView1.Columns["id"].HeaderText = "Порядковий номер";
                    dataGridView1.Columns["login"].HeaderText = "Логін";
                    dataGridView1.Columns["pass"].HeaderText = "Пароль";
                    dataGridView1.Columns["rights"].HeaderText = "Права доступу";
                    dataGridView1.Columns["fullName"].HeaderText = "ФІО";
                }

                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додаванні користувача: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // Открываем форму для добавления нового пользователя
            AddUserForm addUserForm = new AddUserForm();
            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                // При успешном добавлении обновляем список пользователей
                LoadUserAccounts();
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                // Получаем значение rights выбранного пользователя из выделенной строки
                string selectedUserRights = dataGridView1.SelectedRows[0].Cells["rights"].Value.ToString();

                // Открываем форму для редактирования пользователя
                EditUserForm editUserForm = new EditUserForm(selectedUserId, selectedUserRights);
                if (editUserForm.ShowDialog() == DialogResult.OK)
                {
                    // При успешном редактировании обновляем список пользователей
                    LoadUserAccounts();
                }
            }
            else
            {
                MessageBox.Show("Виберіть користувача для редагування.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного пользователя из выделенной строки
                int selectedUserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                string login = dataGridView1.SelectedRows[0].Cells["login"].Value.ToString();
                string password = dataGridView1.SelectedRows[0].Cells["pass"].Value.ToString();

                DialogResult result = MessageBox.Show($"Ви впевнені, що бажаєте видалити цього користувача : логін - '{login}', пароль - '{password}'?", "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dataBase.openConnection();
                        string query = $"DELETE FROM Users_db WHERE id = {selectedUserId}";
                        SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                        command.ExecuteNonQuery();
                        dataBase.closeConnection();
                        MessageBox.Show("Користувача успішно видалено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadUserAccounts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при видаленні користувача: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть користувача для видалення.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void AccForm_FormClosing(object sender, FormClosingEventArgs e)
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