using Eplan.EplApi.ApplicationFramework;
using System;
using System.Windows.Forms;

namespace ST.EplAddin.Comments
{
    public partial class CommentInsertForm : Form
    {
        public CommentInsertForm()
        {
            InitializeComponent();
        }

        private void CommentInsertForm_Load(object sender, EventArgs e)
        {
            UserRights oUserRights = new UserRights();
            UserNameTextBox.Text = oUserRights.GetUser();
            StatusComboBox.SelectedIndex = 0;
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            CommentInsert.InsertCommentNew(CommentText.Text, UserNameTextBox.Text, StatusComboBox.SelectedIndex);
            Close();
        }

        private void CommentInsertForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
