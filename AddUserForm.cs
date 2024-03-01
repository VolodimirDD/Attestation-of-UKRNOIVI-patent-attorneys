using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class AddUserForm : Form
    {
        private data_base dataBase = new data_base();

        public AddUserForm()
        {
            InitializeComponent();
            LoadRights();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;// Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen;// Устанавливаем форму по центру экрана

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            string rights = cmbRights.SelectedItem?.ToString(); // Получение выбранного значения прав доступа
            string fio = fullName.Text.Trim();
            

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rights) || string.IsNullOrEmpty(fio))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            }

            try
            {
                dataBase.openConnection();
                string query = $"INSERT INTO Users_db (login, pass, rights, fullName) VALUES ('{login}', '{password}', '{rights}', '{fio}')";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.ExecuteNonQuery();
                dataBase.closeConnection();
                MessageBox.Show("Користувача успішно додано.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додаванні користувача: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private void LoadRights()
        {
            cmbRights.Items.Add("admin");
            cmbRights.Items.Add("candidate");
            // Запрещаем ввод с клавиатуры
            cmbRights.DropDownStyle = ComboBoxStyle.DropDownList;
        }

    }
}