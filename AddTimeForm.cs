using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class AddTimeForm : Form
    {
        private data_base dataBase = new data_base();

        public AddTimeForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;// Запрещаем изменение размеров формы
            this.StartPosition = FormStartPosition.CenterScreen;// Устанавливаем форму по центру экрана
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string TimeName = textBoxTime.Text;

            if (!string.IsNullOrEmpty(TimeName))
            {
                try
                {
                    dataBase.openConnection();
                    string query = "INSERT INTO Time (Meaning) VALUES (@Name)";
                    SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                    command.Parameters.AddWithValue("@Name", TimeName);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запис успішно додано.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при додаванні запису: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    dataBase.closeConnection();
                }
            }
            else
            {
                MessageBox.Show("Введіть значення часу.", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
    }
}