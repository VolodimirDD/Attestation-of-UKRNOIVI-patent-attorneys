using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Атестація
{
    public partial class Certification : Form
    {
        private List<Question> questions;
        private int currentQuestionIndex;
        private string attestation;
        private data_base dataBase;
        private Timer timer;
        string startTime;


        public Certification(string attestation)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            // Скрыть кнопки формы
            this.ControlBox = false;
            this.attestation = attestation;
            questions = new List<Question>();
            currentQuestionIndex = 0;
            dataBase = new data_base();

            dataBase.openConnection();

            // Получение вопросов для выбранной аттестации
            string questionQuery = "SELECT Q.Text, A.Text, A.IsCorrect " +
                                   "FROM Questions Q " +
                                   "JOIN Answers A ON Q.Id = A.QuestionId " +
                                   "WHERE Q.AttestationsId = (SELECT Id FROM Attestations WHERE Name = @Attestation)";
            SqlCommand questionCommand = new SqlCommand(questionQuery, dataBase.getConnection());
            questionCommand.Parameters.AddWithValue("@Attestation", attestation);

            using (SqlDataReader questionReader = questionCommand.ExecuteReader())
            {
                while (questionReader.Read())
                {
                    string questionText = questionReader.GetString(0);
                    string answerText = questionReader.GetString(1);
                    bool isCorrect = questionReader.GetBoolean(2);

                    Question existingQuestion = questions.Find(q => q.Text == questionText);
                    if (existingQuestion != null)
                    {
                        existingQuestion.Answers.Add(answerText);
                    }
                    else
                    {
                        questions.Add(new Question(questionText, new List<string> { answerText }, isCorrect));
                    }
                }
            }

            // Получение времени для выбранной аттестации
            string timeQuery = "SELECT T.Meaning " +
                               "FROM Time T " +
                               "JOIN Attestations A ON T.Id = A.TimeId " +
                               "WHERE A.Name = @Attestation";
            SqlCommand timeCommand = new SqlCommand(timeQuery, dataBase.getConnection());
            timeCommand.Parameters.AddWithValue("@Attestation", attestation);
            string timeMeaning = timeCommand.ExecuteScalar()?.ToString();

            dataBase.closeConnection();

            DisplayQuestion();

            if (timeMeaning != null)
            {
                labelTime.Text = timeMeaning;
                startTime = timeMeaning;
                // Запуск обратного отсчета времени
                TimeSpan timeSpan = TimeSpan.Parse(timeMeaning);
                timer = new Timer();
                timer.Interval = 1000;  // Интервал в миллисекундах (1 секунда)
                timer.Tick += (sender, e) =>
                {
                    timeSpan = timeSpan.Subtract(TimeSpan.FromSeconds(1));
                    labelTime.Text = timeSpan.ToString();

                    if (timeSpan.TotalSeconds <= 0)
                    {
                        timer.Stop();
                        List<(string, string, string)> results = new List<(string, string, string)>();
                        foreach (Question question in questions)
                        {
                            string userAnswer = ""; // Получите выбранный ответ пользователя
                            string correctAnswer = ""; // Получите правильный ответ из базы данных для данного вопроса

                            results.Add((question.Text, userAnswer, correctAnswer));
                        }
                        MessageBox.Show("Час закінчився. Атестація не пройдена!");

                        Results res = new Results(attestation, startTime, labelTime.Text, results);
                        res.Show();
                        this.Hide();
                    }
                };
                timer.Start();
            }
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                Question currentQuestion = questions[currentQuestionIndex];
                questionLabel.Text = currentQuestion.Text;

                answersListBox.Items.Clear();
                foreach (string answer in currentQuestion.Answers)
                {
                    answersListBox.Items.Add(answer);
                }

                answersListBox.IntegralHeight = false;
            }
            else
            {
                // Все вопросы пройдены
                MessageBox.Show("Тест завершено!");

                List<(string, string, string)> results = new List<(string, string, string)>();
                foreach (Question question in questions)
                {
                    string userAnswer = ""; // Получите выбранный ответ пользователя
                    string correctAnswer = ""; // Получите правильный ответ из базы данных для данного вопроса

                    results.Add((question.Text, userAnswer, correctAnswer));
                }

                Results res = new Results(attestation, startTime, labelTime.Text, results);
                res.Show();
                this.Hide();

                // Остановка таймера
                if (timer != null)
                {
                    timer.Stop();
                }
            }
        }

        private void Certification_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (answersListBox.SelectedItem != null)
            {
                // Обрабатываем ответ пользователя
                string selectedAnswer = answersListBox.SelectedItem.ToString();
                // Здесь можно выполнить необходимые действия для обработки ответа
                Question currentQuestion = questions[currentQuestionIndex];
                currentQuestion.UserAnswer = selectedAnswer;

                currentQuestionIndex++;

                if (currentQuestionIndex >= questions.Count)
                {
                    // Ответили на последний вопрос
                    if (timer != null)
                    {
                        timer.Stop();
                    }

                    MessageBox.Show("Тестування завершено!");

                    List<(string, string, string)> results = new List<(string, string, string)>();
                    foreach (Question question in questions)
                    {
                        string userAnswer = question.UserAnswer;
                        string correctAnswer = ""; // Получите правильный ответ из базы данных для данного вопроса

                        results.Add((question.Text, userAnswer, correctAnswer));
                    }

                    Results res = new Results(attestation, startTime, labelTime.Text, results);
                    res.Show();
                    this.Hide();
                    this.Close();
                }
                else
                {
                    DisplayQuestion();
                }
            }
            else
            {
                MessageBox.Show("Оберіть варіант відповіді!");
            }
        }

        private void Certification_Load(object sender, EventArgs e)
        {
            string fullNameValue = Вхід.FullName;
            fullName.Text = fullNameValue;
            fullName.TextAlign = HorizontalAlignment.Center; // Установка выравнивания текста по центру
            fullName.ReadOnly = true; // Установка текстового поля только для чтения
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public bool IsCorrect { get; set; }
        public string UserAnswer { get; set; } // Добавленное свойство

        public Question(string text, List<string> answers, bool isCorrect)
        {
            Text = text;
            Answers = answers;
            IsCorrect = isCorrect;
        }
    }
}
