using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.LastTerminalStrip
{
    public partial class LoggerForm : Form
    {
        public LoggerForm()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            InternalLogger internalLogger = new InternalLogger();
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
    }
}
