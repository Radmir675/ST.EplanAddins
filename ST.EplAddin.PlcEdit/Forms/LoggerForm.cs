using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class LoggerForm : Form
    {
        public LoggerForm(List<string> messages)
        {
            InitializeComponent();
            foreach (string message in messages)
            {
                richTextBox.AppendText(message + Environment.NewLine);
            }
        }

        private void LoggerForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
