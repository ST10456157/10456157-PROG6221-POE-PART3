using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10456157_PROG6221_POE_PART3
{
    public partial class MainForm : Form
    {
        private List<string> activityLog = new List<string>();
        private string userName = "User";
        private string favoriteTopic = "";
        private string lastTopic = "";

        private Dictionary<string, string> keywordResponses = new Dictionary<string, string>
        {
            { "password", "Use strong, unique passwords and enable two-factor authentication." },
            { "privacy", "Limit what you share online and use secure apps." },
            { "scam", "Watch out for offers that seem too good to be true." }
        };

        private List<string> phishingTips = new List<string>
        {
            "Be cautious of emails asking for personal info.",
            "Never click suspicious links in emails.",
            "Verify sender email addresses carefully."
        };

        public MainForm()
        {
            InitializeComponent();
            PlayGreeting();
            SetupUI();
        }
        private void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");
                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
                else
                {
                    MessageBox.Show("Greeting audio file not found.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not play greeting audio.");
            }
        }

        private void SetupUI()
        {
            this.Text = "Cybersecurity Awareness Bot";
            this.Size = new Size(700, 550);

            Label asciiHeader = new Label
            {
                Font = new Font("Consolas", 8),
                Location = new Point(20, 0),
                Size = new Size(650, 60),
                Text = "  ___  _  _  ____  ____  ____  ____  ____  ___  _  _  ____  __  ____  _   \n" +
           " / __)( \\/ )(  _ \\(  __)(  _ \\/ ___)(  __)/ __)/ )( \\(  _ \\(  )(_  _)( \\/ )   (  _ \\ /  \\(_  _)\n" +
           "( (__  )  /  ) _ ( ) _)  )   /\\___ \\ ) _)( (__ ) \\/ ( )   / )(   )(   )  /   /   ) _ ((  O ) )(  \n" +
           " \\___)(__/  (____/(____)(__\\_)(____/(____)\\___)\\____/(__\\_)(__) (__) (__/      (____/ \\__/ (__)"
            };

            Label lblWelcome = new Label { Text = "Ask a cybersecurity question:", Location = new Point(20, 70), AutoSize = true };
            TextBox txtInput = new TextBox { Name = "txtInput", Location = new Point(20, 100), Width = 500 };
            Button btnSend = new Button { Text = "Ask", Location = new Point(530, 98) };
            ListBox lstOutput = new ListBox { Name = "lstOutput", Location = new Point(20, 140), Size = new Size(640, 250) };
            Button btnTasks = new Button { Text = "Task Assistant", Location = new Point(20, 410) };
            Button btnQuiz = new Button { Text = "Cyber Quiz", Location = new Point(140, 410) };
            Button btnLog = new Button { Text = "View Log", Location = new Point(260, 410) };

            btnSend.Click += (s, e) => HandleInput(txtInput.Text, lstOutput);
            btnTasks.Click += (s, e) => new TaskForm(activityLog).Show();
            btnQuiz.Click += (s, e) => new QuizForm(activityLog).Show();
            btnLog.Click += (s, e) => new ActivityLogForm(activityLog).Show();

            this.Controls.Add(asciiHeader);
            this.Controls.Add(lblWelcome);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnSend);
            this.Controls.Add(lstOutput);
            this.Controls.Add(btnTasks);
            this.Controls.Add(btnQuiz);
            this.Controls.Add(btnLog);
        }

        private void HandleInput(string input, ListBox output)
        {
            string lower = input.ToLower();
            string response = "I didn’t quite understand that. Can you rephrase?";

            if (string.IsNullOrWhiteSpace(input)) return;
            if (lower.Contains("exit")) { this.Close(); return; }

            if (lower.Contains("my name is"))
            {
                userName = input.Split(new[] { "is" }, StringSplitOptions.None).Last().Trim();
                response = $"Nice to meet you, {userName}. How can I assist you today?";
            }
            else if (lower.Contains("interested in"))
            {
                favoriteTopic = input.Split(new[] { "in" }, StringSplitOptions.None).Last().Trim();
                response = $"Great! I'll remember that you're interested in {favoriteTopic}.";
            }
            else if (lower.Contains("worried") || lower.Contains("frustrated") || lower.Contains("curious"))
            {
                response = "It's okay to feel that way. Let's work through it together.";
            }
            else if (lower.Contains("phishing"))
            {
                Random rand = new Random();
                response = phishingTips[rand.Next(phishingTips.Count)];
                lastTopic = "phishing";
            }
            else if (keywordResponses.Keys.Any(k => lower.Contains(k)))
            {
                string key = keywordResponses.Keys.First(k => lower.Contains(k));
                response = keywordResponses[key];
                lastTopic = key;
            }
            else if (lower.Contains("more") && !string.IsNullOrEmpty(lastTopic))
            {
                response = $"Here's another tip about {lastTopic}: Stay vigilant and keep your software updated.";
            }

            output.Items.Add($"> {userName}: {input}");
            output.Items.Add($"Bot: {response}");
            activityLog.Add($"{DateTime.Now}: {userName} asked '{input}' → {response}");
        }

    }
}
