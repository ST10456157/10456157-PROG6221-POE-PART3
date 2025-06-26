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
    public partial class TaskForm : Form
    {
        private List<string> activityLog;
        private List<TaskItem> tasks = new List<TaskItem>();

        public TaskForm(List<string> log)
        {
            InitializeComponent();
            activityLog = log;
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Task Assistant";
            this.Size = new System.Drawing.Size(400, 400);

            Label lblTitle = new Label { Text = "Task Title:", Top = 20, Left = 20 };
            TextBox txtTitle = new TextBox { Top = 45, Left = 20, Width = 300 };

            Label lblDate = new Label { Text = "Reminder Date:", Top = 80, Left = 20 };
            DateTimePicker dtpDate = new DateTimePicker { Top = 105, Left = 20, Width = 300 };

            Button btnAdd = new Button { Text = "Add Task", Top = 140, Left = 20 };
            ListBox lstTasks = new ListBox { Top = 180, Left = 20, Width = 340, Height = 150 };

            btnAdd.Click += (s, e) =>
            {
                string title = txtTitle.Text;
                DateTime reminder = dtpDate.Value;
                if (!string.IsNullOrWhiteSpace(title))
                {
                    tasks.Add(new TaskItem { Title = title, ReminderDate = reminder });
                    lstTasks.Items.Add($"{title} - {reminder.ToShortDateString()}");
                    activityLog.Add($"Task added: {title} (Reminder: {reminder})");
                }
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitle);
            this.Controls.Add(lblDate);
            this.Controls.Add(dtpDate);
            this.Controls.Add(btnAdd);
            this.Controls.Add(lstTasks);
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public DateTime ReminderDate { get; set; }
    }
        
}

