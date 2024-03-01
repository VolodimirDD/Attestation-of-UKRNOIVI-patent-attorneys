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
    public partial class Results : Form
    {
        private string attestation;
        private string endTime;
        private List<(string, string, string)> results;
        private data_base dataBase;
        private string startTime;

        public Results(string attestation, string startTime, string endTime, List<(string, string, string)> results)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.attestation = attestation;
            this.endTime = endTime;
            this.results = results;
            this.startTime = startTime;
            dataBase = new data_base();
            dataGridView1.ReadOnly = true;
            // Установка режима автоматического изменения размера столбцов для отображения всего содержимого
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Установка свойства AutoSizeMode для каждого столбца
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            
        }

        private string GetCorrectAnswerForQuestion(string question)
        {
            dataBase.openConnection();

            string answerQuery = "SELECT A.Text " +
                                 "FROM Answers A " +
                                 "JOIN Questions Q ON A.QuestionId = Q.Id " +
                                 "WHERE Q.Text = @Question AND A.IsCorrect = 1";
            SqlCommand answerCommand = new SqlCommand(answerQuery, dataBase.getConnection());
            answerCommand.Parameters.AddWithValue("@Question", question);

            string correctAnswer = answerCommand.ExecuteScalar()?.ToString();

            dataBase.closeConnection();

            return correctAnswer;
        }

        private void Results_Load_1(object sender, EventArgs e)
        {
            // Вывод наименования аттестации и времени окончания
            attestationLabel.Text = attestation;
            endTimeLabel.Text = endTime;
            timeLabel.Text = startTime;

            // Создание столбцов в DataGridView
            dataGridView1.Columns.Add("questionColumn", "Питання");
            dataGridView1.Columns.Add("userAnswerColumn", "Ваша відповідь");
            dataGridView1.Columns.Add("correctAnswerColumn", "Правильна відповідь");

            // Подписываемся на событие CellFormatting
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;

            int correctAnswersCount = 0;
            int totalQuestionsCount = results.Count;

            // Вывод результатов в DataGridView и подсчет правильных ответов
            foreach (var result in results)
            {
                string question = result.Item1;
                string userAnswer = result.Item2;
                string correctAnswer = GetCorrectAnswerForQuestion(question);

                dataGridView1.Rows.Add(question, userAnswer, correctAnswer);

                if (userAnswer == correctAnswer)
                {
                    correctAnswersCount++;
                }
            }

            // Вычисление использованного времени
            DateTime startTimeValue = DateTime.Parse(startTime);
            DateTime endTimeValue = DateTime.Parse(endTime);
            TimeSpan timeUsed = endTimeValue - startTimeValue;
            endTimeLabel.Text = timeUsed.ToString(@"hh\:mm\:ss");

            // Вычисление процента правильных ответов
            double percentage = (double)correctAnswersCount / totalQuestionsCount * 100;

            // Вывод количества правильных ответов
            trueLabel.Text = $"Вірних відповідей {correctAnswersCount} з {totalQuestionsCount} ({Math.Round(percentage, 1)}%)";

            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // Проверяем, является ли причина закрытия формы нажатием на кнопку закрытия формы (крестик)
            {

                Application.Exit(); // Завершаем работу программы
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Проверяем, что текущая ячейка принадлежит столбцам "userAnswerColumn" и "correctAnswerColumn"
            if (dataGridView1.Columns[e.ColumnIndex].Name == "userAnswerColumn" || dataGridView1.Columns[e.ColumnIndex].Name == "correctAnswerColumn")
            {
                // Получаем значения ячеек для текущей строки
                var userAnswer = dataGridView1.Rows[e.RowIndex].Cells["userAnswerColumn"].Value?.ToString();
                var correctAnswer = dataGridView1.Rows[e.RowIndex].Cells["correctAnswerColumn"].Value?.ToString();

                // Если оба значения пустые, не устанавливаем цвет фона для строки
                if (string.IsNullOrEmpty(userAnswer) && string.IsNullOrEmpty(correctAnswer))
                {
                    e.CellStyle.BackColor = Color.Transparent;
                }
                // Если значения совпадают, устанавливаем синий цвет фона для строки
                else if (userAnswer == correctAnswer)
                {
                    e.CellStyle.BackColor = Color.Blue;
                }
                // Если значения отличаются, устанавливаем красный цвет фона для строки
                else
                {
                    e.CellStyle.BackColor = Color.Red;
                }
            }
        }
    }
}
