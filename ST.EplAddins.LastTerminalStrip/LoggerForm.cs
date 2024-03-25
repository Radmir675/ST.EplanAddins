using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.LastTerminalStrip
{
    public partial class LoggerForm : Form
    {
        private readonly string projectName;

        public static List<string> EmptyTerminalStripsName { get; set; }
        public LoggerForm(string projectName)
        {
            InitializeComponent();
            this.projectName = projectName;
        }
        public void ShowLogs(List<string> logs)
        {
            if (richTextBox.Text != string.Empty)
            {
                richTextBox.AppendText(Environment.NewLine + "Следующие определения клеммников были добавлены:");
            }
            else
            {
                richTextBox.AppendText("Следующие определения клеммников были добавлены:");
            }
            foreach (string log in logs)
            {
                richTextBox.AppendText(Environment.NewLine + DateTime.Now.ToString() + " | " + log.Remove(0, 2));
            }
            this.Show();
        }

        private void richTextBox_Click(object sender, EventArgs e)
        {
            richTextBox.Focus();
        }

        private void LoggerForm_KeyDown(object sender, KeyEventArgs e)
        {
            string copyText = richTextBox.SelectedText;
            if (copyText != string.Empty)
                Clipboard.SetText(copyText);
        }

        private void ShowHistory_Click(object sender, EventArgs e)
        {
            InternalLogger internalLogger = new InternalLogger(projectName);
            var oldLogs = internalLogger.ReadFileLog();
            if (oldLogs.Any())
            {
                richTextBox.Text = null;
                foreach (var log in oldLogs)
                {
                    richTextBox.AppendText(log + Environment.NewLine);
                }
            }
        }
        public void PressShowHistory()
        {
            button1.PerformClick();
        }

        private void back_plates_button_Click(object sender, EventArgs e)
        {
            richTextBox.Text = null;
            richTextBox.Text = "В следующих клеммах отсуствует торцевая пластина:" + Environment.NewLine;
            foreach (var text in EmptyTerminalStripsName)
            {
                richTextBox.AppendText(text + Environment.NewLine);
            }

            Search search = new Search();
            search.ClearSearchDB(CurrentProject);
            search.AddToSearchDB(result.ToArray());

        }
    }
}
