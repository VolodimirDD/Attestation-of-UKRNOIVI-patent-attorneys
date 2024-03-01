using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class CandidateForm : Form
    {
        private data_base dataBase = new data_base();
        public string FullName { get; set; }

        public CandidateForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;// Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen;// Устанавливаем форму по центру экрана
        }

        private void CandidateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Завершаем работу программы
                Application.Exit();
            }
        }

        private void CandidateForm_Load(object sender, EventArgs e)
        {
            dataBase.openConnection();
            fullName.Text = FullName;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения       

            string query = "SELECT Name FROM Attestations";
            SqlCommand command = new SqlCommand(query, dataBase.getConnection());
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                string attestationName = row["Name"].ToString();
                attestations.Items.Add(attestationName);
            }

            dataBase.closeConnection();
        }

        private void btnChoise_Click(object sender, EventArgs e)
        {
            string selectedAttestation = attestations.SelectedItem as string;
            if (selectedAttestation != null)
            {
                dataBase.openConnection();

                // Проверка наличия вопросов для выбранной аттестации
                string questionCountQuery = "SELECT COUNT(*) FROM Questions WHERE AttestationsId = (SELECT Id FROM Attestations WHERE Name = @Attestation)";
                SqlCommand questionCountCommand = new SqlCommand(questionCountQuery, dataBase.getConnection());
                questionCountCommand.Parameters.AddWithValue("@Attestation", selectedAttestation);

                int questionCount = (int)questionCountCommand.ExecuteScalar();
                if (questionCount == 0)
                {
                    MessageBox.Show("Запитання для обраної атестації не додано. Будь ласка, виберіть іншу атестацію.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataBase.closeConnection();
                    return;
                }

                dataBase.closeConnection();

                // Открываем следующую форму, передавая выбранную аттестацию
                Certification nextForm = new Certification(selectedAttestation);
                nextForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть атестацію.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
