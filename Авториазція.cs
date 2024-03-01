using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Атестація
{
    public partial class Вхід : Form
    {
        public static string FullName { get; set; }
        data_base dataBase = new data_base();
        
        public Вхід()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана
        }

        private void Вхід_Load(object sender, EventArgs e)
        {
            pass.PasswordChar = '*';
            
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            var login = log.Text;
            var password = pass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"SELECT id, login, pass, rights, fullName FROM Users_db WHERE login = '{login}' AND pass = '{password}'";
            dataBase.openConnection();
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataBase.closeConnection();

            if (table.Rows.Count > 0)
            {
                string userRights = table.Rows[0]["rights"].ToString();
                FullName = table.Rows[0]["fullName"].ToString();

                if (userRights == "candidate")
                {
                    CandidateForm candidateForm = new CandidateForm();
                    candidateForm.FullName = FullName; // Передаем значение fullName на форму CandidateForm
                    this.Hide();
                    candidateForm.ShowDialog();
                }
                else if (userRights == "admin")
                {
                    AdminForm adminForm = new AdminForm();
                    adminForm.FullName = FullName; // Передаем значение fullName на форму AdminForm
                    this.Hide();
                    adminForm.ShowDialog();
                }


            }
            else
            {
                MessageBox.Show("Неправильне ім'я користувача або пароль!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Вхід_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (e.CloseReason == CloseReason.UserClosing) // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            {
                
                Application.Exit(); // Завершаем работу программы
            }
        }

        private void Вхід_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            dataBase.closeConnection();
        }
    }
}
