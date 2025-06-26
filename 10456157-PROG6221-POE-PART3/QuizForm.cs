using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10456157_PROG6221_POE_PART3
{
    public partial class QuizForm : Form
    {
        private List<string> activityLog;
        private List<Question> questions;
        private int current = 0;
        private int score = 0;

        private Label lblQuestion;
        private RadioButton[] options;
        private Button btnSubmit;

        public QuizForm(List<string> log)
        {
            InitializeComponent();
            activityLog = log;
            SetupQuiz();
        }

        private void SetupQuiz()
        {
            this.Text = "Cybersecurity Quiz";
            this.Size = new System.Drawing.Size(500, 300);

            questions = new List<Question>
            {
                new Question("What is phishing?", new[] { "A type of malware", "Fake email scam", "Firewall attack", "Wi-Fi trick" }, 1),
                new Question("What's a strong password?", new[] { "123456", "Your name", "Mix of symbols", "password" }, 2),
                new Question("When should you update software?", new[] { "Never", "Once a year", "When updates are available", "Only when hacked" }, 2)
            };

            lblQuestion = new Label { Top = 20, Left = 20, Width = 440 };
            options = new RadioButton[4];
            for (int i = 0; i < 4; i++)
            {
                options[i] = new RadioButton { Top = 60 + i * 30, Left = 20, Width = 400 };
                this.Controls.Add(options[i]);
            }

            btnSubmit = new Button { Text = "Submit", Top = 200, Left = 20 };
            btnSubmit.Click += SubmitAnswer;

            this.Controls.Add(lblQuestion);
            this.Controls.Add(btnSubmit);

            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (current >= questions.Count)
            {
                MessageBox.Show($"Quiz complete! Score: {score}/{questions.Count}");
                activityLog.Add($"Quiz completed: {score}/{questions.Count}");
                this.Close();
                return;
            }

            Question q = questions[current];
            lblQuestion.Text = q.Text;
            for (int i = 0; i < 4; i++)
            {
                options[i].Text = q.Options[i];
                options[i].Checked = false;
            }
        }

        private void SubmitAnswer(object sender, EventArgs e)
        {
            var selected = options.ToList().FindIndex(rb => rb.Checked);
            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            if (selected == questions[current].CorrectIndex)
            {
                score++;
                activityLog.Add($"Q{current + 1}: Correct");
            }
            else
            {
                activityLog.Add($"Q{current + 1}: Incorrect");
            }

            current++;
            LoadQuestion();
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }

        public Question(string text, string[] options, int correctIndex)
        {
            Text = text;
            Options = options;
            CorrectIndex = correctIndex;
        }
        
    }
}
