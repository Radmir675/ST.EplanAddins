using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.LastTerminalStrip
{
    public partial class LoggerForm : Form
    {
        private readonly Project project;
        public delegate void EventHandler();
        public event EventHandler AccountHandler;
        public static List<Terminal> EmptyTerminalStrips { get; set; }
        public LoggerForm(Project project)
        {
            InitializeComponent();
            this.project = project;
        }
        public void ShowLogs(List<string> logs)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);
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
                richTextBox.AppendText(Environment.NewLine + DateTime.Now.ToString() + " | " + log.Remove(0, 1));
            }
            this.Show(eplanOwner);
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
            richTextBox.Text = null;
            InternalLogger internalLogger = new InternalLogger(project);
            var oldLogs = internalLogger.ReadFileLog();
            if (oldLogs.Any())
            {
                foreach (var log in oldLogs)
                {
                    richTextBox.AppendText(log + Environment.NewLine);
                }
            }
            back_plates_button.Enabled = false;
        }
        public void PressShowHistory()
        {
            button1.PerformClick();
        }

        private void back_plates_button_Click(object sender, EventArgs e)
        {
            richTextBox.Text = null;
            richTextBox.Text = "В следующих клеммах отсуствует торцевая пластина:" + Environment.NewLine;
            foreach (var text in EmptyTerminalStrips)
            {
                richTextBox.AppendText(text.Name + Environment.NewLine);
            }
            if (EmptyTerminalStrips.Any())
            {
                AccountHandler?.Invoke();
            }
        }
    }
}
