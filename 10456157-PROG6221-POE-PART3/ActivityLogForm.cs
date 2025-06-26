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
    public partial class ActivityLogForm : Form
    {
        public ActivityLogForm(List<string> log)
        {
            InitializeComponent();
            this.Text = "Activity Log";
            this.Size = new System.Drawing.Size(500, 400);

            ListBox lstLog = new ListBox { Dock = DockStyle.Fill };
            foreach (string entry in log)
            {
                lstLog.Items.Add(entry);
            }
            this.Controls.Add(lstLog);
        }
        
    }
}
