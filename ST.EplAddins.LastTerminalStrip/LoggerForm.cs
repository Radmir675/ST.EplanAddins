using System;
using System.Windows.Forms;

namespace ST.EplAddin.LastTerminalStrip
{
    public partial class LoggerForm : Form
    {
        public LoggerForm()
        {
            InitializeComponent();
        }
        public void AddFirstLog()
        {
            if (richTextBox.Text != string.Empty)
            {
                richTextBox.AppendText(Environment.NewLine + "Следующие клеммы были добавлены:");
            }
            else
            {
                richTextBox.AppendText("Следующие клеммы были добавлены:");
            }
        }
        public void AddLog(string log)
        {
            richTextBox.AppendText(Environment.NewLine + DateTime.Now.ToString() + " | " + log.Remove(0, 2));
        }

        private void richTextBox_Click(object sender, EventArgs e)
        {
            richTextBox.Focus();
        }

        private void LoggerForm_KeyDown(object sender, KeyEventArgs e)
        {
            string copyText = richTextBox.SelectedText;
            Clipboard.SetText(copyText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функционал находится в разработке");
        }
    }
}
