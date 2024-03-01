using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Атестація
{
    public partial class QuestionsForm : Form
    {
        private data_base dataBase = new data_base();

        public QuestionsForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;  
            dataGridView1.ReadOnly = true; // Установка свойства ReadOnly для DataGridView
            dataGridView1.AllowUserToDeleteRows = false;
            // Установка режима автоматического изменения размера столбцов для отображения всего содержимого
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Установка свойства AutoSizeMode для каждого столбца
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void QuestionsForm_Load(object sender, EventArgs e)
        {
            LoadQuestionsData();
            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения
        }

        private void LoadQuestionsData()
        {
            try
            {
                dataBase.openConnection();
                string query = @"SELECT Q.Id, C.Name AS Category, Q.[Text] AS Question, A.Name AS Attestation,
                        AN.[Text] AS Answer, AN.IsCorrect, SQ.[Text] AS SubQuestion
                        FROM Questions Q
                        JOIN Categories C ON Q.CategoryId = C.Id
                        JOIN Attestations A ON Q.AttestationsId = A.Id
                        LEFT JOIN Answers AN ON Q.Id = AN.QuestionId
                        LEFT JOIN SubQuestions SQ ON Q.Id = SQ.QuestionId";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Очищаем DataGridView перед загрузкой данных
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                if (table.Rows.Count > 0)
                {
                    // Создаем новую таблицу для отображения данных
                    DataTable displayTable = new DataTable();

                    // Добавляем необходимые столбцы в новую таблицу
                    displayTable.Columns.Add("ID", typeof(int));
                    displayTable.Columns.Add("Категорія", typeof(string));
                    displayTable.Columns.Add("Атестація", typeof(string));
                    displayTable.Columns.Add("Питання", typeof(string));
                    displayTable.Columns.Add("Правильна відповідь", typeof(string));
                    displayTable.Columns.Add("Підпитання", typeof(string));
                    displayTable.Columns.Add("Варіанти відповідей", typeof(string));

                    int prevId = -1; // Идентификатор предыдущего вопроса

                    // Добавляем строки с данными в новую таблицу
                    foreach (DataRow row in table.Rows)
                    {
                        int questionId = Convert.ToInt32(row["Id"]);
                        string questionText = row["Question"].ToString();
                        string answerText = row["Answer"].ToString();
                        bool isCorrect = Convert.ToBoolean(row["IsCorrect"]);

                        if (questionId != prevId)
                        {
                            // Добавляем основную информацию о вопросе
                            DataRow questionRow = displayTable.NewRow();
                            questionRow["ID"] = questionId;
                            questionRow["Категорія"] = row["Category"];
                            questionRow["Атестація"] = row["Attestation"];
                            questionRow["Питання"] = questionText;
                            questionRow["Правильна відповідь"] = isCorrect ? answerText : "";
                            displayTable.Rows.Add(questionRow);

                            prevId = questionId;
                        }

                        // Добавляем підпитання для текущего вопроса
                        if (!string.IsNullOrEmpty(row["SubQuestion"].ToString()))
                        {
                            DataRow subQuestionRow = displayTable.NewRow();
                            subQuestionRow["Підпитання"] = row["SubQuestion"];
                            displayTable.Rows.Add(subQuestionRow);
                        }

                        // Добавляем варианты ответов для текущего вопроса
                        if (!string.IsNullOrEmpty(answerText))
                        {
                            DataRow answerRow = displayTable.NewRow();
                            answerRow["Варіанти відповідей"] = answerText;
                            displayTable.Rows.Add(answerRow);
                        }
                    }

                    // Устанавливаем источник данных для DataGridView
                    dataGridView1.DataSource = displayTable;
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

        private void Back_Click(object sender, EventArgs e)
        {
            AdminForm admform = new AdminForm();
            admform.Show();
            this.Hide();
        }

        private void QuestionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            {

                Application.Exit(); // Завершаем работу программы
            }
        }
    }
}