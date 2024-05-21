using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //CommentInsert.InsertComment(CommentText.Text, UserNameTextBox.Text, StatusComboBox.SelectedIndex);
            CommentInsert.InsertCommentNew(CommentText.Text, UserNameTextBox.Text, StatusComboBox.SelectedIndex);
            Close();
        }
    }
}
