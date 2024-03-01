using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
namespace Атестація
{
    public partial class EditUserForm : Form
    {
        private int userId;
        private data_base dataBase = new data_base();
        private string selectedUserRights;

        public EditUserForm(int selectedUserId, string rights)
        {
            InitializeComponent();
            userId = selectedUserId;
            selectedUserRights = rights;
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана 
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            LoadUserData();
            LoadRights();  
        }


        private void LoadUserData()
        {
            try
            {
                dataBase.openConnection();
                string query = $"SELECT * FROM Users_db WHERE id = {userId}";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    txtLogin.Text = reader["login"].ToString();
                    txtPassword.Text = reader["pass"].ToString();
                    fullName.Text = reader["fullName"].ToString();
                }

                reader.Close();
                dataBase.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні даних користувача: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void LoadRights()
        {
            cmbRights.Items.Clear();
            cmbRights.Items.Add("admin");
            cmbRights.Items.Add("candidate");
            cmbRights.SelectedItem = selectedUserRights;
            cmbRights.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();
            string rights = cmbRights.SelectedItem?.ToString(); // Получаем выбранное право доступа, используя безопасную навигацию по объектам
            string fio = fullName.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rights) || string.IsNullOrEmpty(fio))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                return;
            }

            try
            {
                dataBase.openConnection();
                string query = $"UPDATE Users_db SET login = '{login}', pass = '{password}', rights = '{rights}', fullName = '{fio}' WHERE id = {userId}";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.ExecuteNonQuery();
                dataBase.closeConnection();
                MessageBox.Show("Дані користувача оновлено.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при оновленні даних користувача: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        
    }
}