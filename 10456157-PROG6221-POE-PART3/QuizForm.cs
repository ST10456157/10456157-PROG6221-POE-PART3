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
            this.Size = new System.Drawing.Size(600, 350);

            questions = new List<Question>
            {
                new Question("What is phishing?",
                    new[] {"A type of malware", "Fake email scam", "Firewall attack", "Wi-Fi trick"},
                    1,
                    "Phishing is a fake email scam designed to trick users into giving personal information."),

                new Question("What makes a strong password?",
                    new[] {"123456", "Your name", "Mix of symbols, letters, and numbers", "Password"},
                    2,
                    "A strong password uses symbols, numbers, and letters with no personal details."),

                new Question("When should you update software?",
                    new[] {"Never", "Once a year", "When updates are available", "Only when hacked"},
                    2,
                    "Regular updates help patch security vulnerabilities."),

                new Question("Which indicates a secure website?",
                    new[] {"http://example.com", "https://secure.com", "ftp://file.com", "None"},
                    1,
                    "HTTPS encrypts data and indicates the site uses a security certificate."),

                new Question("What does 2FA stand for?",
                    new[] {"Fast authentication", "Two-factor authentication", "Firewall access", "File access"},
                    1,
                    "Two-factor authentication adds a second step to secure your account."),

                new Question("What should you avoid in emails?",
                    new[] {"Typos", "Urgent language", "Unfamiliar links", "All of the above"},
                    3,
                    "These are common phishing tactics—watch out for them all!"),

                new Question("What is malware?",
                    new[] {"Harmful software", "A game", "Search engine", "Image editor"},
                    0,
                    "Malware is software designed to harm or exploit devices or networks."),

                new Question("Best way to protect privacy?",
                    new[] {"Share passwords", "Use strong passwords", "Post everything online", "Ignore updates"},
                    1,
                    "Strong passwords are essential for privacy protection."),

                new Question("Which is NOT safe?",
                    new[] {"VPN", "Firewall", "Clicking unknown ads", "Antivirus"},
                    2,
                    "Avoid clicking unknown ads—they may lead to malicious sites."),

                new Question("What to do with suspicious emails?",
                    new[] {"Reply", "Click link", "Delete it", "Ignore it"},
                    2,
                    "Never interact—just delete suspicious or unexpected emails.")
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

            Question q = questions[current];
            bool isCorrect = selected == q.CorrectIndex;

            string feedback;
            if (isCorrect)
            {
                score++;
                feedback = $"✅ Correct!\n\nExplanation: {q.Explanation}";
                activityLog.Add($"Q{current + 1}: Correct");
            }
            else
            {
                feedback = $"❌ Incorrect.\n\nCorrect Answer: {q.Options[q.CorrectIndex]}\nExplanation: {q.Explanation}";
                activityLog.Add($"Q{current + 1}: Incorrect");
            }

            MessageBox.Show(feedback, $"Question {current + 1} Feedback");

            current++;
            LoadQuestion();
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }

        public Question(string text, string[] options, int correctIndex, string explanation)
        {
            Text = text;
            Options = options;
            CorrectIndex = correctIndex;
            Explanation = explanation;
        }
    }
}
