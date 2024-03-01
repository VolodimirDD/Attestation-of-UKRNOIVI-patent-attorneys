using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Атестація
{
    public partial class AdminForm : Form
    {
        public string FullName { get; set; }

        public AdminForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D; // Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen; // Устанавливаем форму по центру экрана
        }

        private void Accounts_Click(object sender, EventArgs e)
        {
            AccForm accform = new AccForm();
            accform.Show();
            this.Hide();
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (e.CloseReason == CloseReason.UserClosing) // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            {
                
                Application.Exit(); // Завершаем работу программы
            }
        }

        private void Attestations_Click(object sender, EventArgs e)
        {
            AttestationsForm qwf = new AttestationsForm();
            qwf.Show();
            this.Hide();
        }

        private void Questions_Click(object sender, EventArgs e)
        {
            QuestionsForm qwf = new QuestionsForm();
            qwf.Show();
            this.Hide();
        }

        private void Time_Click(object sender, EventArgs e)
        {
            TimeForm qwf = new TimeForm();
            qwf.Show();
            this.Hide();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения
        }
    }
}
